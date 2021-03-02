using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Linq;
using OpenTK;

namespace StorybrewScripts
{
    public class Parallax : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "sb/blur.jpg";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 10000;

        [Configurable]
        public int BeatDivisor = 8;

        [Configurable]
        public float MoveAmount = .1f;

        [Configurable]
        public float BackgroundScale = 1.1f;

        [Configurable]
        public float BackgroundFade = .7f;

        [Configurable]
        public OsbEasing EasingType;

        public Vector2 GetTrackedLocation(float x, float y)
        {
            var midX = 320f;
            var midY = 240f;

            var newX = -(midX - x) * MoveAmount + midX;
            var newY = -(midY - y) * MoveAmount + midY;

            return new Vector2(newX, newY);
        }

        public override void Generate()
        {
            var bgSprite = GetLayer("Foreground").CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bgSprite.Scale(StartTime, 480.0 / GetMapsetBitmap(BackgroundPath).Height * BackgroundScale);
            bgSprite.Fade(StartTime - 1, StartTime, 0, BackgroundFade);
            bgSprite.Fade(EndTime, BackgroundFade);
            var lastHitObject = Beatmap.HitObjects.First();
            foreach (var hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime < StartTime - 5 || EndTime - 5 <= hitObject.StartTime)
                {
                    lastHitObject = hitObject;
                    continue;
                }

                var oldVec = lastHitObject.PositionAtTime(lastHitObject.EndTime);
                var oldPos = GetTrackedLocation(oldVec.X, oldVec.Y);
                var newVec = hitObject.PositionAtTime(hitObject.StartTime);
                var newPos = GetTrackedLocation(newVec.X, newVec.Y);
                bgSprite.Move(EasingType, lastHitObject.EndTime, hitObject.StartTime, oldPos.X, oldPos.Y, newPos.X, newPos.Y);
                lastHitObject = hitObject;

                if (hitObject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitObject.StartTime;
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitObject.EndTime - endTime < 5;
                        if (complete) endTime = hitObject.EndTime;

                        oldVec = hitObject.PositionAtTime(startTime);
                        oldPos = GetTrackedLocation(oldVec.X, oldVec.Y);
                        newVec = hitObject.PositionAtTime(endTime);
                        newPos = GetTrackedLocation(newVec.X, newVec.Y);
                        bgSprite.Move(EasingType, startTime + 1, endTime, oldPos.X, oldPos.Y, newPos.X, newPos.Y);
                        Log("Point: " + startTime + ": " + oldVec.ToString() + ", " + newVec.ToString());
                        if (complete) Log("EndTime: " + endTime.ToString());
                        if (complete) break;
                        startTime += timestep;
                    }
                }
            }
        }
    }
}
