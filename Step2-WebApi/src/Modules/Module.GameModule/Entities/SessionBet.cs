using System.ComponentModel.DataAnnotations.Schema;
using Module.GameModule.Enums;

namespace Module.GameModule.Entities;

public class SessionBet
{
    public Guid Id { get; private  set; }
    public decimal  Amount { get; private set; }
    public decimal?  WinAmount { get; private set; }
    public BetType BetType { get; private set; }
    public int Quantity { get; private set; }
    public string SelectedBet { get; private set; }
    public bool IsSelected { get; private set; }
    public Guid RouletteSessionId { get; private set; }
    [ForeignKey(nameof(RouletteSessionId))]
    public virtual RouletteWheelSession RouletteWheelSession { get; private set;}

    private SessionBet(decimal amount, BetType betType, int quantity, Guid rouletteSessionId, string selectedBet)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        BetType = betType;
        Quantity = quantity;
        RouletteSessionId = rouletteSessionId;
        SelectedBet = selectedBet;
        WinAmount = null;
    }

    public static SessionBet Create(decimal amount, BetType betType, int quantity, Guid rouletteSessionId, string selectedBet)
    {
        return new(amount, betType, quantity, rouletteSessionId, selectedBet);
    }

    public void Win(decimal winAmount)
    {
        IsSelected = true;
        WinAmount = winAmount;
    }
}