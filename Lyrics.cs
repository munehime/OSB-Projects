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
using System.IO;
using Newtonsoft.Json;

namespace StorybrewScripts
{
    public class LyricsData
    {
        public Language Sentence { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }

    public class Language
    {
        public string Russian { get; set; }
        public string English { get; set; }
    }

    public class Lyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public string LyricsFilePath = "lyrics.json";

        [Configurable]
        public string FontName = "Raleway-Light.ttf";
        [Configurable]
        public int FontSize = 60;
        [Configurable]
        public float FontScale = 0.3f;

        [Configurable]
        public Vector2 Center = new Vector2(320, 240);

        private FontGenerator Font;
        private double BeatDuration;

        public override void Generate()
        {
            Font = SetupFont();
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            using (Stream stream = OpenProjectFile(LyricsFilePath))
            using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
            {
                string data = reader.ReadToEnd();
                List<LyricsData> lines = JsonConvert.DeserializeObject<List<LyricsData>>(data);

                foreach (LyricsData line in lines)
                    GenerateLyrics(line);
            }

            FinalWords("...Sasha... I... I want to... Sing...", 269475, 274808);
        }

        private void GenerateLyrics(LyricsData line)
        {
            float maxLineWidth = 0;
            float firstLinePositionY = 0;
            float secondLinePositionY = 0;
            GenerateLyrics(line.Sentence.Russian, line.StartTime, line.EndTime, ref maxLineWidth, ref firstLinePositionY);
            GenerateLyrics(line.Sentence.English, line.StartTime, line.EndTime, ref maxLineWidth, ref secondLinePositionY, true);

            var paddingX = 35;
            var sprite = GetLayer("line").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(320, firstLinePositionY + (secondLinePositionY - firstLinePositionY) * 0.5f));
            sprite.ScaleVec(OsbEasing.OutExpo, line.StartTime - BeatDuration * 2, line.StartTime, 0, 2, maxLineWidth + paddingX, 2);
            sprite.ScaleVec(OsbEasing.InExpo, line.EndTime - BeatDuration * 2, line.EndTime, maxLineWidth + paddingX, 2, 0, 2);
        }

        private void GenerateLyrics(string Sentence, double StartTime, double EndTime, ref float maxLineWidth, ref float linePositionY, bool isEnglish = false)
        {
            float lineWidth = 0f;
            float lineHeight = 0f;
            foreach (var letter in Sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * FontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight);
            }
            maxLineWidth = Math.Max(maxLineWidth, lineWidth);

            var letterX = Center.X - lineWidth * 0.5f;
            var letterY = Center.Y + (isEnglish ? 1 : -1) * lineHeight * 0.15f;
            var delay = 0d;

            foreach (var letter in Sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, letterY) + texture.OffsetFor(OsbOrigin.Centre) * FontScale;
                    var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    var direction = Random(0, 2);

                    sprite.MoveX(OsbEasing.OutQuad, StartTime - BeatDuration * 2 + delay, StartTime + delay, position.X + 150, position.X + 10);
                    sprite.MoveX(OsbEasing.None, StartTime + delay, EndTime + delay, position.X + 10, position.X - 10);
                    sprite.MoveY(OsbEasing.InQuad, EndTime - BeatDuration * 2 + delay, EndTime + delay, position.Y, position.Y + (isEnglish ? -1 : 1) * 12);
                    sprite.Fade(StartTime - BeatDuration * 2 + delay, StartTime + delay, 0, 1);
                    sprite.Fade(EndTime - BeatDuration * 2 + delay, EndTime + delay, 1, 0);
                    sprite.Scale(StartTime - BeatDuration * 2 + delay, FontScale);

                    linePositionY = Math.Max(linePositionY, position.Y);
                    delay += 50;
                }

                letterX += texture.BaseWidth * FontScale;
            }
        }

        private void FinalWords(string sentence, double startTime, double endTime)
        {
            float fontScale = 0.4f;
            float lineWidth = 0;
            foreach (var letter in sentence)
                lineWidth += Font.GetTexture(letter.ToString()).BaseWidth * fontScale;


            var letterX = 320 - lineWidth * 0.5f;
            var delay = 0d;

            foreach (var letter in sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, 220) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    var direction = Random(0, 2);

                    sprite.MoveX(OsbEasing.OutQuad, startTime - BeatDuration * 2 + delay, startTime + delay, position.X + 150, position.X + 20);
                    sprite.MoveX(OsbEasing.None, startTime + delay, endTime + delay, position.X + 20, position.X - 20);
                    sprite.MoveY(OsbEasing.InQuad, endTime - BeatDuration * 2 + delay, endTime + delay, position.Y, position.Y + 25);
                    sprite.Fade(startTime - BeatDuration * 2 + delay, startTime + delay, 0, 1);
                    sprite.Fade(endTime - BeatDuration * 2 + delay, endTime + delay, 1, 0);
                    sprite.Scale(startTime - BeatDuration * 2 + delay, fontScale);

                    delay += 50;
                }

                letterX += texture.BaseWidth * fontScale;
            }
        }

        FontGenerator SetupFont()
        {
            return LoadFont("sb/f", new FontDescription()
            {
                FontPath = FontName,
                FontSize = FontSize,
                Color = Color4.White
            });
        }
    }
}
