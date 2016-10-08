using System;
using System.Collections.Generic;
using System.Linq;

namespace _15Puzzle
{
    /// Я не до конца понял, что такое класс-декоратор ///
    /// 
    public class ImmutableGameDecorator : ImmutableGame
    {
        public readonly int[] squareTiles;
        public Dictionary<int, int> gameHistory;

        public ImmutableGameDecorator()
        {
            squareTiles = (int[])squareTilesValues.Clone();
        }

        public override Point GetLocation(int value)
        {
            int shift;
            if (gameHistory.ContainsKey(value))
            {
                shift = gameHistory[value];
            }
            else
                shift = squareTiles[value];
            int x = shift % sideSize;
            return new Point(x, (shift - x) / sideSize);
        }

        public override int this[int x, int y]
        {
            get
            {
                var value = gameHistory.FirstOrDefault(a => a.Value == x + y * sideSize).Key;
                if (value != default(int))
                    return value;
                else
                    return squareTiles[x + y * sideSize];
            }
            protected set { }
        }

        public ImmutableGameDecorator ImmutableShift(int value)
        {
            var valueCoord = GetLocation(value);
            var nullCoord = GetLocation(0);

            if (IsShiftAvaliable(valueCoord, nullCoord))
            {
                gameHistory.Add(value, valueCoord.x + valueCoord.y * sideSize);
                gameHistory.Add(value, valueCoord.x + valueCoord.y * sideSize);
                return this;
            }
            else
            {
                throw new Exception("This value cannot be shifted.");
            }
        }
    }
}
