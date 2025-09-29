﻿

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.EntitiesConfigurations
{
    public class PollConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder.HasIndex(x => x.Title).IsUnique();

            builder.Property(x => x.Title)
                .HasMaxLength(100);
            builder.Property(x => x.Description)
                .HasMaxLength(1500);
        }
    }
}
