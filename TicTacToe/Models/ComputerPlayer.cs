using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class ComputerPlayer
    {
        private readonly GameBoard.Slot computerPlayer;
        private readonly GameBoard.Slot humanPlayer;

        public ComputerPlayer(GameBoard.Slot computer, GameBoard.Slot human)
        {
            computerPlayer = computer;
            humanPlayer = human;
        }
        public Move Minimax(GameBoard board, GameBoard.Slot player)
        {

            //List<int> availableSlots = board.Slots.Where(slot => slot == GameBoard.Slot.Empty).Select((slot, id) => id).ToList();
            List<int> availableSlots = new();

            for (int i = 0; i < board.Slots.Length; i++)
            {
                if (board.Slots[i] == GameBoard.Slot.Empty)
                {
                    availableSlots.Add(i);
                }
            }

            if (GameManager.IsWinning(board, humanPlayer))
            {
                return new Move { score = -10 };
            }
            else if (GameManager.IsWinning(board, computerPlayer))
            {
                return new Move { score = 10 };
            }
            else if (board.Slots.All(slot => slot != GameBoard.Slot.Empty))
            {
                return new Move { score = 0 };
            }

            List<Move> moves = new();

            for (int i = 0; i < availableSlots.Count; i++)
            {
                Move move = new();
                move.id = availableSlots[i];

                board.Slots[availableSlots[i]] = player;

                if (player == computerPlayer)
                {
                    Move result = Minimax(board, humanPlayer);
                    move.score = result.score;
                }
                else
                {
                    Move result = Minimax(board, computerPlayer);
                    move.score = result.score;
                }

                board.Slots[availableSlots[i]] = GameBoard.Slot.Empty;

                moves.Add(move);
            }

            int bestMove = 0;
            if (player == computerPlayer)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].score > bestScore)
                    {
                        bestScore = moves[i].score;
                        bestMove = i;
                    }
                }
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].score < bestScore)
                    {
                        bestScore = moves[i].score;
                        bestMove = i;
                    }
                }
            }
            return moves[bestMove];
        }

        public void ComputerTurn(GameManager manager)
        {
            if (manager.State == GameManager.GameState.XTurn && computerPlayer == GameBoard.Slot.X ||
                manager.State == GameManager.GameState.OTurn && computerPlayer == GameBoard.Slot.O)
            {
                manager.DoTurn(Minimax(manager.Board, computerPlayer).id);
            }
        }
    }
}
