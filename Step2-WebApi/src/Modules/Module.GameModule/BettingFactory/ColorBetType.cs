using Module.GameModule.Enums;

namespace Module.GameModule.BettingFactory;

public class ColorBetType : IBetType
{
    public double BettingValue { get; set; }
    public BetType BetType { get; set; }

    public ColorBetType()
    {
        BettingValue = 50;
        BetType = BetType.Color;
    }
}