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
    public class Pulsing : StoryboardObjectGenerator
    {   
        [Configurable]
        public OsbEasing EasingScale;

        [Configurable]
        public OsbEasing EasingFade;

        public override void Generate()
        {
            var layer = GetLayer("MainBackground");
            var bg = layer.CreateSprite("878911.jpg", OsbOrigin.Centre);
            var screenScale = 480.0 / 1200;
            var beatDuration = Beatmap.GetTimingPointAt(7340).BeatDuration;
            var easingScale = EasingScale;
            var easingFade = EasingFade;

            var StartTime = 7340;
            var Time = StartTime + 1622; 

            bg.StartLoopGroup(7340, 7);
                bg.Scale(easingScale, 0, beatDuration * 4, screenScale, screenScale * 1.05);
                bg.Fade(easingFade, 0, beatDuration * 4, 1, 0);
            bg.EndGroup();

            bg.StartLoopGroup(18691, 2);
                bg.Scale(easingScale, 0, beatDuration * 1, screenScale, screenScale * 1.05);
                bg.Fade(easingFade, 0, beatDuration * 1, 0.85, 0);
            bg.EndGroup();

            bg.StartLoopGroup(19502, 4);
                bg.Scale(easingScale, 0, beatDuration * 0.5, screenScale, screenScale * 1.05);
                bg.Fade(easingFade, 0, beatDuration * 0.5, 0.75, 0);
            bg.EndGroup();

            bg.Scale(OsbEasing.OutCirc, 20313, 23313, screenScale, screenScale * 1.1);
            bg.Fade(OsbEasing.OutCirc, 20313, 23313, 1, 0);                
        }
    }
}
