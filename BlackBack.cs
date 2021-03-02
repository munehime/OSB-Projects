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
    public class BlackBack : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("");
            var sprite = layer.CreateSprite("SB/black.png", OsbOrigin.Centre);
		    
            sprite.Move(19502, 301.3818, 410.4727);
            sprite.Scale(19502, 0.8);
            sprite.ScaleVec(OsbEasing.In, 19502, 19705, 5, 0, 5, 0.08);
            sprite.ScaleVec(OsbEasing.Out, 19705, 19908, 5, 0.08, 5, 0.05);
            sprite.Fade(19502, 0.85);
            sprite.ScaleVec(OsbEasing.In, 74029, 74232, 5, 0.05, 5, 0.08);
            sprite.ScaleVec(OsbEasing.Out, 74232, 74435, 5, 0.08, 5, 0);
            sprite.Fade(74435, 0.85);
            sprite.ScaleVec(OsbEasing.In, 84367, 84570, 5, 0, 5, 0.08);
            sprite.ScaleVec(OsbEasing.Out, 84570, 84874, 5, 0.08, 5, 0.05);
            sprite.Fade(84468, 0.85);
            sprite.ScaleVec(OsbEasing.In, 139198, 139401, 5, 0.05, 5, 0.08);
            sprite.ScaleVec(OsbEasing.Out, 139401, 139603, 5, 0.08, 5, 0);
            sprite.Fade(139603, 0.85);
            sprite.ScaleVec(OsbEasing.In, 149435, 149637, 5, 0, 5, 0.08);
            sprite.ScaleVec(OsbEasing.Out, 149637, 149840, 5, 0.08, 5, 0.05);
            sprite.Fade(149435, 0.85);
            sprite.ScaleVec(OsbEasing.In, 205786, 205989, 5, 0.05, 5, 0.08);
            sprite.ScaleVec(OsbEasing.Out, 205989, 206191, 5, 0.08, 5, 0);
            sprite.Fade(206191, 0.85);
        }
    }
}
