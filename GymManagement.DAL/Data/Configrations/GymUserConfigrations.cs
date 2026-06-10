using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GymManagement.DAL.Data.Configrations
{
    public class GymUserConfigrations<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Name).
                HasColumnType("varchar").
                HasMaxLength(50);

            builder.Property(p => p.Email).
               HasColumnType("varchar").
               HasMaxLength(100);

            builder.Property(X => X.Phone)
                   .HasColumnType("varchar")
                   .HasMaxLength(11);

            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUser_EmailCheck", "Email LIKE '_%@_%._%'");
                tb.HasCheckConstraint("GymUser_PhoneCheck", "[Phone] LIKE '010%' OR [Phone] LIKE '011%' OR [Phone] LIKE '012%' OR [Phone] LIKE '015%'");
            });

            builder.OwnsOne(p => p.Address, Address =>
            {
                Address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);

                Address.Property(a => a.City)
                      .HasColumnType("varchar")
                      .HasColumnName("City")
                      .HasMaxLength(30);

                Address.Property(a => a.BuildingNumber)
                       .HasColumnName("BuildingNumber");
            });


        }
    }
}
