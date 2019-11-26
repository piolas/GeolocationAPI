using Geolocation.Infrastructure.Commands;
using Geolocation.Infrastructure.Commands.URL;
using Geolocation.Infrastructure.DTO;
using Geolocation.Infrastructure.Queries.URL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GeolocationAPI.Controllers
{
    public class URLController : BaseApiController
    {
        private readonly IMediator _mediator;

        public URLController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get geolocation data based on provided URL parameter
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        [Route("{url}")]
        [HttpGet]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeolocationDataByIP([FromRoute] string url)
        {
            var geolocationData = await _mediator.Send(new GetGeolocationDataByURLQuery(url));

            return Ok(geolocationData);
        }

        /// <summary>
        /// Add geolocation data based on URL paramter
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(GeolocationResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddGeolocationDataByURL([FromBody] URLDataDTO url)
        {
            var geolocationData = await _mediator.Send(new AddURLCommand { URLParameter = url.URLParameter });
            return Ok(geolocationData);
        }

        /// <summary>
        /// Delete geolocation data based on URL parameter
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteGeolocationDataByIP([FromBody] URLDataDTO url)
        {
            var result = await _mediator.Send(new DeleteURLCommand(url.URLParameter));
            return Ok(result);
        }
    }
}
