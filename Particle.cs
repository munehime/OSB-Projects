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
    public class Particle : StoryboardObjectGenerator
    {
        private int particleDuration = 4000;
        private int particleAmout = 50;

        public override void Generate()
        {
            var timestep = particleDuration / particleAmout;
            using (var pool = new OsbSpritePool(GetLayer(""), "sb/d.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            {
                sprite.Scale(startTime, Random(0.02f, 0.03f));
            }))
            {
                for (double startTime = 0; startTime < AudioDuration; startTime += timestep)
                {
                    var moveSpeed = Random(40, 120);
                    var endTime = startTime + Math.Ceiling(480f / moveSpeed) * particleDuration;
                    var sprite = pool.Get(startTime, endTime);

                    var startX = Random(-109, 749);
                    sprite.MoveX(startTime, endTime, startX, startX + Random(-20, 20));
                    sprite.MoveY(startTime, endTime, 482, -moveSpeed);

                    sprite.Fade(OsbEasing.None, startTime, endTime, 1, 0);
                    sprite.Additive(startTime, endTime);
                }
            }

        }
    }
}
