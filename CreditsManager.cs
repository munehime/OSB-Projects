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
using System.Drawing;
using System.IO;
using System.Linq;

namespace StorybrewScripts
{
    public class CreditsManager : StoryboardObjectGenerator
    {
        private string tempPath;
        private FontGenerator Font;
        private string splitPath = "sb/f/sp";

        public override void Generate()
        {
            tempPath = $"{ProjectPath}/temp/c";
            Font = SetupFont();

            GenerateSplitCredits("osu! Community Choice 2019", 224285, 228571, true);
            GenerateSplitCredits("VARIOUS ARTISTS", 224785, 228571, false);
            GenerateSplitCredits("BEATMAP", 228571, 231999, true);
            GenerateSplitCredits("- REM -", 229071, 231999, false);
            GenerateSplitCredits("HITSOUND", 231999, 235428, true);
            GenerateSplitCredits("-AKIIRA-", 232499, 235428, false);
            GenerateSplitCredits("STORYBOARD", 235428, 237999, true);
            GenerateSplitCredits("MUNRIKA", 235928, 237999, false);
        }

        private void GenerateSplitCredits(string sentence, int startTime, int endTime, bool top)
        {
            var fontScale = top ? 0.35f : 0.3f;

            var texture = Font.GetTexture(sentence);
            var img = new Bitmap(Image.FromFile(texture.Path));
            var fileName = Path.GetFileNameWithoutExtension(texture.Path);
            var origin = OsbOrigin.TopCentre;
            for (int i = 0; i < 2; i++)
            {
                var rectangle = new Rectangle(0, img.Height / 2 * i, img.Width, img.Height / 2);
                var half = img.Clone(rectangle, img.PixelFormat);
                var path = $"{MapsetPath}/{splitPath}/{fileName}_{i}.png";
                half.Save(path);
                half.Dispose();

                var centerY = 0f;
                if (top) centerY = 240 - texture.BaseHeight * 0.175f;
                else centerY = 240 + texture.BaseHeight * 0.175f;

                var realPath = $"{splitPath}/{Path.GetFileName($"{MapsetPath}/{splitPath}/{fileName}_{i}.png")}";
                var position = new Vector2(320 - texture.BaseWidth * fontScale * 0.5f, centerY - texture.BaseHeight * fontScale * 0.5f) + texture.OffsetFor(origin) * fontScale;
                var sprite = GetLayer("").CreateSprite(realPath, origin, position);

                if (i != 1)
                {
                    sprite.MoveX(OsbEasing.Out, startTime, startTime + 300, position.X - 10, position.X);
                }
                else
                {
                    sprite.MoveX(OsbEasing.Out, startTime, startTime + 300, position.X + 10, position.X);
                }

                sprite.Scale(startTime, fontScale);
                sprite.Fade(OsbEasing.Out, startTime, startTime + 300, 0, 1);
                sprite.Fade(OsbEasing.Out, endTime - 300, endTime, 1, 0);

                origin = OsbOrigin.BottomCentre;
            }
        }

        private FontGenerator SetupFont()
        {
            var font = LoadFont(tempPath, new FontDescription()
            {
                FontPath = "SVN-Product Sans Bold.ttf",
                FontSize = 60,
                Color = Color4.White,
            });

            return font;
        }
    }
}
