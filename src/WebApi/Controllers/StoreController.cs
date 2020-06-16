using FoodOnline.Domain;
using FoodOnline.Domain.Stores.Queries;
using FoodOnline.Domain.Stores.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodOnline.WebApi.Controllers
{
    public class StoreController : ApiController
    {
        [Authorize(Policy = Policy.Staff)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterStore register)
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

        [Authorize(Policy = Policy.StaffAndOwner)]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] UpdateStore update)
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

        [Authorize(Policy = Policy.StaffAndCustomer)]
        [HttpPost("open")]
        public async Task<IActionResult> UpdateOpenStore([FromForm] UpdateOpenStore update)
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

        [Authorize(Policy = Policy.StaffAndOwner)]
        [HttpPost("publish")]
        public async Task<IActionResult> UpdatePublishStore([FromForm] UpdatePublishStore update)
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

        [Authorize(Policy = Policy.Staff)]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromForm] DeleteStore delete)
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

        [Authorize(Policy = Policy.StaffAndOwner)]
        [HttpGet("ownerstores/{id}")]
        public async Task<IActionResult> GetOwnerStores(string id)
        {
            try
            {
                if (User.GetRole() != Role.Admin && User.GetSubject() != id)
                {
                    return BadRequestWithMessage("You can't access this resource!");
                }
                var result = await Send(new GetOwnerStores { OwnerId = id });
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(string id)
        {
            try
            {
                var result = await Mediator.Send(new GetStore { Id = id });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequestWithMessage(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStores([FromQuery] GetStores get)
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