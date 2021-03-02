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
    public class Soild : StoryboardObjectGenerator
    {
        private FontGenerator Font;
        private float fontScale = 0.35f;
        private double beatDuration;

        public override void Generate()
        {
            Font = setupFont();
            beatDuration = Beatmap.GetTimingPointAt((int)Beatmap.HitObjects.First().StartTime).BeatDuration;
            GenerateText("osu! Community Choice 2019", 1985, 10768);
        }

        private void GenerateText(string sentence, int startTime, int endTime)
        {
            var lineWidth = 0f;
            var isEmpty = 0;
            foreach (var letter in sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * fontScale;
                if (texture.IsEmpty) isEmpty++;
            }

            var letterX = 320f - lineWidth * 0.5f;
            var delay = 0d;
            var _delay = 75 * (sentence.Count() - isEmpty);

            var 
            foreach (var letter in sentence)
            {
                var texture = Font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, 240) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    var sprite = GetLayer("character").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprite.Scale(startTime, fontScale);
                    sprite.Fade(startTime + delay, startTime + delay + beatDuration * 2, 0, 1);
                    sprite.Fade(endTime - _delay - beatDuration * 2, endTime - _delay, 1, 0);

                    delay += 75;
                    _delay += -75;
                }

                letterX += texture.BaseWidth * fontScale;
            }
        }

        private FontGenerator setupFont()
        {
            var font = LoadFont("sb/f/b", new FontDescription()
            {
                FontPath = "SVN-Product Sans Bold.ttf",
                FontSize = 60,
                Color = Color4.White
            });

            return font;
        }
    }
}
