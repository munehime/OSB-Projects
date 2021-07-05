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
    public class Logo : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            AddLogo();
            AddMainLogoWithBottomText();
            AddMainLogo();
        }

        private void AddLogo(double startTime = 971, double endTime = 2865)
        {
            var logoPath = "sb/logo/logo.png";
            var sprite = GetLayer("logo").CreateSprite(logoPath);
            var bitmap = GetMapsetBitmap(logoPath);

            sprite.Fade(-BeatDuration * 4, 0, 0, 1);
            sprite.Scale(-BeatDuration * 4, bitmap.Height * 0.0012);
            sprite.Fade(OsbEasing.OutExpo, endTime, endTime + BeatDuration * 0.25, 1, 0);
            sprite.Color(startTime, Color4.Black);
        }

        private void AddMainLogo(double startTime = 74076)
        {
            var logoPath = "sb/logo/main_logo.png";
            var sprite = GetLayer("main logo 2").CreateSprite(logoPath);
            var bitmap = GetMapsetBitmap(logoPath);

            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + BeatDuration, bitmap.Height * 0.75, bitmap.Height * 0.0015);
            sprite.Fade(OsbEasing.None, startTime, startTime + BeatDuration / 2, 0, 1);

            sprite.Fade(OsbEasing.None, 78023 - BeatDuration * 2, 78023 + BeatDuration * 2, 1, 0);
        }

        private void AddMainLogoWithBottomText(double startTime = 3497, double endTime = 8549)
        {
            var logoPath = "sb/logo/main_logo2.png";
            var sprite = GetLayer("main logo").CreateSprite(logoPath, OsbOrigin.Centre, new Vector2(320, 225));
            var bitmap = GetMapsetBitmap(logoPath);

            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + BeatDuration * 1.5, bitmap.Height * 0.75, bitmap.Height * 0.0022);
            sprite.Fade(OsbEasing.None, startTime, startTime + BeatDuration, 0, 1);

            sprite.Scale(OsbEasing.OutExpo, endTime, endTime + BeatDuration * 2, bitmap.Height * 0.0022, 0);

            var bottomTextPath = "sb/logo/bottom_text.png";
            sprite = GetLayer("main logo").CreateSprite(bottomTextPath, OsbOrigin.Centre, new Vector2(320, 330));
            bitmap = GetMapsetBitmap(bottomTextPath);

            sprite.Scale(startTime + BeatDuration * 1.5, bitmap.Height * 0.017);
            sprite.Fade(startTime + BeatDuration * 1.5, startTime + BeatDuration * 3, 0, 1);
            sprite.Fade(endTime - BeatDuration, endTime, 1, 0);
        }
    }
}
