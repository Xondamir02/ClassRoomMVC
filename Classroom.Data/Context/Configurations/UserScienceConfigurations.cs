using Classroom.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classroom.Data.Context.Configurations;

public class UserScienceConfigurations : IEntityTypeConfiguration<UserScience>
{
    public void Configure(EntityTypeBuilder<UserScience> builder)
    {
        builder.HasKey(s => new { s.UserId, s.ScienceId });
    }
}