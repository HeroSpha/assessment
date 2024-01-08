using MediatR;
using ErrorOr;
using FluentValidation;
using Mapster;
using Module.GameModule.Entities;
using Module.GameModule.Enums;
using Module.GameModule.Features.BetPlacements.Models;
using Module.GameModule.Features.RouletteWheel.Errors;

namespace Module.GameModule.Features.BetPlacements;

public static class CreateBetPlacement
{
    public record BetPlacementCommand(
        Guid Id,
        decimal Amount,
        BetType BetType,
        int Quantity,
        string SelectedBet,
        bool IsSelected,
        Guid RouletteSessionId) : IRequest<ErrorOr<SessionBettingResult>>;

    public class BetPlacementCommandHandler : IRequestHandler<BetPlacementCommand, ErrorOr<SessionBettingResult>>
    {
        public async Task<ErrorOr<SessionBettingResult>> Handle(BetPlacementCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var rouletteWheelSession = RouletteWheelSession.Instance;
            
            if (rouletteWheelSession.State != SessionState.Betting)
            {
                return RouletteWheelErrors.RouletteWheelState.RouletteWheelSessionState;
            }

            var sessionBetting = SessionBet.Create(
                request.Amount,
                request.BetType,
                request.Quantity,
                rouletteWheelSession.Id,
                request.SelectedBet);
            
            rouletteWheelSession.PlaceBet(sessionBetting);
            
            return sessionBetting.Adapt<SessionBettingResult>();

        }
    }
    
    public class BetPlacementCommandValidator : AbstractValidator<BetPlacementCommand>
    {
        public BetPlacementCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero");
            
            RuleFor(x => x.RouletteSessionId)
                .NotNull()
                .WithMessage("Session id must not be null");
            
            RuleFor(x => x.SelectedBet)
                .NotNull()
                .NotEmpty()
                .WithMessage("Selected bet must not be empty.");
            
            RuleFor(x => x.IsSelected)
                .Must(x => x);
        }
    }
    
}