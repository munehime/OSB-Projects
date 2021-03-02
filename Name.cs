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
    public class Name : StoryboardObjectGenerator
    {   
        [Configurable]
        public string Path = "SB/Text/Rem.png";

        [Configurable]
        public string PathG = "SB/Text/glow/Rem.png";

        [Configurable]
        public int X = 0;

        [Configurable]
        public int Y = 0;

        public override void Generate()
        {
            var layer = GetLayer("");
            var sprite = layer.CreateSprite(Path, OsbOrigin.Centre);
            var spriteG = layer.CreateSprite(PathG, OsbOrigin.Centre);
            
            sprite.Move(OsbEasing.InSine, 12002, 12205, X, 334 + Y, X, 234 + Y);
            sprite.Move(12205, 16867, X, 234 + Y, X ,197.5 + Y);
            sprite.Move(OsbEasing.OutSine, 16867, 17070, X, 197.5 + Y, X, 97.5 + Y);
            sprite.Scale(10381, 0.5);
            sprite.Fade(OsbEasing.InSine, 12002, 12205, 0, 1);
            sprite.Fade(OsbEasing.OutSine, 16867, 17070, 1, 0);

            spriteG.Move(OsbEasing.InSine, 12002, 12205, X, 334 + Y, X, 234 + Y);
            spriteG.Move(12205, 16867, X, 234 + Y, X ,197.5 + Y);
            spriteG.Move(OsbEasing.OutSine, 16867, 17070, X, 197.5 + Y, X, 97.5 + Y);
            spriteG.Scale(10381, 0.5);
            spriteG.Fade(OsbEasing.InSine, 12002, 12205, 0, 1);
            spriteG.Fade(OsbEasing.OutSine, 16867, 17070, 1, 0);
        }
    }
}
