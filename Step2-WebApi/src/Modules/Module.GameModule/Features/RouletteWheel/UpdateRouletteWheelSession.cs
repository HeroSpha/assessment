using System.ComponentModel;
using Dapper;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.GameModule.Abstractions;
using Module.GameModule.Contracts;
using Module.GameModule.Entities;
using Module.GameModule.Enums;
using Module.GameModule.Features.RouletteWheel.Errors;
using Module.GameModule.Features.RouletteWheel.Models;
using Module.GameModule.Persistence;
using Module.SharedModule.Common;

namespace Module.GameModule.Features.RouletteWheel;

public static class UpdateRouletteWheelSession
{
    public record UpdateRouletteWheelSessionCommand(
        SessionState State) : IRequest<ErrorOr<RouletteWheelSessionResult>>;
    
    internal class UpdateRouletteWheelSessionCommandHandler : IRequestHandler<UpdateRouletteWheelSessionCommand, ErrorOr<RouletteWheelSessionResult>>
    {
        private readonly IBetTypeFactory _betTypeFactory;

        private readonly RouletteWheelDbContext _rouletteWheelDbContext;
        public UpdateRouletteWheelSessionCommandHandler(IBetTypeFactory betTypeFactory, RouletteWheelDbContext rouletteWheelDbContext)
        {
            _betTypeFactory = betTypeFactory;
            _rouletteWheelDbContext = rouletteWheelDbContext;
        }

        public async Task<ErrorOr<RouletteWheelSessionResult>> Handle(UpdateRouletteWheelSessionCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var rouletteWheelSession = RouletteWheelSession.Instance;
            switch (request.State)
            {
                case SessionState.Result:
                {
                    if (rouletteWheelSession.State != SessionState.Spinning)
                    {
                        return RouletteWheelErrors.RouletteWheelState.RouletteWheelSessionState;
                    }
                    
                    rouletteWheelSession.EndSession();

                    ProcessResult(rouletteWheelSession);
                }
                    break;
                case SessionState.Spinning:
                {
                    if (rouletteWheelSession.State != SessionState.Betting)
                    {
                        return RouletteWheelErrors.RouletteWheelState.RouletteWheelSessionState;
                    }
            
                    rouletteWheelSession.Spin();
                }
                    break;
                default:
                    return  RouletteWheelErrors.RouletteWheelState.RouletteWheelSessionState;
            }

           

            return rouletteWheelSession.Adapt<RouletteWheelSessionResult>();
        }

        private async void ProcessResult(RouletteWheelSession rouletteWheelSession)
        {
            try
            {
                var selectedPocket = rouletteWheelSession.GetWinningPocket();

                var groupedBets = from bet in rouletteWheelSession.SessionBets
                    group bet by bet.BetType;
                foreach (var bet in groupedBets)
                {
                    switch (bet.Key)
                    {
                        case BetType.Single:
                        {
                            ProcessSingleWinnings(bet, selectedPocket);
                        }
                            break;
                        case BetType.Color:
                        {
                            ProcessColorWinnings(bet, selectedPocket);
                        }
                            break;
                    }
                }

                await SubmitRouletteWheelSession(rouletteWheelSession).ConfigureAwait(false);
                await SubmitWinnings(rouletteWheelSession).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task SubmitWinnings(RouletteWheelSession rouletteWheelSession)
        {
            await using var connection = _rouletteWheelDbContext.Database.GetDbConnection();
            await connection.OpenAsync().ConfigureAwait(false);
            var query =
                @"INSERT INTO SessionBets(Id,Amount,WinAmount,BetType,Quantity, SelectedBet,IsSelected,RouletteSessionId) VALUES(@Id,@Amount,@WinAmount,@BetType,@Quantity,@SelectedBet,@IsSelected,@RouletteSessionId);";

            foreach (var bet in rouletteWheelSession.SessionBets)
            {
                await connection.QueryAsync(query, bet).ConfigureAwait(false);
            }

            await connection.CloseAsync().ConfigureAwait(false);
        }

        private async Task SubmitRouletteWheelSession(RouletteWheelSession rouletteWheelSession)
        {
            try
            {
                await using var connection = _rouletteWheelDbContext.Database.GetDbConnection();
                await connection.OpenAsync().ConfigureAwait(false);
                var query =
                    @"INSERT INTO RouletteWheelSessions(Id,State,StartTime,EndTime,SelectedNumber, PocketColor) VALUES(@Id,@State,@StartTime,@EndTime,@SelectedNumber,@PocketColor);";
                await connection.QueryAsync(query, rouletteWheelSession).ConfigureAwait(false);
                await connection.CloseAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void ProcessColorWinnings(IGrouping<BetType, SessionBet> bet, Pocket selectedPocket)
        {
            var betType = _betTypeFactory.CreateBetType(bet.Key);
            foreach (var sessionBet in bet.AsEnumerable().Where(x => x.SelectedBet == selectedPocket.Color.ToString()))
            {
                var winAmount = (decimal)betType.BettingValue * (sessionBet.Amount * sessionBet.Quantity);
                sessionBet.Win(winAmount);
            }
        }

        private void ProcessSingleWinnings(IGrouping<BetType, SessionBet> bet, Pocket selectedPocket)
        {
            var betType = _betTypeFactory.CreateBetType(bet.Key);
            foreach (var sessionBet in bet.AsEnumerable()
                         .Where(x => int.Parse(x.SelectedBet) == selectedPocket.Number).ToList())
            {
                var winAmount = (decimal)betType.BettingValue * (sessionBet.Amount * sessionBet.Quantity);
                sessionBet.Win(winAmount);
            }
        }
    }
}