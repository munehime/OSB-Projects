using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;

namespace StorybrewScripts
{
    public class MovingUpParticles : StoryboardObjectGenerator
    {
        double particleDuration = 2000;
        int particleAmout = 10;

        public override void Generate()
        {
            var timestep = particleDuration / particleAmout;
            using (var pool = new OsbSpritePool(GetLayer(""), "sb/d.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            {
                sprite.Scale(startTime, Random(0.02f, 0.05f));
            }))
            {
                for (double startTime = 0; startTime < AudioDuration; startTime += timestep)
                {
                    var moveSpeed = Random(40, 120);
                    var endTime = startTime + Math.Ceiling(480f / moveSpeed) * particleDuration;
                    var sprite = pool.Get(startTime, endTime);

                    var startX = Random(-107, 747f);
                    sprite.MoveX(startTime, endTime, startX, startX + Random(-50, 50f));
                    sprite.MoveY(startTime, endTime, 482, 240 - moveSpeed);

                    sprite.Fade(startTime, startTime + 100, 0, 1);
                    sprite.Fade(OsbEasing.InSine, startTime + 100, endTime, 1, 0);
                    sprite.Additive(startTime, endTime);
                }
            }
        }
    }
}
