using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Configrations
{
    internal class CategoryConfigrations : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.Property(X => X.CategoryName)
                 .HasColumnType("varchar")
                 .HasMaxLength(20);

            builder.Property(X => X.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

            builder.HasData(
                             new Category { Id = 1, CategoryName = "Cardio" },
                             new Category { Id = 2, CategoryName = "Strength" },
                             new Category { Id = 3, CategoryName = "Yoga" },
                             new Category { Id = 4, CategoryName = "Boxing" },
                             new Category { Id = 5, CategoryName = "CrossFit" }
                         );

        }
    }
}
   