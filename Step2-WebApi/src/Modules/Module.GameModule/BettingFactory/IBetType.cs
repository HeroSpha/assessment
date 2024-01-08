using Module.GameModule.Enums;

namespace Module.GameModule.BettingFactory;

public interface IBetType
{
    public double BettingValue { get; set; }
    public BetType BetType { get; set; }  
    
}