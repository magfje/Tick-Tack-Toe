using System.Text;
using static System.Console;

namespace Tick_Tack_Toe;

internal class Program
{
    private static void Main()
    {
        OutputEncoding = Encoding.UTF8;

        var squareList = new List<Square>();

        for (var i = 0; i < 9; i++)
        {
            var xPos = i switch
            {
                1 or 4 or 7 => 6,
                2 or 5 or 8 => 12,
                _ => 0
            };

            var yPos = i switch
            {
                < 3 => 0,
                < 6 => 3,
                _ => 6
            };

            var isSel = i == 0;

            var square = new Square(xPos, yPos, isSel);
            squareList.Add(square);
        }

        var gameOver = false;
        var selectedIndex = 0;

        ConsoleKey keyPressed;
        do
        {
            if (!gameOver)
                foreach (var a in squareList)
                {
                    a.Draw();
                    a.IsSelected = false;
                }
            else
                WriteLine("Game OVer!");

            var keyInfo = ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.LeftArrow)
            {
                if (selectedIndex == 0) selectedIndex = 2;
                else if (selectedIndex == 3) selectedIndex = 5;
                else if (selectedIndex == 6) selectedIndex = 8;
                else selectedIndex -= 1;
            }
            else if (keyPressed == ConsoleKey.RightArrow)
            {
                if (selectedIndex == 2) selectedIndex = 0;
                else if (selectedIndex == 5) selectedIndex = 3;
                else if (selectedIndex == 8) selectedIndex = 6;
                else selectedIndex++;
            }
            else if (keyPressed == ConsoleKey.UpArrow)
            {
                if (selectedIndex == 0) selectedIndex = 6;
                else if (selectedIndex == 1) selectedIndex = 7;
                else if (selectedIndex == 2) selectedIndex = 8;
                else selectedIndex -= 3;
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                if (selectedIndex == 8) selectedIndex = 2;
                else if (selectedIndex == 7) selectedIndex = 1;
                else if (selectedIndex == 6) selectedIndex = 0;
                else selectedIndex += 3;
            }
            else if (keyPressed == ConsoleKey.Enter)
            {
                squareList[selectedIndex].UserPick();
                if (!squareList.Exists(x => x.IsUser == null)) gameOver = true;
                if (!gameOver) BotPicker();
            }

            squareList[selectedIndex].IsSelected = true;
            Clear();
        } while (keyPressed != ConsoleKey.Escape);

        void BotPicker()
        {
            while (true)
            {
                var randInd = new Random().Next(0, 8);
                if (squareList[randInd].IsUser == null)
                    squareList[randInd].BotPick();
                else
                    continue;
                break;
            }
        }
    }
}

//TODO: add winchecks