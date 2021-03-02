using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
using System.IO;

namespace StorybrewScripts
{
    public class Screenshots : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            List<string> spritePaths = new List<string>(Directory.GetFiles(@"E:/osu!/Songs/973362 Various Artists - Patt!Memories/sb/screenshots/"));
            List<int> height = new List<int>();
            List<OsbSprite> screenshots = new List<OsbSprite>();

            foreach (var spritepath in spritePaths)
            {
                var spriteCreate = GetLayer("").CreateSprite(spritepath.Remove(0, 53), OsbOrigin.Centre);
                var bitmap = GetMapsetBitmap(spritepath.Remove(0, 53));
                screenshots.Add(spriteCreate);
                height.Add(bitmap.Height);
            }

            screenshots[0].Move(1054443, 300, 220);
            screenshots[0].Scale(1054443, 480.0 / (height[0] + 150));
            screenshots[0].Rotate(1054443, MathHelper.DegreesToRadians(-12));
            screenshots[0].Fade(1054443, 1054643, 0, 1);
            screenshots[0].Fade(1055243, 1055443, 1, 0);

            screenshots[1].Move(1055443, 350, 220);
            screenshots[1].Scale(1055443, 480.0 / (height[1] + 200));
            screenshots[1].Rotate(1055443, MathHelper.DegreesToRadians(3));
            screenshots[1].Fade(1055443, 1055643, 0, 1);
            screenshots[1].Fade(1056443, 1056643, 1, 0);

            screenshots[2].Move(1056443, 350, 220);
            screenshots[2].Scale(1056443, 480.0 / (height[2] + 250));
            screenshots[2].Rotate(1056443, MathHelper.DegreesToRadians(-7));
            screenshots[2].Fade(1056443, 1056643, 0, 1);
            screenshots[2].Fade(1057243, 1057443, 1, 0);

            screenshots[3].Move(1057443, 370, 222);
            screenshots[3].Scale(1057443, 480.0 / (height[3] + 180));
            screenshots[3].Rotate(1057443, MathHelper.DegreesToRadians(5));
            screenshots[3].Fade(1057443, 1057643, 0, 1);
            screenshots[3].Fade(1058243, 1058443, 1, 0);

            screenshots[4].Move(1058443, 340, 210);
            screenshots[4].Scale(1058443, 480.0 / (height[4] + 210));
            screenshots[4].Rotate(1058443, MathHelper.DegreesToRadians(-9));
            screenshots[4].Fade(1058443, 1058643, 0, 1);
            screenshots[4].Fade(1059243, 1059443, 1, 0);

            screenshots[5].Move(1059443, 340, 210);
            screenshots[5].Scale(1059443, 480.0 / (height[5] + 230));
            screenshots[5].Rotate(1059443, MathHelper.DegreesToRadians(2));
            screenshots[5].Fade(1059443, 1059643, 0, 1);
            screenshots[5].Fade(1060243, 1060443, 1, 0);

            screenshots[6].Move(1060443, 340, 210);
            screenshots[6].Scale(1060443, 480.0 / (height[6] + 230));
            screenshots[6].Rotate(1060443, MathHelper.DegreesToRadians(-4.5));
            screenshots[6].Fade(1060443, 1060643, 0, 1);
            screenshots[6].Fade(1061243, 1061443, 1, 0);

            screenshots[7].Move(1061443, 340, 220);
            screenshots[7].Scale(1061443, 480.0 / (height[7] + 100));
            screenshots[7].Rotate(1061443, MathHelper.DegreesToRadians(1.5));
            screenshots[7].Fade(1061443, 1061643, 0, 1);
            screenshots[7].Fade(1062243, 1062443, 1, 0);
        }
    }
}
