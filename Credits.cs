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
    public class Credits : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("");
            var artist = layer.CreateSprite("SB/Text/Artist.png", OsbOrigin.Centre);
            var artistG = layer.CreateSprite("SB/Text/glow/Artist.png", OsbOrigin.Centre);
            var title = layer.CreateSprite("SB/Text/Title.png", OsbOrigin.Centre);
            var titleG = layer.CreateSprite("SB/Text/glow/Title.png", OsbOrigin.Centre);
            var beatmap = layer.CreateSprite("SB/Text/Beatmap.png", OsbOrigin.Centre);
            var beatmapG = layer.CreateSprite("SB/Text/glow/Beatmap.png", OsbOrigin.Centre);
            var diff = layer.CreateSprite("SB/Text/Diff.png", OsbOrigin.Centre);
            var diffG = layer.CreateSprite("SB/Text/glow/Diff.png", OsbOrigin.Centre);
            var storyboard = layer.CreateSprite("SB/Text/Storyboard.png", OsbOrigin.Centre);
            var storyboardG = layer.CreateSprite("SB/Text/glow/Storyboard.png", OsbOrigin.Centre);

		    artist.Move(OsbEasing.InSine, 7137, 7340, 706, 123, 506, 123);
            artist.Move(7340, 10381, 506, 123, 446, 123);
            artist.Move(OsbEasing.OutSine, 10381, 10583, 446, 123, 346, 123);
            artist.Scale(7137, 0.5);
            artist.Fade(OsbEasing.InSine, 7137, 7340, 0, 1);
            artist.Fade(OsbEasing.OutSine, 10381, 10583, 1, 0);

            artistG.Move(OsbEasing.InSine, 7137, 7340, 706, 123, 506, 123);
            artistG.Move(7340, 10381, 506, 123, 446, 123);
            artistG.Move(OsbEasing.OutSine, 10381, 10583, 446, 123, 346, 123);
            artistG.Scale(7137, 0.5);
            artistG.Fade(OsbEasing.InSine, 7137, 7340, 0, 1);
            artistG.Fade(OsbEasing.OutSine, 10381, 10583, 1, 0);

            title.Move(OsbEasing.InSine, 8759, 8962, 334, 171, 434, 171);
            title.Move(8962, 10381, 434, 171, 464, 171);
            title.Move(OsbEasing.OutSine, 10381, 10583, 464, 171, 564, 171);
            title.Scale(8759, 0.45);
            title.Fade(OsbEasing.InSine, 8759, 8962, 0, 1);
            title.Fade(OsbEasing.OutSine, 10381, 10583, 1, 0);

            titleG.Move(OsbEasing.InSine, 8759, 8962, 334, 171, 434, 171);
            titleG.Move(8962, 10381, 434, 171, 464, 171);
            titleG.Move(OsbEasing.OutSine, 10381, 10583, 464, 171, 564, 171);
            titleG.Scale(8759, 0.45);
            titleG.Fade(OsbEasing.InSine, 8759, 8962, 0, 1);
            titleG.Fade(OsbEasing.OutSine, 10381, 10583, 1, 0);

            beatmap.Move(OsbEasing.InSine, 10381, 10583, 440, 320, 440, 220);
            beatmap.Move(10583, 16867, 440, 220, 440, 170);
            beatmap.Move(OsbEasing.OutSine, 16867, 17070, 440, 170, 440, 70);
            beatmap.Scale(10381, 0.5);
            beatmap.Fade(OsbEasing.InSine, 10381, 10583, 0, 1);
            beatmap.Fade(OsbEasing.OutSine, 16867, 17070, 1, 0);

            beatmapG.Move(OsbEasing.InSine, 10381, 10583, 440, 320, 440, 220);
            beatmapG.Move(10583, 16867, 440, 220, 440, 170);
            beatmapG.Move(OsbEasing.OutSine, 16867, 17070, 440, 170, 440, 70);
            beatmapG.Scale(10381, 0.5);
            beatmapG.Fade(OsbEasing.InSine, 10381, 10583, 0, 1);
            beatmapG.Fade(OsbEasing.OutSine, 16867, 17070, 1, 0);

            diff.Move(OsbEasing.InSine, 12002, 12205, 406, 334, 406, 234);
            diff.Move(12205, 16867, 406, 234, 406 ,197.5);
            diff.Move(OsbEasing.OutSine, 16867, 17070, 406, 197.5, 406, 97.5);
            diff.Scale(10381, 0.5);
            diff.Fade(OsbEasing.InSine, 12002, 12205, 0, 1);
            diff.Fade(OsbEasing.OutSine, 16867, 17070, 1, 0);

            diffG.Move(OsbEasing.InSine, 12002, 12205, 406, 334, 406, 234);
            diffG.Move(12205, 16867, 406, 234, 406 ,197.5);
            diffG.Move(OsbEasing.OutSine, 16867, 17070, 406, 197.5, 406, 97.5);
            diffG.Scale(10381, 0.5);
            diffG.Fade(OsbEasing.InSine, 12002, 12205, 0, 1);
            diffG.Fade(OsbEasing.OutSine, 16867, 17070, 1, 0);

            storyboard.Move(OsbEasing.InSine, 16867, 17070, 511, 216, 411, 216);
            storyboard.Move(17070, 20110, 411, 216, 361, 216);
            storyboard.Move(OsbEasing.OutSine, 20110, 20313, 361, 216, 261, 216);
            storyboard.Scale(16867, 0.5);
            storyboard.Fade(OsbEasing.InSine, 16867, 17070, 0, 1);
            storyboard.Fade(OsbEasing.OutSine, 20110, 20313, 1, 0);

            storyboardG.Move(OsbEasing.InSine, 16867, 17070, 511, 216, 411, 216);
            storyboardG.Move(17070, 20110, 411, 216, 361, 216);
            storyboardG.Move(OsbEasing.OutSine, 20110, 20313, 361, 216, 261, 216);
            storyboardG.Scale(16867, 0.5);
            storyboardG.Fade(OsbEasing.InSine, 16867, 17070, 0, 1);
            storyboardG.Fade(OsbEasing.OutSine, 20110, 20313, 1, 0);
        }
    }
}
