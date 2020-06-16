using FoodOnline.Domain;
using FoodOnline.Domain.StoreUsers.Queries;
using FoodOnline.Domain.StoreUsers.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodOnline.WebApi.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class StoreUserController : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterStoreUser register)
        {
            try
            {
                await Mediator.Send(register);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] UpdateStoreUser update)
        {
            try
            {
                await Mediator.Send(update);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromForm] DeleteStoreUser delete)
        {
            try
            {
                await Mediator.Send(delete);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetStoreUser([FromQuery] GetStoreUser get)
        {
            try
            {
                var result = await Mediator.Send(get);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }

        [HttpGet("usersforstore")]
        public async Task<IActionResult> GetStoreUsersForStore([FromQuery] GetStoreUsersrForStore get)
        {
            try
            {
                var result = await Mediator.Send(get);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetStoreUsers([FromQuery] GetStoreUsers get)
        {
            try
            {
                var result = await Mediator.Send(get);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }
    }
}