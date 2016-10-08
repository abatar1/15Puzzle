using System;

namespace _15Puzzle
{
    public class ImmutableGame : Game
    {

        public ImmutableGame(params int[] input) : base(input)
        {

        }

        public override Game Shift(int value)
        {
            var valueCoord = GetLocation(value);
            var nullCoord = GetLocation(0);

            if (IsShiftAvaliable(valueCoord, nullCoord))
            {
                var newSquareTilesValues = (int[])squareTilesValues.Clone();
                newSquareTilesValues[nullCoord.x + nullCoord.y * sideSize] = value;
                newSquareTilesValues[valueCoord.x + valueCoord.y * sideSize] = 0;
                return new Game(newSquareTilesValues);
            }
            else
            {
                throw new Exception("This value cannot be shifted.");
            }
        }
    }
}
