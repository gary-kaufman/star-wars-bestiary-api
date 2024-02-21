using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StarWarsBestiaryApi;

public class CreatureContext: DbContext
{
    public CreatureContext(DbContextOptions<CreatureContext> options)
        : base(options)
    {
    }

    public DbSet<Creature> Creatures { get; set; } = null!;

}
