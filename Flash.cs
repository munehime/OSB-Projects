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
        public override void Generate()
        {
            var layer = GetLayer("Flash");
            var flash = layer.CreateSprite("SB/white.jpg", OsbOrigin.Centre);
        
            var beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            var screenScale = 480.0 / 768;

            flash.Scale(7340, screenScale * 2);
            flash.Fade(OsbEasing.Out, 7340, 8340, 1, 0);
            flash.Fade(OsbEasing.Out, 20313, 21813, 0.5, 0);
            flash.Fade(46259, 47259, 1, 0);
            flash.Fade(59232, 60232, 1, 0);
            flash.Fade(85178, 86178, 1, 0);
            flash.Fade(124097, 125097, 1, 0);
            flash.Fade(150043, 151043, 1, 0);
            flash.Fade(163016, 164016, 1, 0);
            flash.Fade(177610, 178610, 1, 0);
        }
    }
}
