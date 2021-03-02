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
    public class LyricsJson
    {
        public string sentence { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public Vector2 position { get; set; }
    }

    public class Lyrics : StoryboardObjectGenerator
    {
        private double beatDuration;
        private FontGenerator font;

        private float fontScale = 0.25f;

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            font = SetupFont();
            
            var lines = JsonConvert.DeserializeObject<List<LyricsJson>>(File.ReadAllText($"{ProjectPath}/lyrics.json"));
            foreach (var line in lines)
            {
                GenerateText(line);
            }
        }

        private void GenerateText(LyricsJson line)
        {
            var lineWidth = 0f;
            var lineHeight = 0f;
            var emptyTexCount = 0;
            foreach (var letter in line.sentence)
            {
                var fontTex = font.GetTexture(letter.ToString().ToUpper());
                lineWidth += fontTex.BaseWidth * fontScale;
                lineHeight = Math.Max(lineHeight, fontTex.BaseHeight * fontScale);
                if (fontTex.IsEmpty) emptyTexCount++;
            }

            var letterX = line.position.X - lineWidth * 0.5f;
            var delay = 0d;
            var reverseDelay = 50d * (line.sentence.Count() - emptyTexCount);

            var distance = (float)Random(1f, 5f);
            var direction = Random(-1, 2);

            while (direction == 0)
                direction = Random(-1, 2);

            var firstLetter = true;
            foreach (var letter in line.sentence)
            {
                var fontTex = font.GetTexture(letter.ToString());
                if (!fontTex.IsEmpty)
                {
                    var position = new Vector2(letterX, line.position.Y) + fontTex.OffsetFor(OsbOrigin.Centre) * fontScale;
                    var boxPosition = new Vector2(letterX, line.position.Y) + fontTex.OffsetFor(OsbOrigin.CentreLeft) * fontScale;
                    var sprite = GetLayer("lyrics").CreateSprite(fontTex.Path, OsbOrigin.Centre, position);

                    if (firstLetter)
                    {
                        GenerateTextBox(line.startTime, line.endTime, boxPosition, distance, direction, lineWidth, lineHeight + 1.25f);
                        firstLetter = false;
                    }

                    sprite.Move(OsbEasing.OutSine, line.startTime + delay, line.startTime + beatDuration * 2 + delay, position.X + Random(10, 20), position.Y + Random(-10, 10), position.X - direction * distance, position.Y);
                    sprite.Move(line.startTime + beatDuration * 2 + delay, line.endTime - beatDuration * 2 - reverseDelay, position.X - direction * distance, position.Y, position.X + direction * distance, position.Y);
                    sprite.Move(OsbEasing.InSine, line.endTime - beatDuration * 2 - reverseDelay, line.endTime - reverseDelay, position.X + direction * distance, position.Y, position.X - Random(15, 25), position.Y - Random(-10, 10));

                    sprite.Fade(OsbEasing.InSine, line.startTime + delay, line.startTime + beatDuration * 2 + delay, 0, 1);
                    sprite.Fade(OsbEasing.Out, line.endTime - beatDuration * 2 - reverseDelay, line.endTime - reverseDelay, 1, 0);
                    sprite.Scale(line.startTime + delay, fontScale);
                }
                delay += 50;
                reverseDelay -= 50;
                letterX += fontTex.BaseWidth * fontScale;
            }
        }

        private void GenerateTextBox(double startTime, double endTime, Vector2 position, float distance, int direction, float lineWidth, float lineHeight)
        {
            var box = GetLayer("box").CreateSprite("sb/p.png", OsbOrigin.CentreLeft, new Vector2((position.X - 25) - direction * distance, position.Y));
            box.MoveX(startTime, endTime, box.PositionAt(startTime).X, (position.X - 25) + direction * distance);
            box.ScaleVec(OsbEasing.OutQuad, startTime, startTime + beatDuration * 4, 0, lineHeight, lineWidth + 50, lineHeight);
            box.Fade(startTime, 0.75);
            box.Fade(endTime - beatDuration * 4, 0);

            var boxPosition = box.PositionAt(endTime - beatDuration * 4);
            var reverseBox = GetLayer("box").CreateSprite("sb/p.png", OsbOrigin.CentreRight, new Vector2(boxPosition.X + lineWidth + 50, boxPosition.Y));
            reverseBox.MoveX(endTime - beatDuration * 4, endTime, reverseBox.PositionAt(endTime - beatDuration * 4).X, (position.X + lineWidth + 25) + direction * distance);
            reverseBox.ScaleVec(OsbEasing.InSine, endTime - beatDuration * 4, endTime, lineWidth + 50, lineHeight, 0, lineHeight);
            reverseBox.Fade(endTime - beatDuration * 4, 0.75);

            box.Color(startTime, Color4.Black);
            reverseBox.Color(endTime - beatDuration * 4, Color4.Black);
        }

        private FontGenerator SetupFont()
        {
            var _font = LoadFont("sb/f", new FontDescription()
            {
                FontPath = "SourceHanSerif-Regular.otf",
                FontSize = 60,
                Color = Color4.White
            });

            return _font;
        }
    }
}
