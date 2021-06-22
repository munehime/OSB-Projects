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
    public class Ray : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            AddRays(118808, 150808);
            AddRays(225475, 257475);
        }

        private void AddRays(double startTime, double endTime)
        {
            AddRay(startTime, endTime, 1, new Vector2(625, -130), 10000, 50, true);
            AddRay(startTime, endTime, 2, new Vector2(450, -120), 15000, 75);
            AddRay(startTime, endTime, 3, new Vector2(555, -160), 20000, 65, true);
        }

        private void AddRay(double startTime, double endTime, int i, Vector2 initialPosition, double wiggleDuration, double wiggleDistance, bool flip = false)
        {
            var sprite = GetLayer("ray").CreateSprite($"sb/RayofLight{i}.png", OsbOrigin.TopCentre, initialPosition);

            int loopCount = (int)Math.Ceiling(((endTime + BeatDuration * 12) - startTime) / wiggleDuration);
            sprite.StartLoopGroup(startTime, loopCount);
            sprite.MoveX(OsbEasing.InOutSine, 0, wiggleDuration * 0.5, sprite.InitialPosition.X, sprite.InitialPosition.X - 50);
            sprite.MoveX(OsbEasing.InOutSine, wiggleDuration * 0.5, wiggleDuration, sprite.InitialPosition.X - 50, sprite.InitialPosition.X);
            sprite.EndGroup();

            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, endTime + BeatDuration * 12, 1, 0);

            if (flip)
                sprite.FlipH(sprite.StartTime, sprite.EndTime);

            sprite.Additive(sprite.StartTime, sprite.EndTime);
        }
    }
}
