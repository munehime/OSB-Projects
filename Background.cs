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
using System.Drawing;
using System.Linq;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        private double beatDuration;
        private Bitmap bitmap;

        private double floatDistance = 10;
        private double bgScale = 1.125;

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            bitmap = GetMapsetBitmap(Beatmap.BackgroundPath);

            AddBackground();
            AddBlackAndWhiteBackground();
            AddBlurBackground();
            AddVig();

            BGFloat(193310, 223310);
        }

        private void AddBackground()
        {
            var sprite = GetLayer("bg").CreateSprite(Beatmap.BackgroundPath);

            var time = new int[] { 28329, 43310, 58310, 133310, 148310, 193310, 223310, 247685, 255185, 315287 };
            sprite.Scale(28329, 480f / bitmap.Height);
            sprite.Rotate(28329, 0);
            for (int i = 0; i < time.Count() - 1; i += 2)
            {
                sprite.Fade(time[i] - beatDuration / 4, time[i], 0, 1);
                sprite.Fade(time[i + 1], 0);
            }

            sprite.Scale(OsbEasing.OutExpo, 118310, 118310 + beatDuration * 8, (480f / bitmap.Height) * 1.1f, 480f / bitmap.Height);
            sprite.Scale(OsbEasing.OutExpo, 223310, 223310 + beatDuration * 8, (480f / bitmap.Height) * 1.85f, 480f / bitmap.Height);
            sprite.Rotate(OsbEasing.OutExpo, 223310, 223310 + beatDuration * 8, Math.PI / 6, 0);

            sprite.Fade(270185, 0);
            sprite.Fade(283310, 1);
            sprite.Fade(OsbEasing.OutSine, 290810, 298284, 1, 0.15);
            sprite.Fade(298284, 309534, 0.15, 0);
        }

        private void AddBlackAndWhiteBackground()
        {
            var sprite = GetLayer("bw").CreateSprite("sb/bg/bw.jpg", OsbOrigin.Centre);
            sprite.Scale(30, 480f / bitmap.Height);
            sprite.Fade(30, 4249, 0, 0.65);
            sprite.Fade(28329, 0);
            sprite.Fade(238310, 240185, 0, 0.75);
            sprite.Fade(247685, 0.75);

        }

        private void AddBlurBackground()
        {
            var sprite = GetLayer("blur").CreateSprite("sb/bg/blur.jpg");

            var time = new int[] { 43310, 58310, 133310, 148310, 247685, 255185 };
            sprite.Scale(43310, 480f / bitmap.Height);

            for (int i = 0; i < time.Count() - 1; i += 2)
            {
                sprite.Fade(time[i] - beatDuration / 4, time[i], 0, 1);
                sprite.Fade(time[i + 1], 0);
            }
        }

        private void AddVig()
        {
            var sprite = GetLayer("vig").CreateSprite("sb/vig.png");

            sprite.Scale(0, 480f / 1080);
            sprite.Fade(0, 1);
            sprite.Fade(AudioDuration, 1);
        }

        private void BGFloat(double startTime, double endTime)
        {
            var x = 320d;
            var y = 240d;
            var rad = 0d;
            var x1 = x + Random(-floatDistance, floatDistance);
            var y1 = y + Random(-floatDistance, floatDistance);
            var rad1 = rad + Random(-0.020, 0.020);
            var a = 1;

            var sprite = GetLayer("float").CreateSprite("sb/bg/blur.jpg", OsbOrigin.Centre);
            sprite.Scale(startTime, (480f / bitmap.Height) * bgScale);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);

            var timestep = beatDuration / 0.25;
            for (var starttime = startTime; starttime <= endTime - timestep; starttime += timestep)
            {
                sprite.Move(OsbEasing.InOutSine, starttime, starttime + timestep, x, y, x1, y1);
                sprite.Rotate(OsbEasing.InOutSine, starttime, starttime + timestep, rad, rad1);
                x = x1;
                y = y1;
                rad = rad1;
                a++;
                x1 = x + Random(-10, 10);
                y1 = y + Random(-10, 10);
                rad1 = rad + Random(0, 0.020) * Math.Pow(-1, a);
            }
        }
    }
}
