

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.EntitiesConfigurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasIndex(x => new { x.pollId, x.userId }).IsUnique();

            
          
        }
    }
}
