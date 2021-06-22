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
        private int ParticleCount = 50;
        private double Speed = 100;

        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            WindowsParticles(118808, 150808);
            WindowsParticles(225475, 257475);

        }

        private void WindowsParticles(double startTime, double endTime)
        {
            using (var pool = new OsbSpritePool(GetLayer("windows"), "sb/d.png", OsbOrigin.Centre, (sprite, starttime, endtime) => { }))
            {
                var starttime = startTime;
                while (starttime <= endTime)
                {
                    var lifeTime = Random(BeatDuration * 6, BeatDuration * 18);
                    var timeStep = lifeTime / ParticleCount;
                    var endtime = starttime + lifeTime;

                    var moveAngle = Random(-Math.PI, -Math.PI * 2);
                    var moveDistance = (float)Speed * (float)lifeTime * 0.001f;

                    var startPosition = new Vector2(Random(412, 757f), Random(-5, 0));
                    var endPosition = startPosition + new Vector2((float)Math.Cos(moveAngle), (float)Math.Sin(moveAngle)) * moveDistance;

                    var sprite = pool.Get(starttime, endtime);
                    sprite.Scale(starttime, Random(0.01, 0.07));
                    sprite.Fade(OsbEasing.InCubic, starttime, endtime, 1, 0);

                    sprite.Additive(starttime, endtime);
                    sprite.Move(starttime, endtime, startPosition, endPosition);

                    starttime += timeStep;
                }
            }
        }
    }
}
