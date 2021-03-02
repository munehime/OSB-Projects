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
        public string sentence;
        public double startTime;
        public double endTime;
        public Vector2 center;
    }

    public class LyricsManager : StoryboardObjectGenerator
    {
        private FontGenerator Font;
        private double BeatDuration;

        private float fontScale = 0.35f;

        public override void Generate()
        {
            Font = SetupFont();
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            using (var stream = OpenProjectFile("lyrics.json"))
            using (var reader = new StreamReader(stream, System.Text.Encoding.UTF8))
            {
                var data = reader.ReadToEnd();
                var lines = JsonConvert.DeserializeObject<List<LyricsData>>(data);

                foreach (var line in lines)
                    GenerateText(line);
            }
        }

        private void GenerateText(LyricsData line)
        {
            float lineWidth = 0;
            foreach (var letter in line.sentence)
                lineWidth += Font.GetTexture(letter.ToString().ToUpper()).BaseWidth * fontScale;

            if (line.center.Equals(Vector2.Zero)) line.center = new Vector2(320, 370);
            float letterX = line.center.X - lineWidth * 0.5f;

            foreach (var letter in line.sentence)
            {
                var texture = Font.GetTexture(letter.ToString().ToUpper());
                if (!texture.IsEmpty)
                {
                    Vector2 position = new Vector2(letterX, line.center.Y) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    var sprite = GetLayer("text").CreateSprite(texture.Path, OsbOrigin.BottomCentre, position);

                    sprite.ScaleVec(OsbEasing.OutExpo, line.startTime, line.startTime + BeatDuration * 0.5, fontScale, 0, fontScale, fontScale);
                    sprite.ScaleVec(OsbEasing.InExpo, line.endTime - BeatDuration * 0.5, line.endTime, fontScale, fontScale, fontScale, 0);

                    sprite.Fade(OsbEasing.Out, line.startTime, line.startTime + BeatDuration * 0.5, 0, 1);
                    sprite.Fade(OsbEasing.In, line.endTime - BeatDuration * 0.5, line.endTime, 1, 0);

                    sprite.MoveY(OsbEasing.OutExpo, line.startTime, line.startTime + BeatDuration * 0.5, position.Y - 50, position.Y);
                    sprite.MoveY(OsbEasing.InExpo, line.endTime - BeatDuration * 0.5, line.endTime, position.Y, position.Y + 50);
                }

                letterX += texture.BaseWidth * fontScale;
            }
        }

        private FontGenerator SetupFont()
        {
            return LoadFont("sb/f", new FontDescription()
            {
                FontPath = "CODE-Bold.otf",
                FontSize = 60,
                Color = Color4.White
            }, new FontGlow()
            {
                Radius = 2,
                Color = Color4.White
            }, new FontOutline()
            {
                Thickness = 1,
                Color = new Color4(0, 0, 0, 50)
            });
        }

    }
}
