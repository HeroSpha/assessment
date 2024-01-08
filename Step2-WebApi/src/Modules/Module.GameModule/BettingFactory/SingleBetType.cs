using Module.GameModule.Enums;

namespace Module.GameModule.BettingFactory;

public class SingleBetType : IBetType
{
    public double BettingValue { get; set; }
    public BetType BetType { get; set; }

    public SingleBetType()
    {
        BettingValue = 20;
        BetType = BetType.Single;
    }
}