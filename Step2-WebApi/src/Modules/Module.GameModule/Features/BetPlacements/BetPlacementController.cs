using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Module.GameModule.Features.BetPlacements.Models;
using Module.SharedModule.Controllers;

namespace Module.GameModule.Features.BetPlacements;
[Route("/api/roulettewheel/bettings")]
public class BetPlacementController : BaseController
{
    private readonly ISender _sender;
    // ReSharper disable once ConvertToPrimaryConstructor
    public BetPlacementController(ISender sender)
    {
        _sender = sender;
    }
    [HttpPost("place")]
    public async Task<IActionResult> Create(SessionBettingRequest request)
    {
        var command = request.Adapt<CreateBetPlacement.BetPlacementCommand>();
        var response = await _sender.Send(command).ConfigureAwait(false);
        return response.Match(
            Ok,
            Problem);
    }
}