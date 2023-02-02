using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EkspertizaWebApi.Data;

public class Province
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public ICollection<City> Cities { get; set; } = new HashSet<City>();
}

public class ProvinceEntityTypeConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder
            .HasIndex(p => new { p.Name })
            .IsUnique(true);

        builder
            .Property(p => p.Name)
            .HasMaxLength(450)
            .IsRequired();

        builder
            .HasMany(p => p.Cities)
            .WithOne(p => p.Province)
            .HasForeignKey(p => p.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
