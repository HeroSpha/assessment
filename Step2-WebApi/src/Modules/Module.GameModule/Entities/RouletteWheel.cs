using Module.GameModule.Enums;
using Timer = System.Timers.Timer;

namespace Module.GameModule.Entities;

public class RouletteWheel
{
    private static RouletteWheel? _instance;
    public HashSet<Pocket> Pockets{get; }
    
    private RouletteWheel()
    {
        Pockets = new HashSet<Pocket>();
        InitializePockets();
    }

    public static RouletteWheel Instance
    {
        get { return _instance ??= new RouletteWheel(); }
    }

    private void InitializePockets()
    {
        Pockets.Add(new Pocket(0, PocketColor.Green));
        for (var i = 1; i <= 36; i++)
        {
            var pocket = new Pocket(i, i % 2 == 0 ? PocketColor.Red : PocketColor.Black);
            Pockets.Add(pocket);
        }
    }
}