using System.Collections.Generic;
using LaserTjack.Core;
using LaserTjack.Core.Pieces;
using Xunit;

namespace LaserTjack.Test
{
    public class DistortionTest
    {
        private static IEnumerable<T> AsEnum<T>(params T[] values) => values;

        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Right)]
        public void EmptyCell_LightFromAnyDirection_ContinuesInSameDirection(Direction direction)
        {
            Assert.Equal(AsEnum(direction), new EmptyCell().Refract(direction));
        }

        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Right)]
        public void Obelisk_LightFromAnyDirection_Stops(Direction direction)
        {
            Assert.Equal(AsEnum<Direction>(), new Obelisk().Refract(direction));
        }

        [Theory]
        [InlineData(Direction.Up, Direction.Left)]
        [InlineData(Direction.Right, Direction.Up)]
        [InlineData(Direction.Down, Direction.Right)]
        [InlineData(Direction.Left, Direction.Down)]
        public void Pyramid_LightHitsTheFront_Reflects90DegreesRight(Direction pyramidOrientation, Direction expectedDirection)
        {
            var pyramid = new Pyramid(pyramidOrientation);
            Assert.Equal(AsEnum(expectedDirection), pyramid.Refract(pyramidOrientation.Reverse()));
        }

        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Right)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        public void Pyramid_LightHitsFromPyramidsLeft_ReflectsToPyramidFront(Direction pyramidOrientation)
        {
            var pyramid = new Pyramid(pyramidOrientation);
            Assert.Equal(AsEnum(pyramidOrientation), pyramid.Refract(pyramidOrientation.NextClockwise()));
        }

        [Fact]
        public void Pyramid_LightHitsBack_Stops()
        {
            var pyramid = new Pyramid(Direction.Up);
            Assert.Equal(AsEnum<Direction>(), pyramid.Refract(Direction.Up));
        }

        [Theory]
        [InlineData(Direction.Up)]
        [InlineData(Direction.Down)]
        [InlineData(Direction.Left)]
        [InlineData(Direction.Right)]
        public void Pharaoh_LightFromAnyDirection_Stops(Direction direction)
        {
            Assert.Equal(AsEnum<Direction>(), new Pharaoh().Refract(direction));
        }

        [Fact]
        public void Djed_LightHitsFront_Reflects90DegreesRight()
        {
            var djed = new Djed(Direction.Up);
            Assert.Equal(AsEnum(Direction.Left), djed.Refract(Direction.Up.Reverse()));
        }

        [Fact]
        public void Djed_LightHitsFromBehind_Reflects90DegreesRight()
        {
            var djed = new Djed(Direction.Up);
            Assert.Equal(AsEnum(Direction.Right), djed.Refract(Direction.Up));
        }

        [Fact]
        public void EyeOfHorus_RefractsLight_ReturnsUnionOfDjedAndEmptyCell()
        {
            var eyeOfHorus = new EyeOfHorus(Direction.Up);
            Assert.Equal(AsEnum(Direction.Left, Direction.Down), eyeOfHorus.Refract(Direction.Up.Reverse()));
        }
    }
}