using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;

namespace StorybrewScripts
{
    public class TransitionsManager : StoryboardObjectGenerator
    {
        double beatDuration;
        int spritesPerRow = 5;
        float spriteWidth;
        List<OsbSprite> sprites = new List<OsbSprite>();

        public override void Generate()
        {
            spriteWidth = 854f / spritesPerRow;
            var position = new Vector2(-107f + spriteWidth * 0.5f, 0);
            for (int i = 0; i < spritesPerRow * 20; i++)
            {
                if (i % 2 == 0)
                {
                    position.Y = 0;
                    sprites.Add(GetLayer("slide").CreateSprite("sb/p.png", OsbOrigin.TopCentre, position));
                }
                else
                {
                    position.Y = 480;
                    sprites.Add(GetLayer("slide").CreateSprite("sb/p.png", OsbOrigin.BottomCentre, position));
                    position.X += spriteWidth;
                }
            }

            GenerateSideTransition(45983, 46649);
            GenerateSideTransition(74901, 75579);
            GenerateSideTransition(177641, 178259);
            GenerateSideTransition(217028, 217654);
            GenerateSideTransition(284253, 284854);
            GenerateSideTransition(351082, 351653);
            GenerateSideTransition(388199, 388743);
        }

        private void GenerateSideTransition(double startTime, double endTime)
        {
            beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
            double delay = 0;

            for (int i = 0; i < spritesPerRow * 2; i++)
            {
                sprites[i].ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, spriteWidth, 0, spriteWidth, 240);

                sprites[i].Fade(startTime + delay, 1);
                sprites[i].Fade(endTime, 0);

                if (i % 2 != 0)
                    delay += beatDuration / 4;
            }
        }
    }
}
