using Module.GameModule.Enums;

namespace Module.GameModule.Entities;

public class RouletteWheelSession
{
    private static RouletteWheelSession? _instance;

    private RouletteWheelSession()
    {
        State = SessionState.Initial;
        StartTime = DateTime.UtcNow;
    }

    public static RouletteWheelSession Instance => _instance ??= new RouletteWheelSession();
    public Guid Id { get; private set; }
    public SessionState State { get; private set; }
    public DateTime StartTime { get;private set; }
    public DateTime? EndTime { get;private set; }
    public int? SelectedNumber { get; private set; }
    public PocketColor? PocketColor { get; private set; }

    public virtual ICollection<SessionBet> SessionBets { get; private set; }
    public void EndSession()
    {
        if (State != SessionState.Spinning)
        {
            throw new InvalidOperationException("State must be a valid state.");
        }

        var rouletteWheel = RouletteWheel.Instance;
        // winning number is always
        var selectedPocket = rouletteWheel.Pockets.First();
        
        State = SessionState.Initial;
        EndTime = DateTime.UtcNow;
        SelectedNumber = selectedPocket.Number;
        PocketColor = selectedPocket.Color;
        
    }

    public Pocket GetWinningPocket()
    {
        return RouletteWheel.Instance.Pockets.First(x => x.Number == SelectedNumber);
    }
    public void Initialize()
    {
        Id = Guid.NewGuid();
        State = SessionState.Betting;
        SessionBets = new HashSet<SessionBet>();
    }

    public void Spin()
    {
        State = SessionState.Spinning;
    }

    public void PlaceBet(SessionBet bet)
    {
        SessionBets.Add(bet);
    }

    public void UpdateWinnings(IEnumerable<SessionBet> bets)
    {
        SessionBets.Clear();
        SessionBets = new List<SessionBet>(bets);
    }
}