using FoodOnline.Domain;
using FoodOnline.Domain.Users.Queries;
using FoodOnline.Domain.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodOnline.WebApi.Controllers
{
    [Authorize(Roles = Role.User)]
    public class UserController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            if (User.TryGetSubject(out string id))
            {
                var result = await Mediator.Send(new GetUser { Id = id });
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser update)
        {
            try
            {
                await Mediator.Send(update);
                return Ok();
            }
            catch (System.Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            if (User.TryGetSubject(out string id))
            {
                try
                {
                    await Mediator.Send(new DeleteUser { Id = id });
                }
                catch (System.Exception e)
                {
                    return BadRequestWithMessage(e.Message);
                }
                return Ok();
            }
            return NotFound();
        }

        //[Authorize(Roles = Roles.Admin), HttpGet("all")]
        //public async Task<ActionResult> GetAll()
        //{
        //    return null;
        //}

        //[Authorize(Roles = Roles.Admin), HttpGet("get/{id}")]
        //public async Task<ActionResult> GetById([FromBody] GetUser get)
        //{
        //    return null;
        //}
    }
}