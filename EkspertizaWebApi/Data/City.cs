using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EkspertizaWebApi.Data;

public class City
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int ProvinceId { get; set; }

    public Province? Province { get; set; }

    public ICollection<District> Districts { get; set; } = new HashSet<District>();
}

public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasIndex(p => new { p.Name, p.ProvinceId })
            .IsUnique(true);

        builder
            .Property(p => p.Name)
            .HasMaxLength(450)
            .IsRequired();

        builder
            .HasMany(p => p.Districts)
            .WithOne(p => p.City)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
