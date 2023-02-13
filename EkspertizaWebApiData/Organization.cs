using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EkspertizaWebApiData;


public class Organization 
{
    public Guid Id { get; set; }

    public DateTime DateCreated { get; set; }

    public required string Name { get; set; }


}

public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder
            .HasIndex(p => new { p.Name  })
            .IsUnique(false);

        builder
            .Property(p => p.Name)
            .HasMaxLength(450)
            .IsRequired();


    }
}
