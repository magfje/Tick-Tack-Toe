namespace Tick_Tack_Toe;

using static Console;

public class Board
{
    private readonly List<Square> _squareList;
    private bool _gameOver;
    private bool _isDraw;
    private ConsoleKey _keyPressed;
    private int _selectedIndex;

    public Board()
    {
        _squareList = new List<Square>();
        CreateBoard();
    }

    private void CreateBoard()
    {
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
            _squareList.Add(square);
        }
    }

    public void DrawBoard()
    {
        do
        {
            foreach (var a in _squareList)
            {
                a.Draw();
                a.IsSelected = false;
            }


            if (CheckWin() == 1) WriteLine("\nYou Won!");
            else if (CheckWin() == 2) WriteLine("\nComputer Won!");
            else if (_isDraw) WriteLine("\nDraw!");


            KeyController();

            _squareList[_selectedIndex].IsSelected = true;

            Clear();
            if (CheckWin() > 0 || _isDraw) _gameOver = true;
        } while (_keyPressed != ConsoleKey.Escape);
    }

    private void KeyController()
    {
        var keyInfo = ReadKey(true);
        _keyPressed = keyInfo.Key;

        if (_gameOver) return;

        if (_keyPressed == ConsoleKey.LeftArrow)
        {
            if (_selectedIndex is 0 or 3 or 6) _selectedIndex += 2;
            else _selectedIndex -= 1;
        }
        else if (_keyPressed == ConsoleKey.RightArrow)
        {
            if (_selectedIndex is 2 or 5 or 8) _selectedIndex -= 2;
            else _selectedIndex++;
        }
        else if (_keyPressed == ConsoleKey.UpArrow)
        {
            if (_selectedIndex is 0 or 1 or 2) _selectedIndex += 6;
            else _selectedIndex -= 3;
        }
        else if (_keyPressed == ConsoleKey.DownArrow)
        {
            if (_selectedIndex is 6 or 7 or 8) _selectedIndex -= 6;
            else _selectedIndex += 3;
        }
        else if (_keyPressed == ConsoleKey.Enter && _squareList[_selectedIndex].IsUser == null)
        {
            _squareList[_selectedIndex].UserPick();
            if (!_squareList.Exists(x => x.IsUser == null)) _isDraw = true;
            if (CheckWin() < 1) BotPicker();
        }
    }

    private void BotPicker()
    {
        while (!_isDraw)
        {
            var randInd = new Random().Next(0, 8);
            if (_squareList[randInd].IsUser == null)
                _squareList[randInd].BotPick();
            else
                continue;
            break;
        }
    }

    private int CheckWin()
    {
        //ROW
        for (var i = 0; i < 9; i += 3)
            if (_squareList[i].IsUser == _squareList[i + 1].IsUser
                && _squareList[i + 1].IsUser == _squareList[i + 2].IsUser
               )
                return _squareList[i].IsUser switch
                {
                    null => 0,
                    true => 1,
                    false => 2
                };

        //Col
        for (var i = 0; i < 3; i++)
            if (_squareList[i].IsUser == _squareList[i + 3].IsUser
                && _squareList[i + 3].IsUser == _squareList[i + 6].IsUser)
                return _squareList[i].IsUser switch
                {
                    null => 0,
                    true => 1,
                    false => 2
                };

        if (_squareList[0].IsUser != null
            && _squareList[0].IsUser == _squareList[4].IsUser
            && _squareList[4].IsUser == _squareList[8].IsUser)
            return _squareList[0].IsUser switch
            {
                null => 0,
                true => 1,
                false => 2
            };

        if (_squareList[2].IsUser != null
            && _squareList[2].IsUser == _squareList[4].IsUser
            && _squareList[4].IsUser == _squareList[6].IsUser)
            return _squareList[2].IsUser switch
            {
                null => 0,
                true => 1,
                false => 2
            };

        return -1;
    }
}