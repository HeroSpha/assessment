using Module.GameModule.Enums;

namespace Module.GameModule.Entities;

public class Pocket
{
    public int Number{ get; private set; }
    public PocketColor Color { get; private set; }

    public Pocket(int number, PocketColor color)
    {
        Number = number;
        Color = color;
    }
}