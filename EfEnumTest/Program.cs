// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

return;

internal sealed class Context : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder
			.EnableSensitiveDataLogging()
			.LogTo(Console.WriteLine, LogLevel.Information)
			.UseNpgsql(o => {
				o.MapEnum<Foo>();
			});

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Model>(b => {
			b.Property(p => p.Id)
				.ValueGeneratedOnAdd();
			b.Property(p => p.Foo)
				.IsRequired()
				.HasDefaultValue(Foo.One)
				.HasSentinel((Foo)(-1));
			b.Property(p => p.Bar)
				.IsRequired()
				.HasDefaultValue(Bar.Four)
				.HasSentinel((Bar)(-1));
		});
	}
}

internal sealed class Model
{
	public int Id { get; set; }
	public Foo Foo { get; set; }
	public Bar Bar { get; set; }
}

internal enum Foo
{
	One,
	Two,
	Three
}

internal enum Bar
{
	Four,
	Five,
	Six
}