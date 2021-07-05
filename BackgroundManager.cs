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
    public class BackgroundManager : StoryboardObjectGenerator
    {
        private double BeatDuration;

        [Configurable]
        public Color4 Color = Color4.White;

        private string[] BackgroundPaths = new string[] { "kukisHARD.png", "chigara.png", "ava.png", "sola.jpg" };

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            GenerateIntroBackground();
            AddMainLogoBackground();
            AddBackgrounds();
            AddBackgroundFloat(43918, 59076);
            AddSkyBackground();
        }

        private void GenerateIntroBackground(double startTime = 970, double endTime = 3181)
        {
            var bitmap = GetMapsetBitmap("sb/bg/sky.jpg");
            var scale = 480.0 / bitmap.Height;
            var sprite = GetLayer("intro").CreateSprite("sb/bg/sky.jpg");

            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + BeatDuration * 5.5, scale * 1.25, scale * 1.082);
            sprite.Scale(startTime + BeatDuration * 5.5, endTime, scale * 1.082, scale * 3.5);

            sprite.MoveY(OsbEasing.OutExpo, startTime, startTime + BeatDuration * 5.5, 50, 220);

            sprite.Fade(endTime, 1);
        }

        private void AddMainLogoBackground(double startTime = 3497, double endTime = 13602)
        {
            var bg = GetLayer("main logo").CreateSprite("sb/p.png");
            bg.ScaleVec(startTime, 854, 480);
            bg.Fade(endTime, 1);

            var position = new Vector2(-220, 240);
            for (int i = 0; i < 33; i++)
            {
                var sprite = GetLayer("main logo").CreateSprite("sb/p.png", OsbOrigin.CentreRight, position);
                sprite.Rotate(startTime, Math.PI / 6);
                sprite.ScaleVec(OsbEasing.OutCirc, startTime, startTime + BeatDuration, 0, 600, 20, 600);
                sprite.ScaleVec(startTime + BeatDuration * 16, startTime + BeatDuration * 17, 20, 600, 0, 600);
                sprite.Color(startTime, "#A0B4E9");

                position.X += 35;
            }
        }

        private void AddBackgrounds()
        {
            var scale = GetScale(BackgroundPaths[0]);
            var asagaBack = GetLayer("").CreateSprite(BackgroundPaths[0]);
            var asaga = GetLayer("").CreateSprite(BackgroundPaths[0]);
            asagaBack.Scale(13602, scale * 1.2);
            asagaBack.Fade(OsbEasing.OutCubic, 13602, 13602 + BeatDuration * 4, 1, 0);

            asaga.Scale(13602, asagaBack.ScaleAt(13602).X);
            asaga.Additive(13602, 13602 + BeatDuration * 4);
            asaga.Move(13602, 21497, 400, 191, 280, 264);
            asagaBack.Move(13602, 23707, asaga.PositionAt(13602), asaga.PositionAt(23707));

            asaga.Scale(OsbEasing.OutQuad, 21181, 21497, asaga.ScaleAt(21181).X, scale * 1.1);
            asaga.Move(OsbEasing.OutExpo, 21497, 23707, asaga.PositionAt(21497), 100, 480);
            asaga.Scale(OsbEasing.OutExpo, 21497, 23707, asaga.ScaleAt(21497).X, scale * 2.2);

            scale = GetScale(BackgroundPaths[1]);
            var chigara = GetLayer("").CreateSprite(BackgroundPaths[1]);
            chigara.Move(OsbEasing.OutExpo, 23707, 23707 + BeatDuration * 8, 745, 450, 400, 288);
            chigara.Scale(OsbEasing.OutExpo, 23707, 23707 + BeatDuration * 8, scale * 2, scale * 1.2);
            chigara.Move(OsbEasing.InSine, 23707 + BeatDuration * 8, 30655, chigara.PositionAt(23707 + BeatDuration * 8), 300, 220);

            chigara.Move(OsbEasing.OutQuad, 30655, 31128, chigara.PositionAt(30655), 550, 360);
            chigara.Scale(OsbEasing.OutQuad, 30655, 31128, chigara.ScaleAt(30655).X, scale * 1.6);

            chigara.Move(OsbEasing.OutExpo, 31128, 33023, chigara.PositionAt(31128), 190, 300);
            chigara.Scale(OsbEasing.OutQuad, 31128, 32549, chigara.ScaleAt(31128).X, scale * 1.3);

            scale = GetScale(BackgroundPaths[2]);
            var ava = GetLayer("").CreateSprite(BackgroundPaths[2]);
            ava.Scale(33813, scale * 1.3);
            ava.Move(OsbEasing.OutExpo, 33813, 33813 + BeatDuration * 4, 300, 280, 340, 200);
            ava.Move(OsbEasing.InExpo, 33813 + BeatDuration * 4, 38392, ava.PositionAt(33813 + BeatDuration * 4), 400, 182);

            ava.Move(OsbEasing.OutExpo, 38392, 38865, ava.PositionAt(38392), 800, 0);
            ava.Fade(OsbEasing.OutExpo, 38392, 38865, 1, 0);

            scale = GetScale(BackgroundPaths[3]);
            var sola = GetLayer("").CreateSprite(BackgroundPaths[3]);
            sola.Scale(OsbEasing.OutQuad, 38865, 38865 + BeatDuration * 4, scale * 1.3, scale * 1.25);
            sola.Move(OsbEasing.OutExpo, 38865, 38865 + BeatDuration * 4, 300, 280, 340, 200);

            sola.Move(OsbEasing.InExpo, 38865 + BeatDuration * 4, 41076, sola.PositionAt(38865 + BeatDuration * 4), 320, 240);
            sola.Scale(OsbEasing.InExpo, 38865 + BeatDuration * 4, 41076, sola.ScaleAt(38865 + BeatDuration * 4).X, scale * 1.1);

            sola.Scale(OsbEasing.OutExpo, 41076, 41392, sola.ScaleAt(41076).X, scale * 1.5);
            sola.Rotate(OsbEasing.OutExpo, 41076, 41392, 0, -Math.PI / 2);
        }

        private void AddBackgroundFloat(double startTime, double endTime)
        {
            Vector2 moveDistance = new Vector2(Random(-10, 10f), Random(-10, 10f));
            Vector2 startPos = new Vector2(320, 240);
            Vector2 endPos = new Vector2(startPos.X + moveDistance.X, startPos.Y + moveDistance.Y);
            double startRadian = 0;
            double endRadian = startRadian + Random(-Math.PI / 120, Math.PI / 120);
            int a = 1;

            var scale = GetScale(Beatmap.BackgroundPath);
            var sprite = GetLayer("float").CreateSprite(Beatmap.BackgroundPath, OsbOrigin.Centre, startPos);
            sprite.Scale(startTime, scale * 1.15);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);

            for (double starttime = startTime; starttime < endTime; starttime += BeatDuration * 4)
            {
                sprite.Move(OsbEasing.InOutSine, starttime, starttime + BeatDuration * 4, startPos, endPos);
                sprite.Rotate(OsbEasing.InOutSine, starttime, starttime + BeatDuration * 4, startRadian, endRadian);

                startPos = endPos;
                endPos = startPos + new Vector2(Random(-10, 10f), Random(-10, 10f));
                startRadian = endRadian;
                endRadian = startRadian + Random(0, Math.PI / 120) * Math.Pow(-1, ++a);
            }
        }

        private void AddSkyBackground(double startTime = 59076)
        {
            var bitmap = GetMapsetBitmap("sb/bg/sky.jpg");
            var scale = 480.0 / bitmap.Height;
            var sprite = GetLayer("sky").CreateSprite("sb/bg/sky.jpg");

            sprite.Scale(startTime, scale * 1.3);
            sprite.MoveY(startTime, startTime + BeatDuration * 12, 240, 200);
            sprite.MoveY(OsbEasing.OutQuart, startTime + BeatDuration * 14.5, startTime + BeatDuration * 16, 200, 480);
            sprite.Scale(OsbEasing.OutQuart, startTime + BeatDuration * 14.5, startTime + BeatDuration * 16, scale * 1.3, scale * 2);

            sprite.Scale(startTime + BeatDuration * 16, scale * 1.8);
            sprite.MoveY(startTime + BeatDuration * 16, startTime + BeatDuration * 40, 420, 200);
        }

        private double GetScale(string path)
        {
            var bitmap = GetMapsetBitmap(path);
            var ratio = (double)bitmap.Width / bitmap.Height;

            if (ratio != (16.0 / 9))
                return 854.0 / bitmap.Width;
            return 480.0 / bitmap.Height;
        }
    }
}
