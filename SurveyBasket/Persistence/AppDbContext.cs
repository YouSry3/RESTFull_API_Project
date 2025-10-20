



using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SurveyBasket.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options,
        IHttpContextAccessor httpContextAccessor ):
        IdentityDbContext(options)
    {
        private readonly IHttpContextAccessor HttpContextAccessor = httpContextAccessor;

        public DbSet<Answer> Answers { get; set;}
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public DbSet<VoteAnswer> VoteAnswers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

                base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditEntity>();
            foreach (var entry in entries)
            {
                var currentUserId = HttpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
                if (entry.State == EntityState.Added)
                {

                    entry.Property(x=> x.CreatedById).CurrentValue = currentUserId;// set the user id from the context
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(x => x.UpdatedById).CurrentValue = currentUserId;// set the user id from the context
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
