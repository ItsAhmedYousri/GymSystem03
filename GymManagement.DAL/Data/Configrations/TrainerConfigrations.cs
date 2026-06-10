using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Configrations
{
    public class TrainerConfigrations:GymUserConfigrations<Trainer>,IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(p => p.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
