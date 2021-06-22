using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Flash : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            var sprite = GetLayer("").CreateSprite("sb/p.png");

            sprite.ScaleVec(19919, 854, 480);

            sprite.Fade(19919, 19919 + BeatDuration / 2, 0, 1);
            sprite.Fade(20141, 20141 + BeatDuration * 4, 1, 0);

            sprite.Fade(38586, 38586 + BeatDuration /2, 0, 0.7);
            sprite.Fade(38808, 38808 + BeatDuration * 4, 0.7, 0);

            sprite.Fade(43474, 43474 + BeatDuration * 1.5, 0, 0.7);
            sprite.Fade(44141, 44141 + BeatDuration * 6, 0.7, 0);

            sprite.Fade(65363, 65363 + BeatDuration / 4, 0, 1);
            sprite.Fade(65474, 65474 + BeatDuration * 4, 1, 0);

            // First kiai
            sprite.Fade(118808, 118808 + BeatDuration * 6, 1, 0);
            sprite.Fade(129475, 129475 + BeatDuration * 4, 1, 0);
            sprite.Fade(140141, 140141 + BeatDuration * 4, 1, 0);

            sprite.Fade(161475, 161475 + BeatDuration * 4, 1, 0);

            sprite.Fade(182141, 182141 + BeatDuration * 1.5, 0, 1);
            sprite.Fade(182808, 182808 + BeatDuration * 8, 1, 0);

            sprite.Fade(203919, 203919 + BeatDuration / 2, 0, 1);
            sprite.Fade(204141, 204141 + BeatDuration * 4, 1, 0);

            // Second kiai
            sprite.Fade(225475, 225475 + BeatDuration * 6, 1, 0);
            sprite.Fade(236141, 236141 + BeatDuration * 4, 1, 0);
            sprite.Fade(246808, 246808 + BeatDuration * 4, 1, 0);
            sprite.Fade(257475, 257475 + BeatDuration * 8, 1, 0);

            sprite.Additive(sprite.StartTime, sprite.EndTime);
        }
    }
}
