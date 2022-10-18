using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class GameManager
    {
        public enum GameState
        {
            XTurn,
            OTurn,
            OWin,
            XWin,
            Tie,
        }
        public enum GameMode
        {
            PvP,
            PvC,
            CvC,
        }

        public GameState State { get; set; } = GameState.XTurn;
        public GameMode Mode { get; set; } = GameMode.PvP;

        public GameBoard Board { get; }
        public GameManager(GameBoard.Size size, GameMode mode)
        {
            Board = new GameBoard(size);
            Mode = mode;
        }

        private static GameManager? instance;

        public static GameManager? Instance
        {
            get
            {
                return instance;
            }
            set
            {
                if (instance == null)
                {
                    instance = value;
                }
            }
        }

        public bool DoTurn(int slot)
        {
            if (Board.Slots[slot] != GameBoard.Slot.Empty) return false;
            if (IsWinning(Board, GameBoard.Slot.X))
            {
                State = GameState.XWin;
                return false;
            } else if (IsWinning(Board, GameBoard.Slot.O))
            {
                State = GameState.OWin;
                return false;
            }
            if (State == GameState.XTurn)
            {
                Board.Slots[slot] = GameBoard.Slot.X;
                State = GameState.OTurn;
            }
            else
            {
                Board.Slots[slot] = GameBoard.Slot.O;
                State = GameState.XTurn;
            }
            return true;
        }

        public bool IsWinning(GameBoard board, GameBoard.Slot player)
        {
            bool winning = true;
            int size = ((int)board.BoardSize);

            // Check for row combinations
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size + i; j++)
                {
                    winning &= board.Slots[j] == player;
                }
            }
            if (winning) return true;
            winning = true;

            // Check for column combinations
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size * size; j += size)
                {
                    winning &= board.Slots[j] == player;
                }
            }
            if (winning) return true;
            winning = true;

            // Check for diagonal combinations left to right
            for (int i = 0; i < size * size; i += size + 1)
            {
                winning &= board.Slots[i] == player;
            }
            if (winning) return true;
            winning = true;

            // Check for diagonal combinations right to left
            for (int i = 0; i < size * size; i += size - 1)
            {
                winning &= board.Slots[i] == player;
            }
            if (winning) return true;
            return false;

            // Euristic solution

            //switch (board.BoardSize)
            //{
            //    case GameBoard.Size.Small:
            //        return  (board.Slots[0] == player && board.Slots[1] == player && board.Slots[2] == player) ||
            //                (board.Slots[3] == player && board.Slots[4] == player && board.Slots[5] == player) ||
            //                (board.Slots[6] == player && board.Slots[7] == player && board.Slots[8] == player) ||
            //                (board.Slots[0] == player && board.Slots[3] == player && board.Slots[6] == player) ||
            //                (board.Slots[1] == player && board.Slots[4] == player && board.Slots[7] == player) ||
            //                (board.Slots[2] == player && board.Slots[5] == player && board.Slots[8] == player) ||
            //                (board.Slots[0] == player && board.Slots[4] == player && board.Slots[8] == player) ||
            //                (board.Slots[2] == player && board.Slots[4] == player && board.Slots[6] == player);
            //    case GameBoard.Size.Medium:
            //        return (board.Slots[0] == player && board.Slots[1] == player && board.Slots[2] == player && board.Slots[3])
            //        break;
            //    case GameBoard.Size.Large:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
