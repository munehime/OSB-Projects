using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;

namespace StorybrewScripts
{
    public class Titles : StoryboardObjectGenerator
    {
        FontGenerator font;

        float fontScale = 0.35f;

        public override void Generate()
        {
            font = SetupFont("sb/f/t", "SVN-Product Sans Bold");

            GenerateTitle("Parousia", 11982, 46649);
            GenerateTitle("ANiMa", 46627, 75579);
            GenerateTitle("Akasha", 75579, 119687);
            GenerateTitle("Happy End of the World", 127470, 178256);
            GenerateTitle("Halcyon", 178256, 217654);
            GenerateTitle("Ascension to Heaven", 217654, 260854);
            GenerateTitle("Wish upon Twin Stars", 260854, 284854);
            GenerateTitle("Blue Zenith", 284854, 315753);
            GenerateTitle("over the top", 319653, 351653);
            GenerateTitle("FREEDOM DiVE↓", 351653, 388743);
            GenerateTitle("Glorious Crown", 388743, 415613);
        }

        private void GenerateTitle(string title, double startTime, double endTime)
        {
            double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            float lineWidth = 0;
            float lineHeight = 0;
            Vector2 padding = new Vector2(10, 1);

            foreach (var letter in title)
            {
                var texture = font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * fontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontScale);
            }

            double delay = 0;
            float letterX = 0;
            bool first = true;
            foreach (var letter in title)
            {
                var texture = font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    var position = new Vector2(letterX, 390) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    var sprite = GetLayer("text").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprite.Scale(startTime + delay, fontScale);
                    sprite.Move(OsbEasing.OutSine, startTime + delay, startTime + delay + beatDuration * 4, new Vector2(position.X + Random(20, 60f), position.Y + Random(-20, 20f)), position);
                    sprite.Fade(startTime + delay, startTime + delay + beatDuration * 4, 0, 1);

                    if (first)
                    {
                        var box = GetLayer("box").CreateSprite("sb/p.png", OsbOrigin.CentreLeft, new Vector2(position.X - texture.BaseWidth * fontScale * 0.5f - padding.X, position.Y));
                        box.ScaleVec(OsbEasing.OutQuart, startTime, startTime + beatDuration * 8, 0, lineHeight + padding.Y * 2, lineWidth + padding.X * 2, lineHeight + padding.Y * 2);
                        box.Fade(startTime, 0.5);
                        box.Fade(endTime, 0);
                        box.Color(startTime, Color4.Black);
                        first = false;

                        switch ((int)endTime)
                        {
                            case 119687:
                            case 260854:
                            case 315753:
                            case 415613:
                                box.ScaleVec(OsbEasing.InQuart, endTime - beatDuration * 4, endTime, lineWidth + padding.X * 2, lineHeight + padding.Y, 0, lineHeight + padding.Y * 2);
                                break;
                        }
                    }

                    switch ((int)endTime)
                    {
                        case 119687:
                        case 260854:
                        case 315753:
                        case 415613:
                            sprite.Move(OsbEasing.InSine, endTime - delay - beatDuration * 4, endTime - delay, position, new Vector2(position.X + Random(20, 60f), position.Y + Random(-20, 20f)));
                            sprite.Fade(endTime - delay - beatDuration * 4, endTime - delay, 1, 0);
                            break;
                        default:
                            sprite.Fade(endTime, 0);
                            break;
                    }

                    delay += beatDuration / 2;
                }

                letterX += texture.BaseWidth * fontScale;
            }
        }


        private FontGenerator SetupFont(string spritePath, string fontName)
        {
            return LoadFont("sb/f/t", new FontDescription()
            {
                FontPath = "SVN-Product Sans Bold.ttf",
                FontSize = 60,
                Color = Color4.White
            });
        }
    }
}
