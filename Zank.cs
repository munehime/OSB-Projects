using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Zank : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var sprite = GetLayer("").CreateSprite("sb/unnamed/zank.png", OsbOrigin.BottomRight, new Vector2(785, 510));
            var easing = OsbEasing.InCubic;

            sprite.Scale(697049, 0.16);
            sprite.FlipH(697049, 762654);
            sprite.Fade(697049, 1);
            sprite.Fade(762654, 1);

            sprite.StartLoopGroup(697049, 32);
            sprite.Rotate(easing, 0, 316, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-10));
            sprite.Rotate(easing, 316, 632, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));
            sprite.EndGroup();

            sprite.StartLoopGroup(717260, 4);
            sprite.Rotate(easing, 0, 632, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-10));
            sprite.Rotate(easing, 632, 1264, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));
            sprite.EndGroup();

            sprite.StartLoopGroup(722312, 24);
            sprite.Rotate(easing, 0, 316, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-10));
            sprite.Rotate(easing, 316, 632, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));
            sprite.EndGroup();

            sprite.StartLoopGroup(737470, 1);
            sprite.Rotate(easing, 0, 632, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-10));
            sprite.Rotate(easing, 632, 1264, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));
            sprite.EndGroup();

            sprite.StartLoopGroup(738733, 32);
            sprite.Rotate(easing, 0, 316, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-10));
            sprite.Rotate(easing, 316, 632, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));
            sprite.EndGroup();

            sprite.StartLoopGroup(758944, 2);
            sprite.Rotate(easing, 0, 632, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-10));
            sprite.Rotate(easing, 632, 1264, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));
            sprite.EndGroup();

            sprite.StartLoopGroup(761470, 3);
            sprite.Rotate(easing, 0, 316, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-10));
            sprite.Rotate(easing, 316, 632, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(10));
            sprite.EndGroup();
        }
    }
}
