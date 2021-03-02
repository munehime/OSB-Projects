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
    public class PlusingBack : StoryboardObjectGenerator
    {
        [Configurable]
        public OsbEasing EasingScale;

        [Configurable]
        public OsbEasing EasingFade;

        public override void Generate()
        {            
            var layer = GetLayer("");
            var bg = layer.CreateSprite("SB/BG/Glow.png", OsbOrigin.Centre);
            var screenScale = 480.0 / 1200;
            var beatDuration = Beatmap.GetTimingPointAt(59232).BeatDuration;
            var easingScale = EasingScale;
            var easingFade = EasingFade;

            bg.StartLoopGroup(59232, 16);
                bg.Scale(easingScale, 0, beatDuration * 4, screenScale, screenScale * 1.02);
                bg.Fade(easingFade, 0, beatDuration * 4, 1, 0);
            bg.EndGroup();    
        }
    }
}
