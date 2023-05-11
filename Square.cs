namespace Tick_Tack_Toe;

using static Console;

public class Square
{
    public Square(int x, int y, bool isSelected, bool? isUser)
    {
        X = x;
        Y = y;
        IsSelected = isSelected;
        IsUser = isUser;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public bool IsSelected { get; set; }
    public bool? IsUser { get; set; }

    public void Draw()
    {
        var sqXorO = IsUser switch
        {
            null => "  ",
            true => "👻",
            false => "🤖"
        };

        const string sqTop = "┌────┐";
        var sqMid = $"│ {sqXorO} │";
        const string sqBot = "└────┘";


        //string[] sq = { "┌─────┐", "│     │", "│     │", "└─────┘" };
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