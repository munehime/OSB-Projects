using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Black : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            black(762589, 762639, 0, 1);
        }

        void black(int startTime, int endTime, double startOpacity, double endOpacity)
        {
            var sprite = GetLayer("black").CreateSprite("sb/pixel.png", OsbOrigin.Centre);

            sprite.ScaleVec(startTime, 1000, 563);
            sprite.Color(startTime, 0, 0, 0);
            sprite.Fade(startTime, endTime, startOpacity, endOpacity);
            if (startTime == 762589)
                sprite.Fade(770865, 1);
        }
    }
}
