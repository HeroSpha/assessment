using Module.GameModule.Entities;
using Module.GameModule.Enums;

namespace Module.GameModule.Features.RouletteWheel.Models;

public record GetRouletteWheelSessionResult(
    Guid Id,
    SessionState State,
    DateTime StartTime,
    IEnumerable<SessionBettingResult> SessionBets,
    DateTime? EndTime,
    int? SelectedNumber,
    PocketColor? PocketColor);