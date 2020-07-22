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
        #region Properties

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UserServiceTest()
        {
            _mapper = AutoMapperConfig.GetIMapper();
        }

        #endregion

        #region Tests Methods 

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public async Task GetList_ReturnSize(int size)
        {
            List<User> items = GetUserDB(size);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSeed(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            List<DtoUserGet> result = await userService.GetList();

            result.Should().HaveCount(size);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public async Task Get_ReturnIdItemAndBeNull(int id)
        {
            List<User> items = GetUserDB(20);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSeed(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            DtoUserGet result = await userService.Get(id);

            if (id == 0)
                result.ShouldBeNull();
            else
                result.Id.Should().Be(id);
        }

        [Theory]
        [InlineData("test", "email", "123", true, 1)]
        [InlineData("test2", null, "123", true, 1)]
        [InlineData("test", "email largo largo largo largo largo", "123", true, 1)]
        public async Task Add_ReturnNotBeNullAndException(string name, string email, string password, bool isActive, int idRol)
        {
            List<User> items = GetUserDB(20);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSeed(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            if (email == null)
                await Assert.ThrowsAsync<DbUpdateException>(async () => await userService.Add(new DtoUserAdd
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    IsActive = isActive,
                    IdRol = idRol
                }));
            else
            {
                int? id = await userService.Add(new DtoUserAdd
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    IsActive = isActive,
                    IdRol = idRol
                });

                id.Should().NotBeNull();
            }
        }

        [Theory]
        [InlineData(1, "1234")]
        [InlineData(2, "1234566222")]
        [InlineData(0, "1234566222")]
        public async Task Update_ReturnPasswordAndBeNull(int id, string password)
        {
            List<User> items = GetUserDB(2);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSeed(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);
            int? result = await userService.Update(new DtoUserUpdate { Id = id, Password = password });
            if (id == 0)
                result.Should().BeNull();
            else
                context.User.Find(id).Password.Should().Be(password);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(0)]
        public async Task Delete_ReturnNotBeNullAndBeNull(int id)
        {
            List<User> items = GetUserDB(2);

            DbContextOptions<DemoContext> options = SqliteInMemory.CreateOptions<DemoContext>();
            using DemoContext context = new DemoContext(options);
            await ContextConfig.InitializeDatabaseContextSeed(context);
            await ContextConfig.AddDatabaseContext(context, items);

            IUserService userService = InjectUserService(context);

            int? rowsAffected = await userService.Delete(id);
            if(id == 0)
                rowsAffected.Should().BeNull();
            else
                rowsAffected.Should().NotBeNull();
        }

        #endregion

        

        #region Private Methods

        private IUserService InjectUserService(DemoContext context)
        {
            IUserRepository userRepository = new UserRepository(context);

            IUserService service = new UserService(userRepository, _mapper);

            return service;
        }

        private List<User> GetUserDB(int size)
        {
            List<User> result = Builder<User>.CreateListOfSize(size)
                                             .All()
                                             .With(u => u.RolNavigation = Builder<Rol>.CreateNew().Build())
                                             .Build() as List<User>;

            if (result != null)
            {
                foreach (var item in result)
                {
                    item.IsActive = true;
                    item.RolNavigation.Id = item.IdRol;
                }
            }

            return result;
        }
        #endregion
    }
}
