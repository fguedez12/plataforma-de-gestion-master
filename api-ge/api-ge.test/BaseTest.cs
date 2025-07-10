using api_gestiona;
using api_gestiona.Entities;
using api_gestiona.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_ge.test
{
    public class BaseTest
    {
        protected ApplicationDbContext BuildDataBaseContext(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(name).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        protected IMapper ConfigAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new Profiles());
            });

            return config.CreateMapper();
        }

        protected UserManager<Usuario> BuildUserManager()
        {
            var mockUserManager = new Mock<UserManager<Usuario>>(new Mock<IUserStore<Usuario>>()
                .Object, new Mock<IOptions<IdentityOptions>>()
                .Object, new Mock<IPasswordHasher<Usuario>>()
                .Object, new IUserValidator<Usuario>[0],
                    new IPasswordValidator<Usuario>[0],
                    new Mock<ILookupNormalizer>()
                .Object, new Mock<IdentityErrorDescriber>()
                .Object, new Mock<IServiceProvider>()
                .Object, new Mock<ILogger<UserManager<Usuario>>>().Object);

            return mockUserManager.Object;
        }

        protected Servicio BuildObjService(string name)
        {
            var service = new Servicio
            {
                Nombre = name,
                InstitucionId = 1,
                Active = true,
                ReportaPMG = false,
                RevisionRed = true,

            };

            return service;
        }

    }
}
