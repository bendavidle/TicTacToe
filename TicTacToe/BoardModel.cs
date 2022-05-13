using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /*
                 string[,] board = new string[,]
            {
                { " ", " ", "a", " ", "b", " ", "c", " " },
                { " ", "┌", "─", "─", "─", "─", "─", "┐" },
                { "1", "│", "o", " ", " ", " ", " ", "│" },
                { "2", "│", " ", " ", " ", " ", "o", "│" },
                { "3", "│", "x", " ", "x", " ", " ", "│" },
                { " ", "└", "─", "─", "─", "─", "─", "┘" }
            };
     */
    internal class BoardModel
    {
        private Cell[,] _board = new Cell[3, 3]
        {
            { new Cell(), new Cell(), new Cell() },
            { new Cell(), new Cell(), new Cell() },
            { new Cell(), new Cell(), new Cell() }
        };
        public string[,] Board => GenerateBoard();

        public string[,] GenerateBoard()
        {
            string[,] template = {
                { " ", " ", "a", " ", "b", " ", "c", " " },
                { " ", "┌", "─", "─", "─", "─", "─", "┐" },
                { "1", "│", " ", " ", " ", " ", " ", "│" },
                { "2", "│", " ", " ", " ", " ", " ", "│" },
                { "3", "│", " ", " ", " ", " ", " ", "│" },
                { " ", "└", "─", "─", "─", "─", "─", "┘" }
            };

            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    int colIndex = (col * 2) + 2;
                    int rowIndex = row + 2;
                    int marker = _board[row, col].Marker;

                    string cell = marker switch
                    {
                        1 => "X",
                        2 => "O",
                        _ => " "
                    };

                    template[rowIndex, colIndex] = cell;
                }
            }

            return template;
        }

        public void SetPiece(string pos, int piece)
        {
            (int positionY, int positionX) = GetPositions(pos);
            int marker = _board[positionX, positionY].Marker;

            if (marker == 0)
            {
                _board[positionX, positionY].Marker = piece;
            }
        }

        public bool CheckWin()
        {
            List<int[]> rows = GetRows();
            List<int[]> cols = GetCols();
            List<int[]> diagonals = GetDiagonals();

            List<List<int[]>> allLines = new List<List<int[]>>
            {
                rows,
                cols,
                diagonals
            };

            return allLines.Any(section => section.Any(line => line.All(i => i is 1) || line.All(i => i is 2)));
        }

        public bool IsFilled()
        {
            return _board.Cast<Cell>().All(cell => cell.Marker is 1 or 2);
        }


        private List<int[]> GetRows()
        {
            List<int[]> rows = new List<int[]>();
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                int[] boardRow = new int[3];

                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    boardRow[col] = _board[row, col].Marker;
                }

                rows.Add(boardRow);
            }

            return rows;
        }

        private List<int[]> GetCols()
        {
            List<int[]> cols = new List<int[]>();
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                int[] boardCol = new int[3];

                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    boardCol[col] = _board[col, row].Marker;
                }

                cols.Add(boardCol);
            }

            return cols;
        }

        private List<int[]> GetDiagonals()
        {
            List<int[]> diagonals = new List<int[]>();

            for (int i = 0; i < 2; i++)
            {
                int[] diagonal = new int[3];

                for (int j = 0; j < _board.GetLength(0); j++)
                {
                    int tempIndex = i is 1 ? 2 - j : j;
                    diagonal[j] = _board[j, tempIndex].Marker;

                }
                diagonals.Add(diagonal);
            }


            return diagonals;
        }

        public bool IsEmpty(string pos)
        {
            (int positionY, int positionX) = GetPositions(pos);

            return _board[positionX, positionY].Marker == 0;
        }

        private static (int positionY, int positionX) GetPositions(string pos)
        {
            int positionY = pos.ToLower()[0] - 'a';
            int positionX = pos[1] - '0' - 1;
            return (positionY, positionX);
        }

        public void ResetBoard()
        {
            _board = new Cell[3, 3];
        }
    }
}
