using Module.GameModule.Enums;

namespace Module.GameModule.BettingFactory;

public class OrBetType : IBetType
{
    public double BettingValue { get; set; }
    public BetType BetType { get; set; }

    public OrBetType()
    {
        BettingValue = 10;
        BetType = BetType.Or;
    }
}