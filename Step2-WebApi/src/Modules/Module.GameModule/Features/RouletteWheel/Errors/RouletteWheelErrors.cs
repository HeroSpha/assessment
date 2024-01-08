namespace Module.GameModule.Features.RouletteWheel.Errors;
using ErrorOr;

public static class RouletteWheelErrors
{
    public static class RouletteWheelState
    {
        public static Error RouletteWheelSessionState => Error.Conflict("RouletteSession.InvalidSessionState", "Invalid roulette wheel session state");
    }
}