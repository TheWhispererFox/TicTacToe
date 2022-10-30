using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class GameBoard : ICloneable
    {
        public enum Size
        {
            Small = 3,
            Medium = 4,
            Large = 5,
        }
        public enum Slot
        {
            Empty,
            X,
            O,
        }
        public Size BoardSize { get; set; }
        private Slot[] slots;
        public Slot[] Slots { get { return slots; } }
        
        public GameBoard(Size size)
        {
            BoardSize = size;
            switch (size)
            {
                case Size.Small:
                    slots = new Slot[9];
                    break;
                case Size.Medium:
                    slots = new Slot[16];
                    break;
                case Size.Large:
                    slots = new Slot[25];
                    break;
                default:
                    BoardSize = Size.Small;
                    slots = new Slot[9];
                    break;
            }
        }

        public object Clone()
        {
            return new GameBoard(BoardSize) { slots = (Slot[])slots.Clone() };
        }
    }
}
