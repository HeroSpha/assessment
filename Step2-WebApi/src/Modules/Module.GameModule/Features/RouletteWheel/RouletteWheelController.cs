using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Module.GameModule.Features.RouletteWheel.Models;
using Module.SharedModule.Controllers;

namespace Module.GameModule.Features.RouletteWheel;
[Route("/api/roulettewheel/sessions")]
public class RouletteWheelController : BaseController
{
    private readonly ISender _sender;

    public RouletteWheelController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("prepare")]
    public async Task<IActionResult> Prepare()
    {
        var command = new CreateRouletteWheelSession.RouletteWheelSessionCommand();
        var response = await _sender.Send(command).ConfigureAwait(false);
        return response.Match(
            Ok,
            Problem);
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> GetSession()
    {
        var query = new QueryRouletteWheel.GetRouletteWheelSession();
        var response = await _sender.Send(query).ConfigureAwait(false);
        return response.Match(
            Ok,
            Problem);
    }

    [HttpPost("spin")]
    public async Task<IActionResult> Spin(UpdateRouletteWheelSessionRequest input)
    {
        var command = input.Adapt<UpdateRouletteWheelSession.UpdateRouletteWheelSessionCommand>();
        var response = await _sender.Send(command).ConfigureAwait(false);
        return response.Match(
            Ok,
            Problem);
    }
}