using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classroom.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classroom.Data.Context.Configurations
{
    public class UserSchoolConfigurations : IEntityTypeConfiguration<UserSchool>
    {

        public void Configure(EntityTypeBuilder<UserSchool> builder)
        {
            builder.ToTable("users_school");
            builder.HasKey(userSchool => new { userSchool.UserId, userSchool.SchoolId });

            builder
                .HasOne(userSchool => userSchool.School)
                .WithMany(user=>user.UserSchools)
                .HasForeignKey(UserSchool => UserSchool.UserId); 
            
            builder
                .HasOne(userSchool => userSchool.School)
                .WithMany(school=> school.UserSchools)
                .HasForeignKey(userSchool => userSchool.SchoolId);
            
        }

    }
}
