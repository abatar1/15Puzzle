using System;
using System.Linq;

namespace _15Puzzle
{
    public abstract class IGame
    {
        public abstract Point GetLocation(int value);
        public abstract Game Shift(int value);
    }

    public class Game : IGame
    {
        protected int[] squareTilesValues;
        private int[] squareTilesIndexes;
        protected readonly int sideSize;

        public Game(params int[] input)
        {
            var fieldSize = input.Count();
            var n = Math.Sqrt(fieldSize);
            if (n != (int)n || fieldSize == 0)
            {
                throw new Exception("Input matrix isn't square matrix.");
            }
            else
            {
                sideSize = (int)n;
                squareTilesValues = (int[])input.Clone();
                squareTilesIndexes = new int[fieldSize];
                for (int i = 0; i < fieldSize; i++)
                {
                    squareTilesIndexes[input[i]] = i;
                }
            }
        }

        public virtual int this[int x, int y]
        {
            get { return squareTilesValues[x + y * sideSize]; }
            protected set { squareTilesValues[x + y * sideSize] = value; }
        }

        public override Point GetLocation(int value)
        {
            int shift = squareTilesIndexes[value];
            int x = shift % sideSize;
            return new Point(x, (shift - x) / sideSize);
        }

        protected bool IsShiftAvaliable(Point valueCoord, Point nullCoord)
        {
            return (Math.Abs(valueCoord.x - nullCoord.x) | Math.Abs(valueCoord.y - nullCoord.y)) == 1;
        }

        public override Game Shift(int value)
        {
            var valueCoord = GetLocation(value);
            var nullCoord = GetLocation(0);

            if (IsShiftAvaliable(valueCoord, nullCoord))
            {
                squareTilesValues[nullCoord.x + nullCoord.y * sideSize] = value;
                squareTilesValues[valueCoord.x + valueCoord.y * sideSize] = 0;

                int tmp = squareTilesIndexes[value];
                squareTilesIndexes[value] = squareTilesIndexes[0];
                squareTilesIndexes[0] = tmp;
                return this;
            }
            else
            {
                throw new Exception("This value cannot be shifted.");
            }
        }
    }
}
