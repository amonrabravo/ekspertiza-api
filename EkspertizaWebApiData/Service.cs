using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EkspertizaWebApiData;

public class Service
{
    public Guid Id { get; set; }

    public DateTime DateCreated { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public int CityId { get; set; }

    public City? City { get; set; }

    public string? PhoneNumber { get; set; }

    public Guid OrganizationId { get; set; }

    public Organization? Organization { get; set; }

}

public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder
            .HasIndex(p => new { p.Name })
            .IsUnique(false);

        builder
            .Property(p => p.Name)
            .HasMaxLength(450)
            .IsRequired();

        builder
            .Property(p => p.PhoneNumber)
            .HasMaxLength(10)
            .IsUnicode(false)
            .IsFixedLength(true);

    }
}
