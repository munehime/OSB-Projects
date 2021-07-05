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
    public class Particles : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            GenerateParticles(38865, 62865);
        }

        private void GenerateParticles(double startTime, double endTime, double lifeTime = 5000, int particleCount = 100, double speed = 480)
        {
            using (var pool = new OsbSpritePool(GetLayer(""), "sb/d.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            {
                sprite.Fade(startTime, 0);
                sprite.Fade(43918, 1);
                sprite.Fade(59076,0);
            }))
            {
                var timestep = lifeTime / particleCount;
                for (var starttime = startTime; starttime <= endTime - lifeTime; starttime += timestep)
                {
                    var moveDistance = Random(60, speed) * lifeTime * 0.001f;
                    var endtime = starttime + lifeTime;
                    var sprite = pool.Get(starttime, endtime);
                    var startX = Random(-127, 767);

                    sprite.Move(starttime, endtime, startX, 490, startX + Random(-100, 100), 0);
                    sprite.Scale(starttime, Random(0.03, 0.06));
                    sprite.Additive(starttime, endtime);
                }
            }
        }
    }
}
