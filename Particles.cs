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
using System.Linq;
using System.Globalization;

namespace StorybrewScripts
{
    public class Particles : StoryboardObjectGenerator
    {
        private int particleDuration = 2000;
        private int particleAmount = 40;
        private float size = 0.4f;
        private string[] color = new string[] { "#7CFC00", "#7FFF00", "#32CD32", "#00FF00", "#228B22", "#ADFF2F", "#9ACD32", "#00FF7F" };

        public override void Generate()
        {
            GenerateParticles(69259, 91202);
            GenerateParticles(178974, 225602);
        }

        private void GenerateParticles(int startTime, int endTime)
        {
            using (var pool = new OsbSpritePool(GetLayer("particles"), "sb/Petal.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            { }))
            {
                var timeStep = particleDuration / particleAmount;
                for (var starttime = (double)startTime; starttime <= endTime - particleDuration; starttime += timeStep)
                {
                    var endtime = starttime + particleDuration;
                    var sprite = pool.Get(starttime, endtime);
                    int endX, endY = 0;
                    do
                    {
                        endX = Random(200, 777);
                        endY = Random(-10, 470);
                    } while (endX < 747 && 0 < endY);

                    sprite.Move(OsbEasing.InSine, starttime, endtime, Random(-127, 40), Random(480, 490), endX, endY);
                    sprite.ScaleVec(starttime, endtime, Random(-size, size), Random(-size, size), Random(-size, size), Random(-size, size));
                    sprite.Rotate(starttime, endtime, MathHelper.DegreesToRadians(-360), MathHelper.DegreesToRadians(360));

                    sprite.Color(starttime, color[Random(0, 8)]);

                    sprite.Fade(starttime, starttime + 200, 0, 1);
                    sprite.Fade(endtime - 200, endtime, 1, 0);
                }
            }
        }
    }
}
