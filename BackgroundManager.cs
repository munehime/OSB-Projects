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
    public class BackgroundManager : StoryboardObjectGenerator
    {
        private Bitmap bitmap;
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            bitmap = GetMapsetBitmap(Beatmap.BackgroundPath);

            AddBG();
            AddBumpBG(new int[] { 18571, 27628, 36684, 45741 }, new int[] { 25364, 34420, 43477, 52534 });
            AddBumpBG(new int[] { 63854, 72911 }, new int[] { 70647, 79703 });
            AddBumpBG(new int[] { 109137, 118194 }, new int[] { 115930, 124986 });
        }

        private void AddBG()
        {
            var _bitmap = GetMapsetBitmap(Beatmap.BackgroundPath);
            var sprite = GetLayer("bg").CreateSprite(Beatmap.BackgroundPath);

            sprite.Scale(34, 480f / _bitmap.Height);
            sprite.Fade(34, 7250, 0, 0.7);
            sprite.Fade(18571, 0);
            sprite.Fade(55930, 0.7);
            sprite.Fade(63854, 0);
            sprite.Fade(83100, 0.7);
            sprite.Fade(109137, 0);
            sprite.Fade(128383, 0.7);
            sprite.Fade(145364, 0);

        }

        private void AddBumpBG(int[] times1, int[] times2)
        {
            var _bitmap = GetMapsetBitmap(Beatmap.BackgroundPath);
            var size = 480f / _bitmap.Height;
            var sprite = GetLayer("bump").CreateSprite(Beatmap.BackgroundPath);
            sprite.Scale(times1.First(), size);
            sprite.Fade(times1.First(), 0.7);

            foreach (var time in times1)
            {
                sprite.StartLoopGroup(time, 3);
                sprite.Scale(OsbEasing.Out, 0, BeatDuration * 0.375, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, BeatDuration * 0.375, BeatDuration * 0.75, size, size * 1.02f);
                sprite.Scale(OsbEasing.Out, BeatDuration * 0.75, BeatDuration * 1.125, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, BeatDuration * 1.125, BeatDuration * 1.5, size, size * 1.02f);
                sprite.Scale(OsbEasing.Out, BeatDuration * 1.5, BeatDuration * 2.75, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, BeatDuration * 2.75, BeatDuration * 4, size, size * 1.02f);
                sprite.EndGroup();
            }

            foreach (var time in times2)
            {
                sprite.Scale(OsbEasing.Out, time, time + BeatDuration * 0.375, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, time + BeatDuration * 0.375, time + BeatDuration * 0.75, size, size * 1.02f);
                sprite.Scale(OsbEasing.Out, time + BeatDuration * 0.75, time + BeatDuration * 1.125, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, time + BeatDuration * 1.125, time + BeatDuration * 1.5, size, size * 1.02f);
                sprite.Scale(OsbEasing.Out, time + BeatDuration * 1.5, time + BeatDuration * 1.75, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, time + BeatDuration * 1.75, time + BeatDuration * 2, size, size * 1.02f);
                sprite.Scale(OsbEasing.Out, time + BeatDuration * 2, time + BeatDuration * 2.5, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, time + BeatDuration * 2.5, time + BeatDuration * 3, size, size * 1.02f);
                sprite.Scale(OsbEasing.Out, time + BeatDuration * 3, time + BeatDuration * 3.5, size * 1.02f, size);
                sprite.Scale(OsbEasing.In, time + BeatDuration * 3.5, time + BeatDuration * 4, size, size * 1.02f);
            }

            sprite.Scale(OsbEasing.Out, times2.Last() + BeatDuration * 4, times2.Last() + BeatDuration * 6, size * 1.02f, size);
        }
    }
}
