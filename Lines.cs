using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    public class Lines : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            foreach (var hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime <= 1030443 - 5 || 1062443 - 5 < hitObject.StartTime) continue;

                var hsprite = GetLayer("Horinzital").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitObject.Position);

                hsprite.ScaleVec(hitObject.StartTime - 50, 5, 2);
                hsprite.Fade(hitObject.StartTime - 50, hitObject.StartTime, 0, 0.8);
                hsprite.Fade(hitObject.StartTime, hitObject.StartTime + 800, 0.8, 0);

                var startR = 0.0;
                var endR = 0.0;

                do
                {
                    startR = MathHelper.DegreesToRadians(Random(-3, 3));
                    endR = MathHelper.DegreesToRadians(Random(-3, 3));
                } while (Math.Abs(endR - startR) > 2);

                hsprite.Rotate(hitObject.StartTime - 50, hitObject.StartTime + 800, startR, endR);

                /*
                var vsprite = GetLayer("Vertical").CreateSprite("sb/hl.png", OsbOrigin.Centre, hitObject.Position);

                vsprite.ScaleVec(hitObject.StartTime - 50, 6, 2);
                vsprite.Fade(hitObject.StartTime - 50, hitObject.StartTime, 0, 1);
                vsprite.Fade(hitObject.StartTime, hitObject.StartTime + 800, 1, 0);

                do
                {
                    startR = MathHelper.DegreesToRadians(Random(-3, 3));
                    endR = MathHelper.DegreesToRadians(Random(-3, 3));
                } while (Math.Abs(endR - startR) > 2);

                vsprite.Rotate(hitObject.StartTime - 50, hitObject.StartTime + 800, MathHelper.DegreesToRadians(90) + startR, MathHelper.DegreesToRadians(90) + endR);
                */

            }
        }
    }
}
