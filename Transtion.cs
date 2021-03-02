using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Transtion : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var startTime = 862327;
            var endTime = 864041;
            var delay = 0d;
            var spriteCount = 10;
            var width = 854f / spriteCount;
            var posX = -107 + width * 0.5f;

            for (var i = 0; i < spriteCount; i++)
            {
                var sprite = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(posX, 240));
                sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, 0, 480, width, 480);
                //sprite.ScaleVec(OsbEasing.OutExpo, startTime + 600, startTime + 1200, 784, height, 854, height);
                sprite.Fade(startTime, 1);
                sprite.Fade(866465, 1);

                posX += width;
                delay += 100;
            }
        }
    }
}
