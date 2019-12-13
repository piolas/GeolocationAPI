using Geolocation.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geolocation.Infrastructure.Persistence.Configuration
{
    public class RootObjectConfiguration : IEntityTypeConfiguration<RootObject>
    {
        public void Configure(EntityTypeBuilder<RootObject> builder)
        {
            //builder.HasKey(r => r.Id);
        }
    }
}
