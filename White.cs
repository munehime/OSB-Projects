using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class White : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            white(92220, 92720, 1, 0);
            white(145747, 146747, 1, 0);
            white(167982, 168682, 1, 0);
            white(432725, 433225, 1, 0);
            white(697049, 698049, 1, 0);
            white(785635, 786635, 0.5, 0);
            white(866465, 867465, 1, 0);
            white(950697, 951197, 1, 0);
            white(973511, 974511, 1, 0);
            white(981716, 982716, 1, 0);
            white(1030443, 1031443, 1, 0);
            white(1054443, 1055443, 1, 0);
            white(1062443, 1063443, 1, 0);
            white(1066443, 1067443, 1, 0);
        }

        void white(int startTime, int endTime, double startOpacity, double endOpacity)
        {
            var sprite = GetLayer("flash").CreateSprite("sb/pixel.png", OsbOrigin.Centre);

            sprite.ScaleVec(startTime, 1000, 563);
            sprite.Fade(startTime, endTime, startOpacity, endOpacity);
        }
    }
}
