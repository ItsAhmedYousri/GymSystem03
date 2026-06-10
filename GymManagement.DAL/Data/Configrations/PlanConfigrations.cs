using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.DAL.Data.Configrations
{
    public class PlanConfigrations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(p => p.Description)
                .HasMaxLength (200);
            
            
            builder.Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_Plan_Duration_Check", "[DurationDays] Between 1 and 365");
            });
        }
    }
}
