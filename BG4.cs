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
    public class BG4 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("");
            var bg = layer.CreateSprite("SB/BG/Yellow.png", OsbOrigin.Centre);
            var screenScale = 480.0 / 1200;
            
            bg.Scale(150043, screenScale * 1.02);
            bg.Fade(150043, 1);
            bg.Fade(163016, 1);
        }
    }
}
