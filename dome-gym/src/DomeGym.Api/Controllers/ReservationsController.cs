using DomeGym.Application.Reservations.Commands.CreateReservation;
using DomeGym.Contracts.Reservations;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("sessions/{sessionId:guid}/reservations")]
public class ReservationsController : ApiController
{
    private readonly ISender _sender;

    public ReservationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        CreateReservationRequest request,
        Guid sessionId)
    {
        var command = new CreateReservationCommand(
            sessionId,
            request.ParticipantId);

        var createReservationResult = await _sender.Send(command);

        return createReservationResult.Match(_ => NoContent(), Problem);
    }
}