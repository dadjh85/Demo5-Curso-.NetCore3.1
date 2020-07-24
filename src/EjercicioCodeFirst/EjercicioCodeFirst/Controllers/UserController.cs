using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DtoModels.UserModel;
using Service.UserService;

namespace EjercicioCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "DefaultCache")]
        public async Task<IActionResult> Get(int id)
        {
           DtoUserGet result = await _userService.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            List<DtoUserGet> result = await _userService.GetList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]DtoUserAdd item)
        {
            int? id = await _userService.AddAsync(item);

            return CreatedAtAction(nameof(Get), new { id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]DtoUserUpdate item)
        {
            await _userService.UpdateAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}
