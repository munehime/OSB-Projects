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
    public class Transtions : StoryboardObjectGenerator
    {
        private string path = "sb/pixel.png";

        public override void Generate()
        {
            Flash();
            Transtion1(688, 1374);
            Transtion2(10288, 11659, 4, 11659);
            Transtion2(235202, 236574, 4, 236574);
            Transtion3(25031, 25374);
            Transtion4(46631, 47316);
            Transtion5(67888, 69259);
            Transtion5(177602, 178974);
            Transtion6(154202, 154374);
            Transtion7(156688, 157031);
            Transtion8(225259, 225602);
            Blossom(11059, 11574);
            Blossom(236059, 236488);
        }
        
        private void Transtion1(int startTime, int endTime)
        {
            var origin = OsbOrigin.TopCentre;
            var position = new Vector2(320, 0);
            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 2").CreateSprite(path, origin, position);

                sprite.ScaleVec(OsbEasing.OutExpo, startTime, endTime, 854, 240, 854, 0);
                sprite.Color(startTime, Color4.Black);
                sprite.Fade(startTime, 1);
                sprite.Fade(endTime, 1);

                origin = OsbOrigin.BottomCentre;
                position.Y = 480;
            }
        }

        private void Transtion2(int startTime, int endTime, int layerCount, int startTime2)
        {
            var width = 854f / layerCount;

            //Up and down
            var origin = OsbOrigin.BottomCentre;
            var position = new Vector2(747 - width * 0.5f, 480);
            var delay = 0d;

            for (int i = 0; i < layerCount; i++)
            {
                var sprite = GetLayer("transtion 3").CreateSprite(path, origin, position);

                sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, startTime + delay + 350, width, 0, width, 480);
                sprite.Fade(startTime + delay, 1);
                sprite.Fade(endTime, 1);

                if (i % 2 == 0)
                {
                    origin = OsbOrigin.TopCentre;
                    position.Y = 0;
                }
                else
                {
                    origin = OsbOrigin.BottomCentre;
                    position.Y = 480;
                };

                position.X -= width;
                delay += Beatmap.GetTimingPointAt(startTime).BeatDuration / 2;
            }

            //Sides
            origin = OsbOrigin.CentreLeft;
            position = new Vector2(-107, 240);

            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 3").CreateSprite(path, origin, position);

                sprite.ScaleVec(OsbEasing.OutExpo, startTime2, startTime2 + 500, 427, 480, 0, 480);
                sprite.Fade(startTime2, 1);
                sprite.Fade(startTime2 + 500, 1);

                origin = OsbOrigin.CentreRight;
                position.X = 747;
            }
        }

        private void Transtion3(int startTime, int endTime)
        {
            var origin = OsbOrigin.TopCentre;
            var position = new Vector2(320, 0);
            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 4").CreateSprite(path, origin, position);

                sprite.ScaleVec(OsbEasing.OutExpo, startTime, endTime - 200, 854, 0, 854, 220);
                sprite.ScaleVec(OsbEasing.InSine, endTime - 200, endTime, 854, 220, 854, 240);
                sprite.Fade(endTime, endTime + 400, 1, 0);

                origin = OsbOrigin.BottomCentre;
                position.Y = 480;
            }
        }

        private void Transtion4(int startTime, int endTime)
        {
            var origin = OsbOrigin.CentreLeft;
            var position = new Vector2(-107, 240);

            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 5").CreateSprite(path, origin, position);

                sprite.Color(startTime, Color4.Black);
                sprite.ScaleVec(OsbEasing.InExpo, startTime, startTime + Beatmap.GetTimingPointAt(startTime).BeatDuration, 0, 480, 427, 480);
                sprite.Fade(OsbEasing.In, startTime, startTime + Beatmap.GetTimingPointAt(startTime).BeatDuration, 0, 1);

                sprite.ScaleVec(OsbEasing.OutExpo, endTime, endTime + Beatmap.GetTimingPointAt(startTime).BeatDuration, 854, 480, 854, 0);

                origin = OsbOrigin.CentreRight;
                position.X = 747;
            }
        }

        private void Transtion5(int startTime, int endTime)
        {
            var origin = OsbOrigin.CentreLeft;
            var position = new Vector2(-107, 240);

            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 6").CreateSprite(path, origin, position);
                var delay = 0d;
                var scaleX = 0d;

                for (int j = 0; j < 3; j++)
                {
                    sprite.ScaleVec(OsbEasing.OutQuart, startTime + delay, startTime + delay + Beatmap.GetTimingPointAt((int)startTime).BeatDuration, scaleX, 480, scaleX + 427f / 3, 480);

                    delay += Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
                    scaleX += 427f / 3;
                }

                sprite.Fade(startTime, 1);
                sprite.Fade(endTime, 1);

                origin = OsbOrigin.CentreRight;
                position.X = 747;
            }

            origin = OsbOrigin.TopCentre;
            position = new Vector2(-107, 0);
            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 6").CreateSprite(path, origin, position);
                sprite.ScaleVec(OsbEasing.OutExpo, endTime, endTime + 700, 810, 486, 810, 0);
                sprite.Rotate(endTime, MathHelper.DegreesToRadians(-50));

                sprite.Fade(endTime, 1);
                sprite.Fade(endTime + 500, 1);

                origin = OsbOrigin.BottomCentre;
                position = new Vector2(747, 480);
            }
        }

        private void Transtion6(int startTime, int endTime)
        {
            var origin = OsbOrigin.TopCentre;
            var position = new Vector2(320, 0);
            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 7").CreateSprite(path, origin, position);

                sprite.ScaleVec(OsbEasing.OutExpo, startTime, endTime, 854, 0, 854, 240);
                sprite.Color(startTime, Color4.Black);
                sprite.Fade(OsbEasing.OutExpo, startTime, endTime, 0, 1);


                origin = OsbOrigin.BottomCentre;
                position.Y = 480;
            }

            origin = OsbOrigin.CentreLeft;
            position = new Vector2(-107, 240);
        }

        private void Transtion7(int startTime, int endTime)
        {
            var origin = OsbOrigin.CentreLeft; ;
            var position = new Vector2(-107, 240);

            for (int i = 0; i < 2; i++)
            {
                var sprite = GetLayer("transtion 8").CreateSprite(path, origin, position);
                sprite.ScaleVec(OsbEasing.OutExpo, endTime, endTime + 750, 427, 480, 0, 480);
                sprite.Color(endTime, Color4.Black);
                sprite.Fade(endTime, 1);
                sprite.Fade(endTime + 750, 1);

                origin = OsbOrigin.CentreRight;
                position.X = 747;
            }
        }

        private void Transtion8(int startTime, int endTime)
        {
            var position = new Vector2(-107, 240);
            var sprite = GetLayer("transtion 9").CreateSprite("sb/pixel.png", OsbOrigin.CentreLeft, position);
            sprite.ScaleVec(OsbEasing.OutExpo, startTime, endTime, 0, 480, 854, 480);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 1);


            position = new Vector2(747, 240);
            sprite = GetLayer("transtion 9").CreateSprite("sb/pixel.png", OsbOrigin.CentreRight, position);
            sprite.ScaleVec(OsbEasing.OutExpo, endTime, endTime + (endTime - startTime), 854, 480, 0, 480);
            sprite.Fade(endTime, 1);
            sprite.Fade(endTime + (endTime - startTime), 1);
        }

        private void Blossom(int startTime, int endTime)
        {
            double rad = -0.5 * Math.PI;
            double delay = 0;
            for (int i = 0; i < 6; i++)
            {
                double x = 150 * Math.Cos(rad) + 320;
                double y = 150 * Math.Sin(rad) + 240;

                var sprite = GetLayer("blossom 1").CreateSprite("sb/blossom.png", OsbOrigin.Centre, new Vector2((float)x, (float)y));
                sprite.Rotate(startTime + delay, rad + Math.PI / 2);
                sprite.Scale(OsbEasing.OutBack, startTime + delay - 200, startTime + delay, 0, 0.5);
                sprite.Color(startTime + delay, new Color4(255, 198, 248, 255));
                sprite.Fade(startTime + delay - 200, startTime + delay, 0, 1);
                sprite.Fade(endTime + Beatmap.GetTimingPointAt(endTime).BeatDuration / 2 - 200, endTime + Beatmap.GetTimingPointAt(endTime).BeatDuration / 2 - 40, 1, 0);

                sprite.Move(OsbEasing.InBack, endTime, endTime + Beatmap.GetTimingPointAt(endTime).BeatDuration / 2, x, y, 360, 240);

                rad += (Math.PI * 2) / 6;
                delay += Beatmap.GetTimingPointAt(startTime).BeatDuration / 4;
            }
        }

        private void Flash()
        {
            var time = new int[] { 22631, 91202, 247545 };
            var sprite = GetLayer("flash").CreateSprite(path, OsbOrigin.Centre);
            sprite.ScaleVec(time[0], 854, 480);
            foreach (var t in time)
                sprite.Fade(t, t + 1000, 1, 0);
        }
    }
}
