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
    public class Parallax : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            GenerateParallax(47316, 68916);
            GenerateParallax(157031, 178631);
        }

        private void GenerateParallax(int startTime, int endTime)
        {
            var path = Beatmap.BackgroundPath;
            var sprite = GetLayer("").CreateSprite(path, OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap(path);
            sprite.Fade(startTime - 1, 0);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);
            sprite.Color(startTime, new Color4(255, 128, 128, 255));
            sprite.Scale(startTime, (480f / bitmap.Height) * 1.01f);

            var lastHitobject = Beatmap.HitObjects.First();
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < startTime - 5 || endTime < hitobject.StartTime - 5)
                {
                    lastHitobject = hitobject;
                    continue;
                }

                var oldVec = hitobject.PositionAtTime(hitobject.EndTime);
                var oldPos = GetTrackedLocation(oldVec.X, oldVec.Y);
                var newVec = hitobject.PositionAtTime(hitobject.StartTime);
                var newPos = GetTrackedLocation(newVec.X, newVec.Y);
                sprite.Move(lastHitobject.EndTime, hitobject.StartTime, oldPos.X, oldPos.Y, newPos.X, newPos.Y);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                    var starttime = hitobject.StartTime;
                    while (true)
                    {
                        var endtime = starttime + timestep;

                        var complete = hitobject.EndTime - endtime < 5;
                        if (complete) endtime = hitobject.EndTime;

                        oldVec = hitobject.PositionAtTime(starttime);
                        oldPos = GetTrackedLocation(oldVec.X, oldVec.Y);
                        newVec = hitobject.PositionAtTime(endtime);
                        newPos = GetTrackedLocation(newVec.X, newVec.Y);
                        sprite.Move(starttime, endtime, oldPos.X, oldPos.Y, newPos.X, newPos.Y);
                        if (complete) break;
                        starttime += timestep;
                    }
                }

                lastHitobject = hitobject;
            }
        }

        public Vector2 GetTrackedLocation(float x, float y)
        {
            var MoveAmount = 0.01f;
            var midX = 320f;
            var midY = 240f;

            var newX = -(midX - x) * MoveAmount + midX;
            var newY = -(midY - y) * MoveAmount + midY;

            return new Vector2(newX, newY);
        }
    }
}
