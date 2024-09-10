using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Application.Models;
using HotelManagementSystem.Application.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace HotelManagementSystem.WebApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly ITimeProviderService _timeProvider;

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ReservationController(ILogger<
            ReservationController> logger,
            IMemoryCache cache,
            IUnitOfWorkService unitOfWork,
            ITimeProviderService timeProvider)
        {
            _logger = logger;
            _cache = cache;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }
        public IActionResult Index()
        {
            try
            {
                var data = new ReservationModel();

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured.");
                throw;
            }

        }

        public IActionResult NewReservation()
        {
            try
            {
                var data = new HotelRoomModel();

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured.");
                throw;
            }

        }

        public async Task<IActionResult> LoadReservation()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["columns[0][search][value]"].FirstOrDefault();
                var docStatus = Request.Form["columns[1][search][value]"].FirstOrDefault();
                var dateFrom = Request.Form["columns[2][search][value]"].FirstOrDefault();
                var dateTo = Request.Form["columns[3][search][value]"].FirstOrDefault();

                if (!string.IsNullOrEmpty(dateFrom))
                {
                    DateFrom = Convert.ToDateTime(dateFrom);
                }

                if (!string.IsNullOrEmpty(dateTo))
                {
                    DateTo = Convert.ToDateTime(dateTo);
                    DateTo = DateTo.Date.AddHours(24);
                }

                int pageSize = 0;
                if (!string.IsNullOrEmpty(length))
                {
                    pageSize = int.Parse(length);
                }
                int skip = 0;
                if (!string.IsNullOrEmpty(start))
                {
                    skip = int.Parse(start);
                }
                int recordsTotal = 0;

                var parameters = new FindParameters
                {
                    Find = searchValue,
                    Status = docStatus,
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                    Take = 500
                };

                var model = await _unitOfWork.Reservations.GetAllReservationsByParams(parameters).OrderByDescending(item => item.Id).ToListAsync();

                _cache.Set("Reservation", model, TimeSpan.FromMinutes(5));

                recordsTotal = model.Count();
                var data = model.Skip(skip).Take(pageSize).ToList();
                return Json(new
                {
                    draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal,
                    data
                });


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> NewReservation([FromBody] ReservationModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (model.CheckIn.HasValue)
                    {
                        model.CheckIn = model.CheckIn.Value.Date.AddHours(14); // Default to 2 PM
                    }

                    // Check if CheckOut has been provided and set default time to 12 PM if time is not included
                    if (model.CheckOut.HasValue)
                    {
                        model.CheckOut = model.CheckOut.Value.Date.AddHours(12); // Default to 12 PM
                    }
                    DateTime currentTime = _timeProvider.GetCurrentTime();
                    string fomattedRefNum = currentTime.ToString("MMddyyHHmmss");

                    model.CreatedBy = model.Name;
                    model.IsWalkIn = false;
                    model.RefNum = fomattedRefNum;


                    await _unitOfWork.Reservations.AddAsync(model);

                    await _unitOfWork.CompleteAsync();

                    return Json(new { success = true, responseText = "Successfully submitted!", refNum = model.RefNum });
                }
                else
                {
                    const string Error = "Something went wrong!";
                    return Json(new { success = false, responseText = Error });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> ReservationSuccess(string refNum)
        {
            try
            {
                var reserveData = await _unitOfWork.Reservations.GetReservationDataAsync(refNum);
                if (reserveData != null)
                {
                    _cache.Set("Reservation", reserveData, TimeSpan.FromMinutes(5));

                    return View(reserveData);

                }
                else
                {
                    ViewData["ControlNo"] = "Error: Reservation data not found.";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred.");
                return StatusCode(500, new { ErrorMessage = "An error occurred." });
            }
        }
    }
}
