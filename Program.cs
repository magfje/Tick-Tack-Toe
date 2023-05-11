using System.Text;
using static System.Console;

namespace Tick_Tack_Toe;

internal class Program
{
    private static void Main(string[] args)
    {
        OutputEncoding = Encoding.UTF8;

        //Make more dynamic with nested loop?
        var square00 = new Square(0, 0, true, null);
        var square01 = new Square(6, 0, false, null);
        var square02 = new Square(12, 0, false, null);
        var square10 = new Square(0, 3, false, null);
        var square11 = new Square(6, 3, false, null);
        var square12 = new Square(12, 3, false, null);
        var square20 = new Square(0, 6, false, null);
        var square21 = new Square(6, 6, false, null);
        var square22 = new Square(12, 6, false, null);

        var array = new Square[3, 3]
        {
            { square00, square01, square02 },
            { square10, square11, square12 },
            { square20, square21, square22 }
        };
        var x = 0;
        var y = 0;
        ConsoleKey keyPressed;
        do
        {
            foreach (var a in array)
            {
                a.Draw();
                a.IsSelected = false;
            }

            //DisplayOptions();
            var keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;

            //update SelectedIndex based on arrow keys
            if (keyPressed == ConsoleKey.LeftArrow)
            {
                x--;
                if (x == -1) x = 2;
            }
            else if (keyPressed == ConsoleKey.RightArrow)
            {
                x++;
                if (x == 3) x = 0;
            }
            else if (keyPressed == ConsoleKey.UpArrow)
            {
                y--;
                if (y == -1) y = 2;
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                y++;
                if (y == 3) y = 0;
            }
            else if (keyPressed == ConsoleKey.Enter)
            {
                array[y, x].UserPick();
                BotPicker();
            }
            else if (keyPressed == ConsoleKey.M)
            {
                array[y, x].BotPick();
            }

            if (array[y, x].IsSelected == false)
                array[y, x].IsSelected = true;
            //else
            //    array[y, x].IsSelected = false;

            Clear();
        } while (keyPressed != ConsoleKey.Escape);

        void BotPicker()
        {
            var y = new Random().Next(0, 2);
            var x = new Random().Next(0, 2);
            if (array[y, x].IsUser == null) array[y, x].BotPick();
        }
        //var a = new Square(0, 1, false, null);
        //a.Draw();
        //var b = new Square(6, 1, false, null);
        //b.Draw();
    }
}