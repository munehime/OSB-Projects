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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace StorybrewScripts
{
    public class Beatmaps
    {
        [JsonProperty]
        public string artist { get; set; }
        [JsonProperty]
        public string title { get; set; }
        [JsonProperty]
        public int startTime { get; set; }
        [JsonProperty]
        public int endTime { get; set; }
        [JsonProperty]
        public string color { get; set; }
    }

    public class BeatmapsManager : StoryboardObjectGenerator
    {
        private Regex pattern = new Regex("[,|♪]");

        private double beatDuration;
        private string lastColor = "#000000";
        private FontGenerator Font;
        private float letterX;
        private float bgScale = 0.65f;
        private int gap = 1600;

        private string[] filePath;

        public override void Generate()
        {
            filePath = Directory.GetFiles($"{MapsetPath}/sb/bg");
            beatDuration = Beatmap.GetTimingPointAt((int)Beatmap.HitObjects.First().StartTime).BeatDuration;
            Font = SetupFont();
            var beatmaps = JsonConvert.DeserializeObject<List<Beatmaps>>(File.ReadAllText($"{ProjectPath}/beatmaps.json"));
            for (int i = 0; i < beatmaps.Count; i++)
            {
                GenerateSoildBackground(beatmaps[i], i);
                GenerateBackground(beatmaps[i]);
                GenerateBeatmapInfo(beatmaps[i]);
            }

            GenerateVig(Beatmap.HitObjects.First().StartTime, Beatmap.HitObjects.Last().EndTime);
        }

        private void GenerateBackground(Beatmaps beatmap)
        {
            string path = $"sb/bg/{GetBackgroundPath(pattern.Replace(beatmap.title, ""))}";
            var bitmap = GetMapsetBitmap(path);
            var sprite = GetLayer("bg").CreateSprite(path, OsbOrigin.Centre);

            sprite.MoveX(OsbEasing.OutExpo, beatmap.startTime - beatDuration * 2.5, beatmap.startTime, 747 + gap * (480f / 1080) * 0.5, 320);
            sprite.MoveX(OsbEasing.OutExpo, beatmap.endTime - beatDuration * 2.5, beatmap.endTime, 320, -107 - gap * (480f / 1080) * 0.5);
            sprite.Scale(OsbEasing.OutExpo, beatmap.startTime, beatmap.startTime + beatDuration * 2.5, 480f / bitmap.Height * bgScale, 480f / bitmap.Height);
            sprite.Scale(OsbEasing.OutSine, beatmap.endTime - beatDuration * 4, beatmap.endTime - beatDuration * 2.5, 480f / bitmap.Height, 480f / bitmap.Height * bgScale);
            sprite.Fade(beatmap.startTime, 1);
            sprite.Fade(OsbEasing.Out, beatmap.endTime - beatDuration * 2, beatmap.endTime - beatDuration, 1, 0);
        }

        private void GenerateSoildBackground(Beatmaps beatmap, int i)
        {
            var sprite = GetLayer("soild").CreateSprite("sb/p.png", OsbOrigin.Centre);
            sprite.ScaleVec(beatmap.startTime - beatDuration * 4, 854, 480);
            sprite.Color(beatmap.startTime - beatDuration * 2.5, beatmap.startTime, lastColor, beatmap.color);
            sprite.Fade(beatmap.startTime - beatDuration * 4, 1);
            sprite.Fade(beatmap.startTime + beatDuration * 2.5, 1);
            if (i == 3 || i == 9)
            {
                sprite.Color(beatmap.endTime - beatDuration * 2.5, beatmap.endTime, beatmap.color, Color4.Black);
                lastColor = "#000000";
            }
            else
            {
                lastColor = beatmap.color;
            }

        }

        private void GenerateBeatmapInfo(Beatmaps beatmap)
        {
            letterX = -60f;
            double delay = 0;
            foreach (var letter in beatmap.artist)
            {
                SpriteCommands(beatmap, letter, delay);
                delay += 50;
            }

            letterX = -60f;
            delay = 0;
            foreach (var letter in beatmap.title)
            {
                SpriteCommands(beatmap, letter, delay, false);
                delay += 50;
            }

            GenerateLeftSide(beatmap.startTime, beatmap.endTime - beatDuration * 4);
        }

        private void SpriteCommands(Beatmaps beatmap, char letter, double delay, bool artist = true)
        {
            var letterY = 0f;
            if (artist) letterY = 360;
            else letterY = 380;

            var fontScale = 0.35f;
            if (artist) fontScale = 0.2f;

            var fontTex = Font.GetTexture(letter.ToString());
            if (!fontTex.IsEmpty)
            {
                var position = new Vector2(letterX, letterY) + fontTex.OffsetFor(OsbOrigin.Centre) * fontScale;
                var sprite = GetLayer("info").CreateSprite(fontTex.Path, OsbOrigin.Centre, position);

                if (artist)
                    sprite.Move(OsbEasing.OutSine, beatmap.startTime + delay, beatmap.startTime + delay + beatDuration * 4, position.X + 100, position.Y - 50, position.X, position.Y);
                else
                    sprite.Move(OsbEasing.OutSine, beatmap.startTime + delay, beatmap.startTime + delay + beatDuration * 4, position.X + 100, position.Y + 50, position.X, position.Y);

                sprite.ScaleVec(OsbEasing.InOutSine, beatmap.startTime + delay, beatmap.startTime + delay + beatDuration * 2, fontScale, -fontScale, fontScale, 0);
                sprite.ScaleVec(OsbEasing.InOutSine, beatmap.startTime + delay + beatDuration * 2, beatmap.startTime + delay + beatDuration * 4, fontScale, 0, fontScale, fontScale);
                sprite.ScaleVec(OsbEasing.InQuart, beatmap.endTime - delay - beatDuration * 7, beatmap.endTime - delay - beatDuration * 3, fontScale, fontScale, fontScale, 0);
                sprite.Move(OsbEasing.InSine, beatmap.endTime - delay - beatDuration * 7, beatmap.endTime - delay - beatDuration * 3, position.X, position.Y, position.X + 250, position.Y);
                sprite.Fade(beatmap.startTime + delay, beatmap.startTime + delay + beatDuration * 4, 0, 1);
                sprite.Fade(OsbEasing.Out, beatmap.endTime - delay - beatDuration * 7, beatmap.endTime - delay - beatDuration * 3, 1, 0);
            }

            letterX += fontTex.BaseWidth * fontScale;
        }

        private void GenerateVig(double startTime, double endTime)
        {
            var sprite = GetLayer("vig").CreateSprite("sb/vig.png", OsbOrigin.Centre);
            sprite.Scale(startTime, 480f / GetMapsetBitmap("sb/vig.png").Height);
            sprite.Fade(startTime - beatDuration * 2.5, startTime, 0, 1);
            sprite.Fade(endTime, endTime + beatDuration * 2.5, 1, 0);

        }

        private void GenerateLeftSide(double startTime, double endTime)
        {
            var sprite = GetLayer("line").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(-68, 385));
            sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, 3, 0, 3, 50);
            sprite.ScaleVec(OsbEasing.InExpo, endTime - beatDuration * 4, endTime, 3, 50, 3, 0);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 1);

            sprite = GetLayer("shadow").CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(-67.1f, 386.1f));
            sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, 3, 0, 3, 50);
            sprite.ScaleVec(OsbEasing.InExpo, endTime - beatDuration * 4, endTime, 3, 50, 3, 0);
            sprite.Fade(startTime, 0.5);
            sprite.Fade(endTime, 0.5);
            sprite.Color(startTime, Color4.Black);
        }

        private FontGenerator SetupFont()
        {
            var font = LoadFont("sb/f/s", new FontDescription()
            {
                FontPath = "SVN-Product Sans Bold.ttf",
                FontSize = 60,
                Color = Color4.White,
            },
            new FontShadow()
            {
                Thickness = 5,
                Color = new Color4(0, 0, 0, 100),
            });

            return font;
        }

        private string GetBackgroundPath(string name)
        {
            string path = "";
            foreach (var file in filePath)
            {
                if (name == Path.GetFileNameWithoutExtension(file))
                {
                    path = Path.GetFileName(file);
                };
            }

            return path;
        }
    }
}
