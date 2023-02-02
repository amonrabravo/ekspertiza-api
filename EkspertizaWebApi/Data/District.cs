using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EkspertizaWebApi.Data;

public class District
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int CityId { get; set; }

    public City? City { get; set; }

    public ICollection<Organization> Organizations { get; set; } = new HashSet<Organization>();
}

public class DistrictEntityTypeConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder
            .HasIndex(p => new { p.Name, p.CityId })
            .IsUnique(true);

        builder
            .Property(p => p.Name)
            .HasMaxLength(450)
            .IsRequired();

        builder
            .HasMany(p => p.Organizations)
            .WithOne(p => p.District)
            .HasForeignKey(p => p.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
