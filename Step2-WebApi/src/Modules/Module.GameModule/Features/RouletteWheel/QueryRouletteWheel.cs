using ErrorOr;
using FluentValidation;
using Mapster;
using MediatR;
using Module.GameModule.Entities;
using Module.GameModule.Enums;
using Module.GameModule.Features.RouletteWheel.Errors;
using Module.GameModule.Features.RouletteWheel.Models;

namespace Module.GameModule.Features.RouletteWheel;

public static class QueryRouletteWheel
{
    public record GetRouletteWheelSession() : IRequest<ErrorOr<GetRouletteWheelSessionResult>>;
    
    internal class QueryRouletteWheelQueryHandler : IRequestHandler<GetRouletteWheelSession, ErrorOr<GetRouletteWheelSessionResult>>
    {
        public async Task<ErrorOr<GetRouletteWheelSessionResult>> Handle(GetRouletteWheelSession request, CancellationToken cancellationToken)
        {
            await Task.Delay(100, cancellationToken).ConfigureAwait(true);
            var session = RouletteWheelSession.Instance;
            if (session.State is SessionState.Initial or SessionState.Result)
            {
                session.Initialize();
            }
            return session.Adapt<GetRouletteWheelSessionResult>();
        }
    }
    
}