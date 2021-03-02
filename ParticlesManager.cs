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
    public class ParticlesManager : StoryboardObjectGenerator
    {
        private double beatDuration;


        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            GenerateCenterParticles(270185, 283935);
        }

        private void GenerateCenterParticles(double startTime, double endTime)
        {
            var particleDuration = 5000;
            var particleAmonut = 100;

            using (var pool = new OsbSpritePool(GetLayer("center"), "sb/d.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            { }))
            {
                var timestep = particleDuration / particleAmonut;
                for (var start_time = startTime; start_time <= endTime - particleDuration; start_time += timestep)
                {
                    var end_time = start_time + particleDuration;
                    var sprite = pool.Get(start_time, end_time);
                    var startX = Random(-127, 767);
                    var endY = Random(-20, 500);

                    sprite.Move(start_time, end_time, startX, 480, startX + Random(-25, 25), Random(50, 380));
                    sprite.Fade(OsbEasing.OutSine, start_time, start_time + beatDuration * 4, 0, 1);
                    sprite.Fade(end_time - beatDuration * 4, end_time, 1, 0);
                    sprite.Scale(start_time, Random(0.03, 0.06));
                    sprite.Additive(start_time, end_time);
                }
            }
        }
    }
}
