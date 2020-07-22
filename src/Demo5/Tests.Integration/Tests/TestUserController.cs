using AutoMapper;
using Database.Model;
using Demo5.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
    [Collection(ServerFixture.Demo5ServerFixture)]
    public class TestUserController
    {
        private readonly ServerFixture _serverFixture;
        private readonly IMapper mapperConfig;
        private readonly UserRepositoryTest _userRepository;
        private readonly RolRepositoryTest _rolRepositoryTest;

        private readonly string BASE_URI = "api/user";

        public TestUserController(ServerFixture serverFixture)
        {

            _serverFixture = serverFixture;
            mapperConfig = AutoMapperConfig.GetIMapper();
            _userRepository = new UserRepositoryTest(serverFixture);
            _rolRepositoryTest = new RolRepositoryTest(serverFixture);
        }

        [Theory]
        [InlineData("test", "email@email.com", "1234", true)]
        [InlineData("test2", "email", "1", true)]
        [ResetDatabase]
        public async Task PostUserAsync_201(string name, string email, string password, bool isActive)
        {
            Rol rol = await  AddRol();
            DtoUserAdd item = GetDtoUserAdd(name, email, password, isActive, rol.Id);

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(cont => cont.Add(item))
                                                                          .PostAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("54321")]
        [ResetDatabase]
        public async Task PutUserAsync_204(string password)
        {
            Rol rol = await AddRol();
            User user = await AddUser(rol.Id);

            DtoUserUpdate itemToUpdate = GetDtoUserUpdate(user.Id, password);
            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(cont => cont.Update(itemToUpdate))
                                                                          .SendAsync("PUT");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        [ResetDatabase]
        public async Task DeleteAsync_204()
        {
            Rol rol = await AddRol();
            User user = await AddUser(rol.Id);

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(cont => cont.Delete(user.Id))
                                                                          .SendAsync("DELETE");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        [Fact]
        [ResetDatabase]
        public async Task GetAsync_200()
        {
            Rol rol = await AddRol();
            User user = await AddUser(rol.Id);

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(cont => cont.Get(user.Id))
                                                                          .GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DtoUserGet>(json); 

            result.Id.Should().Be(user.Id);
        }

        [Fact]
        [ResetDatabase]
        public async Task GetListAsync_200()
        {
            Rol rol = await AddRol();
            await AddUser(rol.Id);

            HttpResponseMessage response = await _serverFixture.TestServer.CreateHttpApiRequest<UserController>(cont => cont.GetList())
                                                                          .GetAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<DtoUserGet>>(json);

            result.Count.Should().Be(1);
        }


        #region Private methods

        private async Task<User> AddUser(int idRol)
        {
            return await _userRepository.Add(new User
            {
                Name = "test", 
                Email = "emailtest",
                Password = "pass1",
                IsActive = true,
                IdRol = idRol
            });
        }

        private async Task<Rol> AddRol()
        {
            return await _rolRepositoryTest.Add(new Rol { Name = "Test" });
        }

        private DtoUserUpdate GetDtoUserUpdate(int id, string password)
        {
            return new DtoUserUpdate
            {
                Id = id,
                Password = password
            };
        }

        private DtoUserAdd GetDtoUserAdd(string name, string email, string password, bool isActive, int idRol)
        {
            return new DtoUserAdd
            {
                Name = name,
                Email = email,
                Password = password,
                IsActive = isActive,
                IdRol = idRol
            };
        }

        #endregion
    }
}
