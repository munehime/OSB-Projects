using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;
using System.Linq;

namespace StorybrewScripts
{
    public class CursorDance : StoryboardObjectGenerator
    {
        private float normalSize = 1f;
        private float expandSize = 1.4f;

        public override void Generate()
        {
            var cursor = GetLayer("cursor").CreateSprite("sb/cursor/cursor.png", OsbOrigin.Centre);
            var lastHitobject = Beatmap.HitObjects.First();
            cursor.Fade(1022243, 1022443, 0, 1);
            var timestep = 0d;
            foreach (var hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime - 5 < 1014443 || 1062443 <= hitObject.StartTime - 5)
                {
                    lastHitobject = hitObject;
                    continue;
                }
                var startPosition = cursor.PositionAt(lastHitobject.EndTime);

                if (75 <= hitObject.StartTime - lastHitobject.EndTime)
                    cursor.Scale(hitObject.StartTime, hitObject.EndTime + 75, expandSize, normalSize);
                else
                    cursor.Scale(hitObject.StartTime, hitObject.EndTime + (hitObject.StartTime - lastHitobject.EndTime) - 10, expandSize, normalSize);

                cursor.Move(OsbEasing.OutSine, lastHitobject.EndTime, hitObject.StartTime, startPosition, hitObject.Position);

                if (hitObject is OsuSlider)
                {
                    timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / 4;
                    var startTime = hitObject.StartTime;

                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitObject.EndTime - endTime < 5;
                        if (complete) endTime = hitObject.EndTime;

                        startPosition = cursor.PositionAt(startTime);
                        cursor.Move(OsbEasing.OutSine, startTime, endTime, startPosition, hitObject.PositionAtTime(endTime));

                        if (complete) break;
                        startTime += timestep;
                    }
                }

                if (hitObject is OsuSpinner)
                {
                    timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / 16;
                    var startTime = hitObject.StartTime;
                    double rad = -0.5 * Math.PI;

                    while (true)
                    {

                        double x = 30 * Math.Cos(rad) + 320;
                        double y = 30 * Math.Sin(rad) + 240;

                        var endTime = startTime + timestep;

                        var complete = hitObject.EndTime - endTime < 5;
                        if (complete) endTime = hitObject.EndTime;

                        startPosition = cursor.PositionAt(startTime);
                        cursor.Move(OsbEasing.OutSine, startTime, endTime, startPosition, x, y);
                        rad += (Math.PI * 2) / 16;

                        if (complete) break;
                        startTime += timestep;
                    }
                }
                lastHitobject = hitObject;
            }

            timestep = Beatmap.GetTimingPointAt(1026443).BeatDuration / 64;
            var lastTime = 1026443d;

            using (var pool = new OsbSpritePool(GetLayer("trail"), "sb/cursor/cursortrail.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            { }))
            {
                for (var starttime = (double)1026443; starttime <= 1062443; starttime += timestep)
                {
                    if (cursor.PositionAt(starttime).X - cursor.PositionAt(lastTime).X == 0 && cursor.PositionAt(starttime).Y - cursor.PositionAt(lastTime).Y == 0) continue;

                    var endtime = starttime + 700;
                    var trail = pool.Get(starttime, endtime);

                    trail.Move(starttime, cursor.PositionAt(starttime));
                    trail.Fade(starttime, endtime, 1, 0);
                    trail.Additive(starttime, endtime);
                    lastTime = starttime;
                }
            }
        }
    }
}
