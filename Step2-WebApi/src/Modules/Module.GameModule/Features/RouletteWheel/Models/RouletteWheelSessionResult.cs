using Module.GameModule.Enums;

namespace Module.GameModule.Features.RouletteWheel.Models;

public record RouletteWheelSessionResult(
Guid Id ,
SessionState State ,
DateTime StartTime,
DateTime? EndTime,
int? SelectedNumber ,
PocketColor? PocketColor);