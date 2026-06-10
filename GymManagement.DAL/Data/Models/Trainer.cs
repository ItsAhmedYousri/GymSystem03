using GymManagement.DAL.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Models
{
    public class Trainer:GymUser
    {
        public Specialties Specialties { get; set; }

        ICollection<Trainer> Trainers { get; set; } = default!;
    }
}
