using AutoMapper;
using Database;
using Database.Model;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repository.UserRepository;
using Service.DtoModels.UserModel;
using Service.UserService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Service.ServiceConfiguration;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;

namespace Tests.Service
{
    public class UserServiceTest
    {
        private readonly IMapper _mapper;

        public UserServiceTest()
        {
            _mapper = AutoMapperConfig.GetIMapper();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public async Task Get_User_RetunIdItem_ReturnBeNull(int id)
        {
            List<User> items = GetUsersDb(10);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSedd(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            DtoUserGet result = await userService.Get(id);
            if (id == 0)
                result.ShouldBeNull();
            else
                result.Id.Should().Be(id);

        }


        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetList_User_ReturnSize(int size)
        {
            List<User> items = GetUsersDb(size);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSedd(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            List<DtoUserGet> result = await userService.GetList();

            result.Should().HaveCount(size);
        }

        [Theory]
        [InlineData("test", 1, "email", "password1", true, 1)]
        [InlineData("test", 1, null, "password1", true, 1)]
        public async Task Add_User_ReturnId_ReturnException(string userName, int yearsOld, string email, string password, bool active, int idRol)
        {
            List<User> items = GetUsersDb(10);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSedd(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            if (email == null)
            {
                await Assert.ThrowsAnyAsync<DbUpdateException>(() => userService.AddAsync(new DtoUserAdd
                {
                    UserName = userName,
                    YearsOld = yearsOld,
                    Email = email,
                    Password = password,
                    Active = active,
                    IdRol = idRol
                }));
            }
            else
            {
                int? id = await userService.AddAsync(new DtoUserAdd
                {
                    UserName = userName,
                    YearsOld = yearsOld,
                    Email = email,
                    Password = password,
                    Active = active,
                    IdRol = idRol
                });

                id.Should().NotBeNull();
            }
        }

        [Theory]
        [InlineData(1, "testpass", "email@email")]
        [InlineData(2, "testpass1111", "email1@email1")]
        [InlineData(0, "testpass2222", "email0@email0")]
        public async Task Update_User_ReturnPassword_ReturnNull(int id, string password, string email)
        {
            List<User> items = GetUsersDb(2);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSedd(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            int? result = await userService.UpdateAsync(new DtoUserUpdate { Id = id, Password = password , Email = email});

            if(id == 0)
                result.Should().BeNull();
            else
                context.User.Find(id).Password.Should().Be(password);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public async Task Delete_User_Return(int id)
        {
            List<User> items = GetUsersDb(2);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSedd(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            int? result = await userService.Delete(id);

            if(id == 0)
                result.Should().BeNull();
            else
                result.Should().NotBeNull();
        }


        #region Private Methods

        private IUserService InjectUserService(DemoContext context)
        {
            IUserRepository userRepository = new UserRepository(context);

            IUserService userService = new UserService(userRepository, _mapper);

            return userService;
        }

        private List<User> GetUsersDb(int size)
        {
            List<User> result = Builder<User>.CreateListOfSize(size)
                                             .All()
                                             .With(u => u.RolNavigation = Builder<Rol>.CreateNew().Build())
                                             .Build() as List<User>;

            if(result != null)
            {
                foreach(var item in result)
                {
                    item.RolNavigation.Id = item.IdRol;
                }
            }


            return result;
        }


        #endregion
    }
}
