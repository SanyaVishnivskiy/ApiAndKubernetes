namespace RestApi.Infrastructure.EF;

public class CategoriesDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }

    public CategoriesDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>(e =>
        {
            e.Property(c => c.Id).HasMaxLength(Category.IdMaxLength);
            e.Property(c => c.Name).HasMaxLength(Category.NameMaxLength);
            e.HasIndex(c => c.Name).IsUnique();
        });

        builder.Entity<Item>(e =>
        {
            e.Property(i => i.Id).HasMaxLength(Item.IdMaxLength);
            e.Property(i => i.Name).HasMaxLength(Item.NameMaxLength);
            e.HasIndex(i => i.Name).IsUnique();
            e.Property(i => i.CategoryId).HasMaxLength(Item.CategoryIdMaxLength);
        });
    }
}
