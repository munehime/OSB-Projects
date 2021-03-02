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
    public class BG : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("MainBackground");
            var bg = layer.CreateSprite("878911.jpg", OsbOrigin.Centre);
            var screenScale = 480.0 / 1200;

            bg.Scale(0, screenScale * 1.02);
            bg.Fade(OsbEasing.In, 0, 2000, 0, 1);
            bg.Fade(216427, 1);
            bg.Fade(216428, 222610, 0.95, 0.85);
            bg.Fade(OsbEasing.Out, 222610, 224637, 0.85, 0);        
        }
    }
}
