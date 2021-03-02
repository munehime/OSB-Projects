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
    public class Black : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("");
            var black = layer.CreateSprite("SB/black.png", OsbOrigin.Centre);
        
            var beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            var screenScale = 480.0 / 768;

            black.Scale(136664, screenScale * 2);
            black.Fade(136664, 137286, 0, 1);
            black.Fade(150043, 1);
        }
    }
}
