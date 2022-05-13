using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Channels;

namespace TicTacToe
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BoardModel boardModel = new BoardModel();
            BoardView game = new BoardView(boardModel);
            int player = 1;
            game.Show();

            while (true)
            {
                string choice = Console.ReadLine();
                if (choice.Length != 2 || !choice.Any(char.IsDigit))
                {
                    ErrorMessage(game, "Position is out of range, please enter new position: ");
                }
                else
                {
                    if (choice[0] - 'a' > 3 || choice[1] - '0' > 3)
                    {
                        ErrorMessage(game, "Position is out of range, please enter new position: ");
                    }
                    else
                    {
                        if (game.Board.IsEmpty(choice))
                        {
                            game.Board.SetPiece(choice, player);
                            player = (2 + 1) - player;
                            Console.Clear();
                            game.Show();

                            if (game.Board.CheckWin())
                            {
                                string cell = player switch
                                {
                                    1 => "O",
                                    2 => "X",
                                    _ => " "
                                };

                                Console.WriteLine(cell + " won!");
                                break;
                            }

                            if (game.Board.CheckWin() || !game.Board.IsFilled()) continue;

                            Console.WriteLine("Its a tie!");
                            break;



                        }

                        ErrorMessage(game, "Position is already occupied, please enter new position: ");

                    }
                }


            }
        }

        private static void ErrorMessage(BoardView game, string msg)
        {
            Console.Clear();
            game.Show();
            Console.WriteLine(msg);
        }
    }
}
