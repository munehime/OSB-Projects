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
    public class Girls : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            GirlsShowIn();
            GirlsPumpUp();
            GirlsMoveIn();
        }

        private void GirlsShowIn(double startTime = 8707)
        {
            AddGirl("asaga", startTime, 1.35, Math.PI / 15,
                new Vector2(-900, -30),
                new Vector2(30, 200),
                new Vector2(60, 200),
                new Vector2(320, 350)
                );

            AddGirl("chigara", startTime + BeatDuration * 1.5, 1.35, Math.PI / 120,
                new Vector2(900, 100),
                new Vector2(480, 300),
                new Vector2(460, 300),
                new Vector2(320, 340)
                );

            AddGirl("ava", startTime + BeatDuration * 3, 1.55, 0,
                new Vector2(-500, 200),
                new Vector2(160, 300),
                new Vector2(180, 300),
                new Vector2(320, 360)
                );

            AddGirl("sola", startTime + BeatDuration * 4.5, 1.9, 0,
                new Vector2(700, 200),
                new Vector2(360, 420),
                new Vector2(360, 420),
                new Vector2(320, 355)
                );
        }

        private void GirlsPumpUp(double startTime = 11865)
        {
            AddGirl2("sola", "uni_back_neutral", startTime, 1.75, Math.PI / 12,
                new Vector2(600, 1800),
                new Vector2(550, 410),
                new Vector2(854, 500)
                );

            AddGirl2("chigara", "uni_handsbehindback_neutral", startTime, 1.9, Math.PI / 20,
                new Vector2(600, 1600),
                new Vector2(410, 380),
                new Vector2(754, 500)
                );

            AddGirl2("ava", "hs_handhair_neutral", startTime, 2.5, -Math.PI / 100,
                new Vector2(-50, 1600),
                new Vector2(30, 600),
                new Vector2(-160, 1200)
                );

            AddGirl2("asaga", "uni_armsup_happy", startTime, 2.1, 0,
                new Vector2(260, 1200),
                new Vector2(280, 310),
                new Vector2(-107, 600)
            );
        }

        private void GirlsMoveIn(double startTime = 59392)
        {
            AddGirl3("ava", startTime, 2, -Math.PI / 100,
                new Vector2(-200, 300),
                new Vector2(200, 400),
                new Vector2(200, 1200)
            );

            AddGirl3("sola", startTime + BeatDuration, 1.75, 0,
                new Vector2(800, 240),
                new Vector2(450, 340),
                new Vector2(854, 1140)
                );

            AddGirl3("chigara", startTime + BeatDuration * 3, 0.9, Math.PI / 60,
                new Vector2(600, 1600),
                new Vector2(510, 420),
                new Vector2(754, 1220)
                );

            AddGirl3("asaga", startTime + BeatDuration * 4, 0.9, 0,
                new Vector2(-100, 1200),
                new Vector2(220, 340),
                new Vector2(-107, 1140)
            );
        }

        private void AddGirl(string character, double startTime, double size, double rotateAngle, Vector2 pos1, Vector2 pos2, Vector2 pos3, Vector2 pos4)
        {
            var charPath = $"sb/characters/{character}/main.png";
            var bitmap = GetMapsetBitmap(charPath);
            var sprite = GetLayer("girls").CreateSprite(charPath);
            var scale = 480.0 / bitmap.Height;

            if (rotateAngle != 0)
                sprite.Rotate(startTime, rotateAngle);

            sprite.MoveX(OsbEasing.OutQuart, startTime, startTime + BeatDuration, pos1.X, pos2.X);
            sprite.MoveY(OsbEasing.OutQuart, startTime, startTime + BeatDuration, pos1.Y, pos2.Y);
            sprite.Scale(OsbEasing.OutQuart, startTime, startTime + BeatDuration, scale * 5.5, scale * size);

            sprite.MoveX(startTime + BeatDuration, startTime + BeatDuration * 4, pos2.X, pos3.X);
            sprite.Scale(startTime + BeatDuration, startTime + BeatDuration * 4, scale * size, scale * (size * 0.925));

            sprite.MoveX(OsbEasing.InBack, startTime + BeatDuration * 4, startTime + BeatDuration * 5, pos3.X, pos4.X);
            sprite.MoveY(OsbEasing.InBack, startTime + BeatDuration * 4, startTime + BeatDuration * 5, pos3.Y, pos4.Y);
            sprite.Scale(OsbEasing.InBack, startTime + BeatDuration * 4, startTime + BeatDuration * 5, scale * (size * 0.925), 0);
        }

        private void AddGirl2(string character, string filename, double startTime, double size, double rotateAngle, Vector2 pos1, Vector2 pos2, Vector2 pos3)
        {
            var charPath = $"sb/characters/{character}/{character}_{filename}.png";
            var bitmap = GetMapsetBitmap(charPath);
            var sprite = GetLayer("girls").CreateSprite(charPath);
            var scale = 480.0 / bitmap.Height;

            if (rotateAngle != 0)
                sprite.Rotate(startTime, rotateAngle);

            sprite.MoveX(OsbEasing.OutQuart, startTime, startTime + BeatDuration, pos1.X, pos2.X);
            sprite.MoveY(OsbEasing.OutQuart, startTime, startTime + BeatDuration, pos1.Y, pos2.Y);
            sprite.Scale(OsbEasing.OutQuart, startTime, startTime + BeatDuration, scale * 5.5, scale * size);

            sprite.MoveX(OsbEasing.InBack, startTime + BeatDuration * 4, startTime + BeatDuration * 5, pos2.X, pos3.X);
            sprite.MoveY(OsbEasing.InBack, startTime + BeatDuration * 4, startTime + BeatDuration * 5, pos2.Y, pos3.Y);
            sprite.Scale(OsbEasing.InBack, startTime + BeatDuration * 4, startTime + BeatDuration * 5, scale * size, scale * size * 2);
        }

        private void AddGirl3(string character, double startTime, double size, double rotateAngle, Vector2 pos1, Vector2 pos2, Vector2 pos3)
        {
            var charPath = $"sb/characters/{character}/main.png";
            var bitmap = GetMapsetBitmap(charPath);
            var sprite = GetLayer("girls").CreateSprite(charPath);
            var scale = 480.0 / bitmap.Height;

            if (rotateAngle != 0)
                sprite.Rotate(startTime, rotateAngle);

            sprite.MoveX(OsbEasing.OutQuart, startTime, startTime + BeatDuration, pos1.X, pos2.X);
            sprite.MoveY(OsbEasing.OutQuart, startTime, startTime + BeatDuration, pos1.Y, pos2.Y);
            sprite.Scale(OsbEasing.OutQuart, startTime, startTime + BeatDuration, scale * 5.5, scale * size);

            sprite.MoveY(startTime + BeatDuration, 63655, pos2.Y, pos2.Y - 10);

            sprite.MoveY(OsbEasing.OutQuart, 63655, 63655 + BeatDuration * 2, pos2.Y - 10, pos3.Y);
        }
    }
}
