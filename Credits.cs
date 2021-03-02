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
    public class Part
    {
        public double startTime { get; set; }
        public double endTime { get; set; }
        public string name { get; set; }
    }

    public class Credits : StoryboardObjectGenerator
    {
        private double beatDuration;
        private FontGenerator Font;

        private float fontScale = 0.3f;
        private Vector2 textPos = new Vector2(-40, 400);

        private Vector2 boxPos;
        private float lastBoxWidth = 0f;
        private float boxHeight = 0f;

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            Font = SetupFont();

            var part = JsonConvert.DeserializeObject<List<Part>>(File.ReadAllText($"{ProjectPath}/parts.json"));
            foreach (var p in part)
            {
                GenerateText(p);
            }

            GenerateBox(193617, 80.7f);
        }

        private void GenerateText(Part part)
        {
            var texture = Font.GetTexture(part.name);
            if (!texture.IsEmpty)
            {
                Vector2 postion = textPos + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                boxPos = new Vector2(postion.X - texture.BaseWidth * 0.5f * fontScale - 15, postion.Y);
                var boxWidth = Math.Max(lastBoxWidth, texture.BaseWidth * fontScale + 30);
                lastBoxWidth = texture.BaseWidth * fontScale + 30;
                boxHeight = texture.BaseHeight * fontScale * 1.1f;

                var sprite = GetLayer("part").CreateSprite("sb/p.png", OsbOrigin.CentreLeft, boxPos);
                sprite.Fade(part.startTime + beatDuration, 0.65);
                sprite.Fade(part.endTime + beatDuration, 0);
                sprite.ScaleVec(part.startTime + beatDuration, texture.BaseWidth * fontScale + 30, boxHeight);
                sprite.Color(part.startTime + beatDuration, Color4.Black);

                sprite = GetLayer("part").CreateSprite(texture.Path, OsbOrigin.Centre, postion);

                sprite.Fade(part.startTime + beatDuration, 1);
                sprite.Fade(part.endTime + beatDuration, 0);
                sprite.Scale(part.startTime + beatDuration, fontScale);

                GenerateBox(part.startTime, boxWidth);
            }
        }

        private void GenerateBox(double startTime, float boxWidth)
        {
            var box = GetLayer("part").CreateSprite("sb/p.png", OsbOrigin.CentreLeft, boxPos);
            box.ScaleVec(OsbEasing.InOutQuad, startTime - beatDuration, startTime, 3, 0, 3, boxHeight);
            box.ScaleVec(OsbEasing.InExpo, startTime, startTime + beatDuration, 3, boxHeight, boxWidth, boxHeight);
            box.ScaleVec(OsbEasing.OutQuart, startTime + beatDuration, startTime + beatDuration * 2, boxWidth, boxHeight, 3, boxHeight);
            box.ScaleVec(OsbEasing.OutSine, startTime + beatDuration * 2, startTime + beatDuration * 2.5, 3, boxHeight, 3, 0);

            box.MoveX(startTime - beatDuration, boxPos.X);
            box.MoveX(startTime + beatDuration, boxPos.X + boxWidth);
            box.Rotate(startTime - beatDuration, 0);
            box.Rotate(startTime + beatDuration, Math.PI);
        }

        private FontGenerator SetupFont()
        {

            return LoadFont("sb/f", new FontDescription()
            {
                FontPath = "Raleway-Light.ttf",
                FontSize = 60,
                Color = Color4.White
            });
        }

    }
}
