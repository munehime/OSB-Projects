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
    public class CreditsManager : StoryboardObjectGenerator
    {
        FontGenerator font;

        float fontScale = 0.3f;

        public override void Generate()
        {
            font = SetupFont();

            GenerateIntroTitle("Doja Cat - Like That (feat. Gucci Mane)", 145364, 147628);
            GenerateIntroTitle("Mapset: Ducky-", 147628, 149892);

            switch (Beatmap.Name)
            {
                case "Aqua_'s Expert":
                case "Nanachi's Extra":
                    var beatmapInfo = Beatmap.ToString().Split('\'');
                    GenerateIntroTitle($"Beatmap: {beatmapInfo[0].Replace("s ", "")}", 149892, 152156);
                    break;

                case "Stormi's Hard":
                    GenerateIntroTitle("Beatmap: Stormiverse", 149892, 152156);
                    break;

                case "Ducky=Eresh's Insane":
                    GenerateIntroTitle("Beatmap: Ducky- x -Eresh", 149892, 152156);
                    break;

                default:
                    GenerateIntroTitle("Beatmap: Ducky-", 149892, 152156);
                    break;
            }

            GenerateIntroTitle("Hitsound: Hokichi", 152156, 154420);
            GenerateIntroTitle("Storyboard: Mune", 154420, 156684);

        }

        private void GenerateIntroTitle(string title, double startTime, double endTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            float lineWidth = 0;
            foreach (var letter in title)
            {
                var texture = font.GetTexture(letter.ToString());
                lineWidth += texture.Width * fontScale;
            }
            float letterX = 320 - lineWidth * 0.5f;
            foreach (var letter in title)
            {
                var texture = font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, 240) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprite.ScaleVec(OsbEasing.InExpo, endTime - beatDuration, endTime, fontScale, fontScale, fontScale, 0);
                    sprite.Move(OsbEasing.OutExpo, startTime, endTime, new Vector2(320, 240), position.X, 240);
                    sprite.Fade(startTime, startTime + beatDuration, 0, 1);
                }

                letterX += texture.Width * fontScale;
            }
        }

        private FontGenerator SetupFont()
        {
            return LoadFont("sb/f/c", new FontDescription()
            {
                FontPath = "CODE-Bold.otf",
                FontSize = 60,
                Color = Color4.White,
                Padding = new Vector2(10, 0),
            });
        }
    }
}
