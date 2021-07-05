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
        public string sentence { get; set; }
        public double startTime { get; set; }
        public double endTime { get; set; }
    }

    public class Lyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public string LyricsFilePath = "lyrics.json";
        [Configurable]
        public string FontName = "SourceHanSerif-Regular.otf";
        [Configurable]
        public int FontSize = 60;
        [Configurable]
        public float FontScale = 0.3f;

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
                    GenerateText(line);
            }
        }

        private void GenerateText(LyricsData line)
        {
            float lineWidth = 0f;
            float lineHeight = 0f;
            foreach (var letter in line.sentence)
            {
                FontTexture texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * FontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
            }

            float letterX = 320 - lineWidth * 0.5f;
            double delay = 0d;
            bool hasBox = false;
            foreach (var letter in line.sentence)
            {
                FontTexture texture = Font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    Vector2 position = new Vector2(letterX, 410) + texture.OffsetFor(OsbOrigin.Centre) * FontScale;
                    if (!hasBox)
                    {
                        var box = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(320, position.Y));
                        box.ScaleVec(OsbEasing.OutExpo, line.startTime, line.startTime + BeatDuration * 2, 0, lineHeight + 5, lineWidth + 20, lineHeight + 5);
                        box.ScaleVec(OsbEasing.InExpo, line.endTime - BeatDuration * 2, line.endTime, lineWidth + 20, lineHeight + 5, 0, lineHeight + 5);
                        box.Fade(line.startTime, 0.8);
                        box.Color(line.startTime, Color4.Black);

                        hasBox = true;
                    }

                    OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprite.Scale(line.startTime + delay, FontScale);
                    sprite.Fade(line.startTime + delay, line.startTime + delay + BeatDuration, 0, 1);
                    sprite.Fade(line.endTime - BeatDuration, line.endTime, 1, 0);

                    delay += BeatDuration / 8;
                }

                letterX += texture.BaseWidth * FontScale;
            }
        }

        private FontGenerator SetupFont()
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
