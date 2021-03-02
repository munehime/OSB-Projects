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
    public class Plusing : StoryboardObjectGenerator
    {
        [Configurable]
        public OsbEasing EasingScale;

        [Configurable]
        public OsbEasing EasingFade;

        public override void Generate()
        {            
            var layer = GetLayer("");
            var bg = layer.CreateSprite("SB/BG/BW.png", OsbOrigin.Centre);
            var screenScale = 480.0 / 1200;
            var beatDuration = Beatmap.GetTimingPointAt(59232).BeatDuration;
            var easingScale = EasingScale;
            var easingFade = EasingFade;

            bg.Scale(easingScale, 216428, 217228, screenScale, screenScale * 1.1);
            bg.Fade(easingFade, 216428, 217228, 1, 0);
        }
    }
}
