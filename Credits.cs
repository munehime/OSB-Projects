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

        private enum Mappers
        {
            olsonn = 0,
            Kuki = 1,
            Roda = 2,
        }

        private FontGenerator Font;
        private double BeatDuration;

        public override void Generate()
        {
            Font = SetupFont();
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            GenerateText("myu - Stardust Tears", 64128, 71707, new Vector2(320, 180));
            GenerateText("Mapset: olsonn", 65234, 71707, new Vector2(320, 210));
            switch (Beatmap.Name)
            {
                case "Kuki's Hard":
                    GenerateText("Hard: Kuki", 66655, 71707, new Vector2(320, 240));
                    break;

                case "Roda's Insane":
                    GenerateText("Insane: Roda", 66655, 71707, new Vector2(320, 240));
                    break;

                default:
                    var beatmapInfo = Beatmap.ToString().Split('\'');
                    GenerateText($"{beatmapInfo[0].Replace("s ", "")}: olsonn", 66655, 71707, new Vector2(320, 240));
                    break;
            }
            GenerateText("Storyboard: Ningguang", 67918, 71707, new Vector2(320, 270));

            AddFlash();
        }

        private void GenerateText(string sentence, double startTime, double endTime, Vector2 letterPos)
        {
            float fontScale = 0.35f;
            float lineWidth = 0;
            foreach (var letter in sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.Width * fontScale;
            }

            float letterX = letterPos.X - lineWidth * 0.5f;
            foreach (var letter in sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, letterPos.Y) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprite.Scale(startTime, fontScale);
                    sprite.Move(OsbEasing.OutExpo, startTime, startTime + BeatDuration * 6, new Vector2(320, position.Y), position.X, position.Y);
                    sprite.Fade(startTime, startTime + BeatDuration, 0, 1);
                    sprite.Fade(endTime, 1);
                }

                letterX += texture.Width * fontScale;
            }
        }

        private void AddFlash(double startTime = 63655)
        {
            var sprite = GetLayer("flash").CreateSprite("sb/p.png");
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, startTime + BeatDuration * 1.5, 0, 1);
            sprite.Fade(startTime + BeatDuration * 1.5, startTime + BeatDuration * 4.5, 1, 0);
            sprite.Fade(startTime + BeatDuration * 21.5, startTime + BeatDuration * 25.5, 0, 1);
            sprite.Fade(startTime + BeatDuration * 37, startTime + BeatDuration * 41, 1, 0);
        }

        private FontGenerator SetupFont()
        {
            return LoadFont("sb/f/c", new FontDescription()
            {
                FontPath = "SourceHanSerif-Regular.otf",
                FontSize = 60,
                Color = Color4.White
            });
        }
    }
}
