using Module.GameModule.Contracts;
using Module.GameModule.Enums;
using Module.SharedModule.Common;

namespace Module.GameModule.BettingFactory;

public class BetTypeFactory : IBetTypeFactory
{
    public IBetType CreateBetType(BetType type)
    {
        ArgumentNullException.ThrowIfNull(type);
        return type switch
        {
            BetType.Single => new SingleBetType(),
            BetType.Or => new OrBetType(),
            BetType.FirstRange => new OrBetType(),
            BetType.SecondRange => new OrBetType(),
            BetType.ThirdRange => new OrBetType(),
            BetType.Color => new ColorBetType(),
            BetType.FirstHalfRange => new OrBetType(),
            BetType.SecondHalfRange => new OrBetType(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}