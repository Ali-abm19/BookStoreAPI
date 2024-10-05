using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BookStore.src.Entity;
using BookStore.src.Services.user;
using BookStore.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static BookStore.src.DTO.UserDTO;

namespace BookStore.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserReadDto>>> GetAll()
        {
            var userList = await _userService.GetAllAsync();
            return Ok(userList);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserReadDto>> GetById([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateOne(Guid id, UserUpdateDto updateDto)
        {
            var userUpdatedById = await _userService.UpdateOneAsync(id, updateDto);
            if (!userUpdatedById)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteOne(Guid id)
        {
            var userDelete = await _userService.DeleteOneAsync(id);
            if (!userDelete)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("signUp")]
        public async Task<ActionResult<UserReadDto>> CreateOne([FromBody] UserCreateDto createDto)
        {
            var UserCreated = await _userService.CreateOneAsync(createDto);
            return Ok(UserCreated);
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<string>> SignInUser([FromBody] UserSigninDto createDto)
        {
            var token = await _userService.SignInAsync(createDto);
            if (token == "Not Found")
            {
                return NotFound();
            }
            else if (token == "Unauthorized")
            {
                return Unauthorized();
            }
            else
                return Ok(token);
        }
    }
}
