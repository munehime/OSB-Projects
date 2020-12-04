using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Flash : StoryboardObjectGenerator
    {
        OsbSprite sprite;
        double beatDuration;
        public override void Generate()
        {
            sprite = GetLayer("").CreateSprite("sb/p.png");
            sprite.ScaleVec(0, 854, 480);
            sprite.Fade(0, 0);

            AddFlash(11982, 8);
            AddFlash(25316, 8);
            AddFlash(46627, 8);
            AddFlash(75579, 8);
            AddFlash(178259, 8);
            AddFlash(178259, 8);
            AddFlash(217654, 8);
            AddFlash(260854, 8);
            AddFlash(284854, 8);
            AddFlash(319653, 8);
            AddFlash(351653, 8);
            AddFlash(388743, 8);
            AddFlash(412743, 4, 0.5);

            sprite = GetLayer("").CreateSprite("sb/p.png");
            var bookmarks = Beatmap.Bookmarks.ToList();
            sprite.ScaleVec(bookmarks[0], 854, 480);
            sprite.Additive(bookmarks[0], bookmarks.Last());
            for (int i = 0; i < bookmarks.Count() - 1; i++)
            {
                beatDuration = Beatmap.GetTimingPointAt(bookmarks[i]).BeatDuration;

                if (bookmarks[i + 1] - bookmarks[i] < beatDuration * 4)
                    sprite.Fade(bookmarks[i], bookmarks[i + 1], 0.075, 0);
                else if (bookmarks[i + 1] - bookmarks[i] < beatDuration * 8)
                    sprite.Fade(bookmarks[i], bookmarks[i] + beatDuration * 4, 0.075, 0);
                else
                    sprite.Fade(bookmarks[i], bookmarks[i] + beatDuration * 8, 0.075, 0);
            }
        }

        void AddFlash(double startTime, double beatMultiply = 4, double opacity = 1, OsbEasing easing = OsbEasing.None)
        {
            beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            sprite.Fade(easing, startTime, startTime + beatDuration * beatMultiply, opacity, 0);
        }
    }
}
