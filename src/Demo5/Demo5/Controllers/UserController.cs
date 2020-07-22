using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.DtoModels.UserModel;
using Service.UserService;

namespace Demo5.Controllers
{
    public class UserController : GenericController
    {
        #region Properties

        private readonly IUserService _userService;

        #endregion

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            List<DtoUserGet> result = await _userService.GetList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            DtoUserGet result = await _userService.Get(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DtoUserAdd item)
        {
            var id = await _userService.Add(item);

            return CreatedAtAction(nameof(Get), new { id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DtoUserUpdate item)
        {
            await _userService.Update(item);
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
