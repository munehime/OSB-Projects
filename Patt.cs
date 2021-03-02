using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
using System.IO;

namespace StorybrewScripts
{
    public class Patt : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            List<string> spritePaths = new List<string>(Directory.GetFiles(@"E:/osu!/Songs/973362 Various Artists - Patt!Memories/sb/Patt/"));
            List<int> height = new List<int>();
            List<OsbSprite> patt = new List<OsbSprite>();

            foreach (var spritepath in spritePaths)
            {
                var spriteCreate = GetLayer("").CreateSprite(spritepath.Remove(0, 53), OsbOrigin.Centre);
                var bitmap = GetMapsetBitmap(spritepath.Remove(0, 53));
                patt.Add(spriteCreate);
                height.Add(bitmap.Height);
            }
            
            patt[0].Move(1062443, 270, 160);
            patt[0].Scale(1062443, 480.0 / (height[0] + 800));
            patt[0].Rotate(1062443, MathHelper.DegreesToRadians(5));
            patt[0].Fade(1062443, 1062643, 0, 1);
            patt[0].Fade(1066443, 1);

            patt[1].Move(1063443, 424, 220);
            patt[1].Scale(1063443, 480.0 / (height[0] + 620));
            patt[1].Rotate(1063443, MathHelper.DegreesToRadians(-10.5));
            patt[1].Fade(1063443, 1063643, 0, 1);
            patt[1].Fade(1066443, 1);

            patt[2].Move(1064443, 300, 220);
            patt[2].Scale(1064443, 480.0 / (height[0] + 620));
            patt[2].Rotate(1064443, MathHelper.DegreesToRadians(9));
            patt[2].Fade(1064443, 1064643, 0, 1);
            patt[2].Fade(1066443, 1);

            patt[3].Move(1065443, 370, 278);
            patt[3].Scale(1065443, 480.0 / (height[0] + 320));
            patt[3].Rotate(1065443, MathHelper.DegreesToRadians(-3));
            patt[3].Fade(1065443, 1065643, 0, 1);
            patt[3].Fade(1066443, 1);

            patt[4].Move(1065943, 320, 235);
            patt[4].Scale(1065943, 480.0 / (height[0] + 300));
            patt[4].Rotate(1065943, MathHelper.DegreesToRadians(2));
            patt[4].Fade(1065943, 1066143, 0, 1);
            patt[4].Fade(1066443, 1);

            patt[5].Scale(1066443, 1072943, 480.0 / (height[5] - 25), 480.0 / (height[5] - 0));
            patt[5].Fade(1066443, 1);
            patt[5].Fade(1071381, 1072943, 1, 0);
        }
    }
}
