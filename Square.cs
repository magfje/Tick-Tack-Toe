namespace Tick_Tack_Toe;

using static Console;

public class Square
{
    public Square(int x, int y, bool isSelected)
    {
        X = x;
        Y = y;
        IsSelected = isSelected;
    }

    private int X { get; }
    private int Y { get; }
    public bool IsSelected { get; set; }
    public bool? IsUser { get; set; }

    public void Draw()
    {
        var pick = IsUser switch
        {
            null => "  ",
            true => "👻",
            false => "🤖"
        };

        const string sqTop = "┌────┐";
        const string sqBot = "└────┘";
        var sqMid = $"│ {pick} │";

        string[] sq = { sqTop, sqMid, sqBot };


        var y = Y;
        foreach (var p in sq)
        {
            if (IsSelected) ForegroundColor = ConsoleColor.Green;

            SetCursorPosition(X, y);
            Write(p);
            y++;
            ResetColor();
        }
    }

    public void UserPick()
    {
        IsUser ??= true;
    }

    public void BotPick()
    {
        IsUser ??= false;
    }
}