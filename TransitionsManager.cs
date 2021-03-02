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
    public class TransitionsManager : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            AddFlash();

        }

        private void AddFlash()
        {
            var time = new List<int>() { 18571, 27628, 63854, 72911, 109137, 118194, 145364 };
            var sprite = GetLayer("flash").CreateSprite("sb/p.png");
            sprite.ScaleVec(time.First(), 854, 480);
            foreach (var t in time)
            {
                sprite.Fade(t, t + BeatDuration * 4, 1, 0);
            }
        }
    }
}
