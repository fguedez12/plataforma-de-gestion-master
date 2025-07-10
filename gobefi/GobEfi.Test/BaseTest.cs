using AutoMapper;
using GobEfi.Web.Core.Profiles;
using GobEfi.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.Test
{
    public class BaseTest
    {
        protected ApplicationDbContext ContextBuilder(string DbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(DbName).Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        protected IMapper AutoMapperConfiguration()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new InmuebleProfile());
            });

            return config.CreateMapper();
        }
    }
}
