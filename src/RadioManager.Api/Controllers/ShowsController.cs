using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RadioManager.Application.Reports.Dtos;
using RadioManager.Application.Reports.Queries.GetDaily;
using RadioManager.Application.Shows.Commands.Create;
using RadioManager.Application.Shows.Dtos;
using RadioManager.Application.Shows.Queries.Get;
using RadioManager.Application.Shows.Queries.GetByDate;

namespace RadioManager.Api.Controllers
{
    /// <summary>
    /// Zarządza audycjami.
    /// </summary>
    [ApiController]
    [Route("api/shows")]
    public class ShowsController(
        ISender sender) : ControllerBase
    {
        /// <summary>
        /// Pobiera szczegóły pojedynczej audycji na podstawie ID.
        /// </summary>
        /// <param name="id">Unikalny identyfikator audycji (GUID).</param>
        /// <returns>Szczegółowe informacje o audycji.</returns>
        /// <response code="200">Zwraca szczegóły audycji.</response>
        /// <response code="404">Nie znaleziono audycji o podanym ID.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ShowDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var query = new GetShowQuery(id);
            var result = await sender.Send(query);

            return result.MatchFirst<IActionResult>(
              Ok,
            error => error.Type switch
            {
                ErrorType.NotFound => NotFound(),
                _ => BadRequest()
            });
        }

        /// <summary>
        /// Tworzy nowy seans filmowy.
        /// </summary>
        /// <param name="command">Dane potrzebne do utworzenia nowegj audycji.</param>
        /// <returns>Lokalizację nowo utworzonego zasobu.</returns>
        /// <response code="201">Audycja została pomyślnie utworzona. Zwraca nagłówek 'Location' z adresem do nowego zasobu.</response>
        /// <response code="400">Przesłane dane są nieprawidłowe (np. błędy walidacji).</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateShowCommand command)
        {
            var showId = await sender.Send(command);

            return CreatedAtAction(nameof(Get), new { id = showId }, null);
        }
        /// <summary>
        /// Pobiera listę wszystkich audycji odbywających się w danym dniu.
        /// </summary>
        /// <param name="date">Data w formacie YYYY-MM-DD, dla której mają być wyszukane audycje.</param>
        /// <returns>Lista audycji w danym dniu.</returns>
        /// <response code="200">Zwraca listę audycji.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShowDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        {
            var query = new GetShowsQuery(date);
            var shows = await sender.Send(query);

            return Ok(shows);
        }
        /// <summary>
        /// Generuje dzienny audycji.
        /// </summary>
        /// <param name="date">Data w formacie YYYY-MM-DD, dla której ma być wygenerowany raport.</param>
        /// <returns>Dzienny raport audycji.</returns>
        /// <response code="200">Zwraca wygenerowany raport.</response>
        [HttpGet("daily-reports")]
        [ProducesResponseType(typeof(DailyReportDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDailyReport([FromQuery] DateTime date)
        {
            var query = new GetDailyReportQuery(date);
            var report = await sender.Send(query);

            return Ok(report);
        }
    }
}
