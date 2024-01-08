using Module.GameModule.BettingFactory;
using Module.GameModule.Enums;

namespace Module.GameModule.Contracts;

public interface IBetTypeFactory
{
    IBetType CreateBetType(BetType type);
}