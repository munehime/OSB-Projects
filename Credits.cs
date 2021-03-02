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
using Newtonsoft.Json;

namespace StorybrewScripts
{
    public class Text
    {
        [JsonProperty()]
        public string sentence { get; set; }

        [JsonProperty()]
        public int startTime { get; set; }

        [JsonProperty()]
        public int endTime { get; set; }

        [JsonProperty()]
        public bool top { get; set; }
    }

    public class Credits : StoryboardObjectGenerator
    {
        private FontGenerator Font;
        private float fontScale = 0.35f;
        private int moveTime = 342;

        public override void Generate()
        {
            Font = SetupFont();
            var data = JsonConvert.DeserializeObject<List<Text>>(File.ReadAllText($"{ProjectPath}/credits.json"));
            foreach (var info in data)
            {
                GenerateText(info);
            }
            GenerateShape();
        }

        private void GenerateText(Text info)
        {
            var lineWidth = 0f;
            foreach (var letter in info.sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * fontScale;
            }

            var letterX = 320 - lineWidth * 0.5f;
            var letterY = 0f;
            if (info.top) letterY = 220; else letterY = 260;
            foreach (var letter in info.sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, letterY)
                        + texture.OffsetFor(OsbOrigin.Centre) * fontScale;

                    var sprite = GetLayer("text").CreateSprite(texture.Path, OsbOrigin.Centre, position);
                    sprite.Scale(info.startTime, fontScale);
                    sprite.Fade(OsbEasing.Out, info.startTime, info.startTime + moveTime, 0, 1);
                    sprite.Fade(OsbEasing.Out, info.endTime - moveTime * 0.5, info.endTime, 1, 0);

                    if (info.top)
                    {
                        sprite.MoveY(OsbEasing.OutExpo, info.startTime, info.startTime + moveTime, 240, 240 - (texture.BaseHeight * 0.25));
                        sprite.MoveY(info.startTime, info.endTime - moveTime, 240 - (texture.BaseHeight * 0.25), 240 - (texture.BaseHeight * 0.2));
                        sprite.MoveY(info.endTime - moveTime * 0.5, info.endTime, 240 - (texture.BaseHeight * 0.2), 240);
                    }
                    else
                    {
                        sprite.MoveY(OsbEasing.OutExpo, info.startTime, info.startTime + moveTime, 240, 240 + (texture.BaseHeight * 0.25));
                        sprite.MoveY(info.startTime, info.endTime - moveTime, 240 + (texture.BaseHeight * 0.25), 240 + (texture.BaseHeight * 0.2));
                        sprite.MoveY(info.endTime - moveTime * 0.5, info.endTime, 240 + (texture.BaseHeight * 0.2), 240);
                    }
                }
                letterX += texture.BaseWidth * fontScale;
            }
        }

        private void GenerateShape()
        {
            var sprite = GetLayer("shape").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.ScaleVec(OsbEasing.OutExpo, 102174, 102516, 0, 3, 300, 3);
            sprite.ScaleVec(112288, 112459, 300, 3, 0, 3);
            sprite.Fade(102174, 1);
            sprite.Fade(112459, 1);

            sprite = GetLayer("shape").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            sprite.Scale(OsbEasing.OutExpo, 112459, 112631, 0, 80);
            sprite.Scale(OsbEasing.OutExpo, 112974, 113145, 80, 854);
            sprite.Rotate(OsbEasing.InExpo, 112459, 112974, MathHelper.DegreesToRadians(-40), MathHelper.DegreesToRadians(30));
            sprite.Rotate(OsbEasing.OutExpo, 112974, 113145, MathHelper.DegreesToRadians(30), MathHelper.DegreesToRadians(90));

            var origin = OsbOrigin.CentreLeft;
            var pos = new Vector2(-107, 240);

            for (var i = 0; i < 2; i++)
            {
                sprite = GetLayer("shape").CreateSprite("sb/pixel.png", origin, pos);
                sprite.ScaleVec(OsbEasing.OutExpo, 113145, 114145, 427, 480, 0, 480);
                sprite.Fade(113145, 1);
                sprite.Fade(114145, 1);

                origin = OsbOrigin.CentreRight;
                pos = new Vector2(747, 240);
            }

        }

        private FontGenerator SetupFont()
        {
            var font = LoadFont("sb/f", new FontDescription()
            {
                FontPath = "fonts/SVN-Product Sans Bold.ttf",
                FontSize = 60,
                Color = Color4.White

            });

            return font;
        }
    }
}
