

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Persistence.Entities;

namespace SurveyBasket.Persistence.EntitiesConfigurations
{
    public class VoteAnswerConfiguration : IEntityTypeConfiguration<VoteAnswer>
    {
        public void Configure(EntityTypeBuilder<VoteAnswer> builder)
        {
            builder.HasIndex(x => new { x.voteId ,x.questionId }).IsUnique();
      
        }
    }
}
