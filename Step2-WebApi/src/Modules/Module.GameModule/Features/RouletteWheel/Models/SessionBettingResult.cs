using Module.GameModule.Enums;

namespace Module.GameModule.Features.RouletteWheel.Models;

public record SessionBettingResult(
Guid Id,
decimal  Amount,
BetType BetType,
int Quantity,
string SelectedBet,
bool IsSelected);