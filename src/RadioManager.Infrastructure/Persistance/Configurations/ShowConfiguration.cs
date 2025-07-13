using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RadioManager.Domain.Shows.Aggregates;
using RadioManager.Domain.Shows.ValueObjects;
using RadioManager.Domain.Types;

namespace RadioManager.Infrastructure.Persistance.Configurations
{
    internal sealed class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => AggregateId.Create(x))
                .IsRequired();

            builder.Property(x => x.Title)
                .HasConversion(x => x.Value, x => ShowTitle.Create(x))
                .IsRequired();

            builder.Property(x => x.Presenter)
                 .HasConversion(x => x.Value, x => Presenter.Create(x))
                .IsRequired();

            builder.OwnsOne(s => s.TimeSlot, timeSlotBuilder =>
            {
                timeSlotBuilder.Property(ts => ts.StartTime)
                    .IsRequired()
                    .HasColumnName("StartTime");

                timeSlotBuilder.Property(ts => ts.DurationMinutes)
                    .IsRequired()
                    .HasColumnName(name: "DurationMinutes");
            });
        }
    }
}
