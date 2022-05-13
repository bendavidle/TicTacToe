using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class BoardView
    {
        public BoardModel Board { get; }
        private string[,] _board => Board.Board;

        public BoardView(BoardModel board)
        {
            Board = board;
        }

        public void Show()
        {

            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    Console.Write(_board[row, col]);
                }
                Console.Write("\r\n");
            }
        }
    }
}
