using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
using System.IO;

namespace StorybrewScripts
{
    public class ModIcons : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            List<string> spritePaths = new List<string>(Directory.GetFiles(@"E:/osu!/Songs/973362 Various Artists - Patt!Memories/sb/mod icons/"));
            List<OsbSprite> sprites = new List<OsbSprite>();

            foreach (var spritepath in spritePaths)
            {
                var spriteCreate = GetLayer("").CreateSprite(spritepath.Remove(0, 53), OsbOrigin.Centre);
                sprites.Add(spriteCreate);
            }

            #region DT

            sprites[0].Move(45462, 666, 94);
            sprites[0].Scale(45462, 45862, 1, 0.36);
            sprites[0].Fade(45462, 45862, 0, 1);
            sprites[0].Scale(73905, 74305, 0.36, 1);
            sprites[0].Fade(73905, 74305, 1, 0);

            sprites[0].Scale(204584, 204984, 1, 0.36);
            sprites[0].Fade(204584, 204984, 0, 1);
            sprites[0].Scale(232989, 233089, 0.36, 1);
            sprites[0].Fade(232989, 233089, 1, 0);

            sprites[0].Scale(442241, 442641, 1, 0.36);
            sprites[0].Fade(442241, 442641, 0, 1);
            sprites[0].Scale(475076, 475476, 0.36, 1);
            sprites[0].Fade(475076, 475476, 1, 0);

            sprites[0].Move(930470, 716, 94);
            sprites[0].Scale(930470, 930870, 1, 0.36);
            sprites[0].Fade(930470, 930870, 0, 1);
            sprites[0].Move(950697, 666, 94);
            sprites[0].Scale(965106, 965506, 0.36, 1);
            sprites[0].Fade(965106, 965506, 1, 0);

            #endregion

            #region FL

            sprites[1].Move(1434, 716, 94);
            sprites[1].Scale(1434, 1834, 1, 0.36);
            sprites[1].Fade(1434, 1834, 0, 1);
            sprites[1].Scale(45301, 45401, 0.36, 1);
            sprites[1].Fade(45301, 45401, 1, 0);

            #endregion

            #region HR

            sprites[2].Move(233089, 666, 94);
            sprites[2].Scale(233089, 233189, 1, 0.36);
            sprites[2].Fade(233089, 233189, 0, 1);
            sprites[2].Scale(275280, 275680, 0.36, 1);
            sprites[2].Fade(275280, 275680, 1, 0);

            sprites[2].Scale(644197, 644597, 1, 0.36);
            sprites[2].Fade(644197, 644597, 0, 1);
            sprites[2].Fade(697049, 0);

            sprites[2].Move(905297, 716, 94);
            sprites[2].Scale(905297, 905697, 1, 0.36);
            sprites[2].Fade(905297, 905697, 0, 1);
            sprites[2].Scale(929797, 930197, 0.36, 1);
            sprites[2].Fade(929797, 930197, 1, 0);

            #endregion

            #region HD

            sprites[3].Move(45262, 716, 94);
            sprites[3].Scale(45262, 45662, 1, 0.36);
            sprites[3].Fade(45262, 45662, 0, 1);
            sprites[3].Scale(74105, 74505, 0.36, 1);
            sprites[3].Fade(74105, 74505, 1, 0);

            sprites[3].Scale(204384, 204784, 1, 0.36);
            sprites[3].Fade(204384, 204784, 0, 1);
            sprites[3].Scale(275480, 275880, 0.36, 1);
            sprites[3].Fade(275480, 275880, 1, 0);

            sprites[3].Scale(442041, 442441, 1, 0.36);
            sprites[3].Fade(442041, 442441, 0, 1);
            sprites[3].Scale(535047, 535447, 0.36, 1);
            sprites[3].Fade(535047, 535447, 1, 0);

            sprites[3].Scale(643997, 644397, 1, 0.36);
            sprites[3].Fade(643997, 644397, 0, 1);
            sprites[3].Fade(697049, 0);
            
            sprites[3].Fade(950697, 1);
            sprites[3].Scale(965306, 965706, 0.36, 1);
            sprites[3].Fade(965306, 965706, 1, 0);

            #endregion
        }
    }
}
