using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.WebApi.Controllers
{
    /// <summary>
    /// A controller that already defines the route to "api/[controller]" and has an
    /// <see cref="IMediator"/> parameter to use when needed.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Creates a that generates a status code 400 of BadRequest and passes the message
        /// in a "new { message };" object.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ActionResult BadRequestWithMessage(string message)
        {
            return BadRequest(new { message });
        }

        protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Mediator.Send(request, cancellationToken);
        }

        protected Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Mediator.Send(request, cancellationToken);
        }

        // protected async Task<(TResponse response, object error)> TrySend<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        // {
        //     try
        //     {
        //         return (await Mediator.Send(request, cancellationToken), null);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         return (null, new { message = ex.Message });
        //     }
        // }

        // protected Task<object> TrySend(object request, CancellationToken cancellationToken = default)
        // {
        //     return Mediator.Send(request, cancellationToken);
        // }
    }
}