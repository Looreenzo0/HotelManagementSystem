using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Application.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelManagementSystem.WebApp.Controllers
{
    public class HotelRoomController : Controller
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IUnitOfWorkService _unitOfWorkService;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public HotelRoomController(ILogger<
            ReservationController> logger,
            IMemoryCache cache,
            IUnitOfWorkService unitOfWorkService,
            ITimeProviderService timeProvider)
        {
            _logger = logger;
            _cache = cache;
            _unitOfWorkService = unitOfWorkService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SearchAllRooms()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["columns[0][search][value]"].FirstOrDefault();

                int skip = 0;
                if (!string.IsNullOrEmpty(start))
                {
                    skip = int.Parse(start);
                }
                int recordsTotal = 0;

                var parameters = new FindParameters
                {
                    Find = searchValue,
                    Take = 500
                };

                var model = await _unitOfWorkService.HotelRooms.SearchHotelRoomByParams(parameters).OrderByDescending(item => item.Id).ToListAsync();

                recordsTotal = model.Count();
                var data = model.Skip(skip).ToList();

                _cache.Set("Room", model, TimeSpan.FromMinutes(5));

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

    }
}
