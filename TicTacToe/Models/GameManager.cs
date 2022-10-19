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

        public void DoTurn(int slot, Action onSuccessfulTurn)
        {
            if (Board.Slots[slot] != GameBoard.Slot.Empty) return;
            if (State == GameState.XTurn)
            {
                Board.Slots[slot] = GameBoard.Slot.X;
                onSuccessfulTurn?.Invoke();
                State = GameState.OTurn;
            }
            else
            {
                Board.Slots[slot] = GameBoard.Slot.O;
                onSuccessfulTurn?.Invoke();
                State = GameState.XTurn;
            }
            if (IsWinning(Board, GameBoard.Slot.X))
            {
                State = GameState.XWin;
            } else if (IsWinning(Board, GameBoard.Slot.O))
            {
                State = GameState.OWin;
            }
        }

        public bool IsWinning(GameBoard board, GameBoard.Slot player)
        {
            bool winning = true;
            int size = ((int)board.BoardSize);

            // Check for row combinations
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    winning &= board.Slots[j + i * size] == player;
                }
                if (!winning) { winning = true; } else { return true; }
            }

            // Check for column combinations
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    winning &= board.Slots[i + j * size] == player;
                }
                if (!winning) { winning = true; } else { return true; }
            }

            // Check for diagonal combinations left to right
            for (int i = 0; i < size * size; i += size + 1)
            {
                winning &= board.Slots[i] == player;
            }
            if (winning) return true;
            winning = true;

            // Check for diagonal combinations right to left
            for (int i = size - 1; i < size * size - (size - 2); i += size - 1)
            {
                winning &= board.Slots[i] == player;
            }
            if (winning) return true;
            return false;
        }
    }
}
