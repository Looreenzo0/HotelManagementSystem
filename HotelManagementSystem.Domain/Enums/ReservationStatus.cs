

using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Domain.Enums
{
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Guaranteed,
        [Display(Name = "Check-in")]
        Checked_In,
        [Display(Name = "Check-out")]
        Checked_Out,
        Canceled6
    }
}
