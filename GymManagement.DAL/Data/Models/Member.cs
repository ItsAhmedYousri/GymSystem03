using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Models
{
    public class Member : GymUser
    {
        public string ?Photo { get; set; }

        public HealthRecord HealthRecord { get; set; } = default!;
        public ICollection<Membership> Memberships { get; set; } = default!;
        public ICollection<Booking> Bookings { get; set; } = default!;


    }
}
