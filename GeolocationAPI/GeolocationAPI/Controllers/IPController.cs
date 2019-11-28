using Geolocation.Infrastructure.Commands;
using Geolocation.Infrastructure.Commands.IP;
using Geolocation.Infrastructure.DTO;
using Geolocation.Infrastructure.Queries.IP;
using GeolocationAPI.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GeolocationAPI.Controllers
{
    public class IPController : BaseApiController
    {
        private readonly IMediator _mediator;

        public IPController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get geolocation data based on provided IP parameter
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [Route("{ip}")]
        [HttpGet]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeolocationDataByIP([FromRoute] string ip)
        {
            IPDataValidator validator = new IPDataValidator();
            var result = validator.Validate(new IPDataDTO { IPParameter=ip});

            if (result.IsValid)
            {
                var geolocationData = await _mediator.Send(new GetGeolocationDataByIPQuery(ip));
                return Ok(geolocationData);
            }
            var errotList = result.Errors;
            var errorMessages = "";

            foreach (var failure in errotList)
            {
                errorMessages += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
            }
            return BadRequest(errorMessages);
            
        }

        /// <summary>
        /// Add geolocation data based on IP paramter
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(GeolocationResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddGeolocationDataByIP([FromBody] IPDataDTO ip)
        {
            var geolocationData = await _mediator.Send(new AddIPCommand { IPParameter = ip.IPParameter });
            return Ok(geolocationData);
        }

        /// <summary>
        /// Delete geolocation data based on IP parameter
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteGeolocationDataByIP([FromBody] IPDataDTO ip)
        {
            var result = await _mediator.Send(new DeleteIPCommand(ip.IPParameter));
            return Ok(result);
        }
    }
}
