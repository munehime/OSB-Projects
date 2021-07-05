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
    public class TransitionsManager : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            ScaleInCircle();
            OpenTransition();
            CloseTransition();
            BackgroundTransition();
            AddFlash();
        }

        private void OpenTransition(double startTime = 971)
        {
            OsbSprite sprite;
            for (var i = 0; i < 2; i++)
            {
                sprite = GetLayer("open").CreateSprite("sb/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(320, 480 * (i % 2)));
                sprite.Fade(-BeatDuration * 8, -BeatDuration * 4, 0, 1);
                sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + BeatDuration * 8, 854, 240, 854, 0);
            }

            var bitmap = GetMapsetBitmap("sb/cir.png");
            var scale = 985.0 / bitmap.Height;
            sprite = GetLayer("open").CreateSprite("sb/cir.png");
            sprite.Scale(OsbEasing.OutQuad, startTime + BeatDuration * 6, startTime + BeatDuration * 8, 0.35, scale * 1.35);
            sprite.Color(startTime + BeatDuration * 6, "#FF5647");

            sprite = GetLayer("open").CreateSprite("sb/cir.png");
            sprite.Scale(OsbEasing.OutQuad, startTime + BeatDuration * 6, startTime + BeatDuration * 8, 0.3, scale * 1.3);
            sprite.Color(startTime + BeatDuration * 6, "#FFAC48");

            sprite = GetLayer("open").CreateSprite("sb/cir.png");
            sprite.Scale(OsbEasing.OutQuad, startTime + BeatDuration * 6.2, startTime + BeatDuration * 8.2, 0.12, scale * 1.12);
            sprite.Color(startTime + BeatDuration * 6, "#FF6873");

            sprite = GetLayer("open").CreateSprite("sb/cir.png");
            sprite.Scale(OsbEasing.OutQuad, startTime + BeatDuration * 6.2, startTime + BeatDuration * 8.2, 0, scale);
        }

        private void CloseTransition(double startTime = 58760)
        {
            OsbSprite sprite;
            for (var i = 0; i < 2; i++)
            {
                sprite = GetLayer("close").CreateSprite("sb/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(320, 480 * (i % 2)));
                sprite.ScaleVec(OsbEasing.InExpo, startTime, startTime + BeatDuration, 854, 0, 854, 240);
                sprite.Fade(startTime + BeatDuration, startTime + BeatDuration * 3, 1, 0);
            }
        }

        private void ScaleInCircle(double startTime = 13602)
        {
            var sprite = GetLayer("circle").CreateSprite("sb/cir.png");
            var size = 980.0 / GetMapsetBitmap("sb/cir.png").Height;
            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + BeatDuration * 2, size, 0);
            sprite.Fade(startTime, startTime + BeatDuration * 2, 1, 0);
            sprite.Color(startTime, Color4.Black);
        }

        private void BackgroundTransition()
        {
            var sprite = GetLayer("bg trans").CreateSprite("sb/p.png");
            sprite.Move(OsbEasing.OutExpo, 22444, 22444 + BeatDuration, 320, 480, 320, 240);
            sprite.ScaleVec(OsbEasing.OutExpo, 22444, 22444 + BeatDuration, 0, 0, 100, 100);
            sprite.Rotate(OsbEasing.Out, 22444, 22444 + BeatDuration * 2, 0, 11 * Math.PI / 6);

            sprite.Move(OsbEasing.OutBack, 22760, 22760 + BeatDuration * 1.5, 320, 240, 55, 100);
            sprite.ScaleVec(OsbEasing.OutExpo, 22760, 22760 + BeatDuration * 1.5, 100, 100, 440, 20);

            sprite.Move(OsbEasing.OutQuad, 22760 + BeatDuration * 1.5, 22760 + BeatDuration * 2, 55, 100, 320, 490);
            sprite.ScaleVec(OsbEasing.OutExpo, 22760 + BeatDuration * 1.5, 22760 + BeatDuration * 2, 440, 20, 1800, 10);
            sprite.Rotate(OsbEasing.Out, 22760 + BeatDuration * 1.5, 22760 + BeatDuration * 2, 11 * Math.PI / 6, 2 * Math.PI);

            sprite = GetLayer("bg trans").CreateSprite("sb/p.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            sprite.ScaleVec(OsbEasing.OutExpo, 22760 + BeatDuration * 2, 22760 + BeatDuration * 3, 854, 0, 854, 480);
            sprite.Fade(OsbEasing.OutExpo, 23707, 23707 + BeatDuration * 8, 1, 0);

            var spriteHeight = 480.0 / 3;
            var posY = (float)spriteHeight * 0.5f;
            for (double time = 32549; time < 33023d; time += BeatDuration / 2)
            {
                sprite = GetLayer("bg trans").CreateSprite("sb/p.png", OsbOrigin.CentreRight, new Vector2(747, posY));
                sprite.ScaleVec(OsbEasing.OutExpo, time, 33023, 0, spriteHeight, 854, spriteHeight);

                posY += (float)spriteHeight;
            }
            sprite = GetLayer("bg trans").CreateSprite("sb/p.png");
            sprite.ScaleVec(OsbEasing.OutExpo, 33023, 33497, 854, 854, 100, 100);
            sprite.ScaleVec(OsbEasing.InBack, 33497, 33813, 100, 100, 854, 854);
            sprite.Rotate(OsbEasing.Out, 33023, 33813, 0, -Math.PI);
            sprite.Fade(33813, 33813 + BeatDuration * 2, 1, 0);

            var posX = -120f;
            for (double time = 38392; time < 38865; time += BeatDuration / 4)
            {
                sprite = GetLayer("bg trans").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(posX, 240));
                sprite.ScaleVec(OsbEasing.OutExpo, time, 38865, 0, 500, 900.0 / 5, 500);
                sprite.Rotate(time, 0.1);

                posX += 900f / 5;
            }
            sprite = GetLayer("bg trans").CreateSprite("sb/p.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            sprite.ScaleVec(38865, 854, 480);
            sprite.Fade(38865, 38865 + BeatDuration, 1, 0);

            for (int i = 0; i < 2; i++)
            {
                sprite = GetLayer("bg trans").CreateSprite("sb/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(320, 480 * (i % 2)));
                sprite.ScaleVec(OsbEasing.OutExpo, 41076, 41076 + BeatDuration, 854, 0, 854, 240);
                sprite.Fade(43918, 43918 + BeatDuration * 2, 1, 0);
            }
        }

        private void AddFlash()
        {
            var times = new double[] { 43918 };
            var sprite = GetLayer("flash").CreateSprite("sb/p.png");
            sprite.ScaleVec(times[0], 854, 480);
            foreach (var time in times)
            {
                sprite.Fade(time, time + BeatDuration * 2, 1, 0);
            }
        }
    }
}
