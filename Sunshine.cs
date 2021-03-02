using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Sunshine : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var sprite = GetLayer("").CreateSprite("sb/burst.png", OsbOrigin.Centre);

            sprite.Move(145747, 50, -50);
            sprite.Scale(145747, 1.35);
            sprite.Fade(145747, 0.75);
            sprite.Fade(166482, 166747, 0.75, 0.35);
            sprite.Fade(167982, 0.75);
            sprite.Rotate(145747, 189865, MathHelper.DegreesToRadians(7), MathHelper.DegreesToRadians(727));
        }
    }
}
