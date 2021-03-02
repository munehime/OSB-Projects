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
    public class TranstionManager : StoryboardObjectGenerator
    {
        private double beatDuration;

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            GeneratePLFlash();
            GenerateNormalFlash();
        }

        private void GeneratePLFlash()
        {
            var sprite = GetLayer("").CreateSprite("sb/hl.png");
            sprite.Scale(OsbEasing.InSine, 27703, 28329, 0, 11);
        }

        private void GenerateNormalFlash()
        {
            var time = new int[] { 28329 };
            var sprite = GetLayer("").CreateSprite("sb/p.png");
            sprite.ScaleVec(time[0], 854, 480);
            foreach (var t in time)
            {
                sprite.Fade(t, t + beatDuration * 4, 1, 0);
            }
        }
    }
}
