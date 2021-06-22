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
    public class Logos : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Arknights();
        }

        private void Arknights()
        {
            var sprite = GetLayer("arknights").CreateAnimation("sb/logo/arknights/arknights.jpg", 25, 40, OsbLoopType.LoopOnce);
            var scale = 480.0 / 1080;

            sprite.Fade(4835, 1);
            sprite.Scale(5835, 9155, scale, scale * (435.0 / 445));
            sprite.Fade(5835, 9155, 1, 0);
        }
    }
}
