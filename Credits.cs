using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;

namespace StorybrewScripts
{
    public class Credits : StoryboardObjectGenerator
    {
        FontGenerator font;

        float fontScale = 0.35f;

        public override void Generate()
        {
            font = SetupFont();

            GenerateIntroTitle("xi - Songs Compilation", 1316, 11982);
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

                    sprite.Scale(startTime, fontScale);
                    sprite.Move(OsbEasing.OutExpo, startTime, endTime - beatDuration * 4, new Vector2(320, 240), position.X, 240);
                    sprite.Move(OsbEasing.InExpo, endTime - beatDuration * 4, endTime, new Vector2(position.X, 240), new Vector2(320, 240));
                    sprite.Fade(startTime, startTime + beatDuration * 4, 0, 1);
                    sprite.Fade(OsbEasing.InExpo, endTime - beatDuration * 4, endTime, 1, 0);
                }

                letterX += texture.Width * fontScale;
            }
        }

        private FontGenerator SetupFont()
        {
            return LoadFont("sb/f/c", new FontDescription()
            {
                FontPath = "Raleway-Light.ttf",
                FontSize = 60,
                Color = Color4.White,
                Padding = new Vector2(20, 0),
            });
        }
    }
}
