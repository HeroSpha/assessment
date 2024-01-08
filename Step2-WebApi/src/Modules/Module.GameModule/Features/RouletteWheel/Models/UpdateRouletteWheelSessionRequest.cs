using Module.GameModule.Enums;

namespace Module.GameModule.Features.RouletteWheel.Models;

public record UpdateRouletteWheelSessionRequest(
    SessionState State);