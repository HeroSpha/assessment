using MediatR;
using ErrorOr;
using Mapster;
using Module.GameModule.Entities;
using Module.GameModule.Enums;
using Module.GameModule.Features.RouletteWheel.Models;

namespace Module.GameModule.Features.RouletteWheel;

public static class CreateRouletteWheelSession
{
    public record RouletteWheelSessionCommand() : IRequest<ErrorOr<RouletteWheelSessionResult>>;
    
    internal class  RouletteWheelSessionCommandHandler : IRequestHandler<RouletteWheelSessionCommand, ErrorOr<RouletteWheelSessionResult>>
    {
        public async Task<ErrorOr<RouletteWheelSessionResult>> Handle(RouletteWheelSessionCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
            
            var rouletteWheel = RouletteWheelSession.Instance;
            if (rouletteWheel.State is SessionState.Initial or SessionState.Result)
            {
                rouletteWheel.Initialize();
            }
            return rouletteWheel.Adapt<RouletteWheelSessionResult>();
        }
    }
}