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
    public class Blackbars : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("").CreateSprite("sb/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(320, (i % 2) * 480));
                sprite.ScaleVec(9475, 854, 75);
                sprite.ScaleVec(OsbEasing.InExpo, 18808, 20141, 854, 75, 854, 0);
                sprite.Color(9475, Color4.Black);
            }
        }
    }
}
