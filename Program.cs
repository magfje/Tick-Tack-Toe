using System.Text;
using static System.Console;

namespace Tick_Tack_Toe;

internal class Program
{
    private static void Main()
    {
        OutputEncoding = Encoding.UTF8;
        new Board().DrawBoard();
    }
}