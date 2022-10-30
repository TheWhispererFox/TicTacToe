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

        public event Action<GameManager> StateChanged;
        public event Action<int> BoardChanged;

        private GameState state = GameState.XTurn;
        public GameState State 
        { 
            get 
            {
                return state;
            } 
            set 
            { 
                state = value;
                StateChanged?.Invoke(this);
            }
        }
        public GameMode Mode { get; set; } = GameMode.PvP;

        public GameBoard Board { get; }

        public ComputerPlayer? ComputerPlayer { get; }
        public GameManager(GameBoard.Size size, GameMode mode, bool isHumanFirst = true)
        {
            Board = new GameBoard(size);
            Mode = mode;
            switch (mode)
            {
                case GameMode.PvP:
                    ComputerPlayer = null;
                    break;
                case GameMode.PvC:
                    ComputerPlayer = new ComputerPlayer(isHumanFirst ? GameBoard.Slot.O : GameBoard.Slot.X, isHumanFirst ? GameBoard.Slot.X : GameBoard.Slot.O);
                    StateChanged += ComputerPlayer.ComputerTurn;
                    break;
                case GameMode.CvC:
                    break;
                default:
                    break;
            }
        }

        public void DoTurn(int slot)
        {
            if (CheckWinCondition())
            {
                return;
            }
            if (Board.Slots[slot] != GameBoard.Slot.Empty) return;
            if (State == GameState.XTurn)
            {
                Board.Slots[slot] = GameBoard.Slot.X;
                BoardChanged?.Invoke(slot);
                State = GameState.OTurn;
            }
            else
            {
                Board.Slots[slot] = GameBoard.Slot.O;
                BoardChanged?.Invoke(slot);
                State = GameState.XTurn;
            }
            CheckWinCondition();
        }

        public bool CheckWinCondition()
        {
            if (IsWinning(Board, GameBoard.Slot.X))
            {
                State = GameState.XWin;
                return true;
            }
            else if (IsWinning(Board, GameBoard.Slot.O))
            {
                State = GameState.OWin;
                return true;
            }
            return false;
        }

        public static bool IsWinning(GameBoard board, GameBoard.Slot player)
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
