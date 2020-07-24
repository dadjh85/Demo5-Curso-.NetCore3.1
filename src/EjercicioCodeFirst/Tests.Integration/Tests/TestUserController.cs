using Database.Model;
using EjercicioCodeFirst.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Service.DtoModels.UserModel;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tests.Integration.Configuration;
using Tests.Integration.Configuration.Database;
using Tests.Integration.Configuration.Database.Repository;
using Xunit;

namespace Tests.Integration.Tests
{
    [Collection(ServerFixture.EjercicioCodeFirstServerFixture)]
    public class TestUserController
    {
        private readonly ServerFixture _serverFixture;
        private readonly UserRepositoryTest _userRepositoryTest;
        private readonly RolRepositoryTest _rolRepositoryTest;

        public TestUserController(ServerFixture serverFixture)
        {
            _serverFixture = serverFixture;
            _userRepositoryTest = new UserRepositoryTest(serverFixture);
            _rolRepositoryTest = new RolRepositoryTest(serverFixture);
        }

        [Theory]
        [InlineData("test", 20, "email@email.com", "1234", true)]
        [InlineData("test1", 20, "email1@email.com", "1234", true)]
        [ResetDatabase]
        public async Task PostUserAsync_201(string userName, int yarsOld, string email, string password, bool active)
        {
            Rol rol = await AddRolAsync();

            var userAdd = new DtoUserAdd { UserName =  userName , YearsOld = yarsOld , Email = email, Password = password, Active = active, IdRol = rol.Id };

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(c => c.Add(userAdd))
                                                                          .PostAsync();


            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        
        [Theory]
        [InlineData("test111", "email111@email.com")]
        [InlineData("test222", "email222@email.com")]
        [ResetDatabase]
        public async Task PutUserAsync_204(string password, string email)
        {
            Rol rol = await AddRolAsync();
            User user = await AddUserAsync(rol.Id);

            var userToUpdate = new DtoUserUpdate { Id = user.Id, Password = password, Email = email };

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(c => c.Update(userToUpdate))
                                                                          .SendAsync("PUT");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        [ResetDatabase]
        public async Task DeleteUserAsync_204()
        {
            Rol rol = await AddRolAsync();
            User user = await AddUserAsync(rol.Id);

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(c => c.Delete(user.Id))
                                                                          .SendAsync("Delete");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        [ResetDatabase]
        public async Task GetUserAsync_200()
        {
            Rol rol = await AddRolAsync();
            User user = await AddUserAsync(rol.Id);

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(c => c.Get(user.Id))
                                                                          .GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DtoUserGet>(json);

            result.Id.Should().Be(user.Id);
        }

        [Fact]
        [ResetDatabase]
        public async Task GetListUserAsync_200()
        {
            Rol rol = await AddRolAsync();
            await AddUserAsync(rol.Id);

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(c => c.GetList())
                                                                          .GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DtoUserGet>>(json);

            result.Count.Should().Be(1);
        }



        private async Task<Rol> AddRolAsync()
            => await _rolRepositoryTest.Add(new Rol { Name = "Test", Description = "testDescription" });

        private async Task<User> AddUserAsync(int idRol)
        {
            return await _userRepositoryTest.Add(new User
            {
                UserName = "test",
                YearsOld = 20,
                Email = "email@email.com",
                Password = "pass1",
                Active = true,
                IdRol = idRol
            });
        }

    }
}
