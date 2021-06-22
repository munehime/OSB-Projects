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
    public class HitobjectsHighlight : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            KiaiHighlight(118808, 152141);
            KiaiHighlight(225475, 257475);
        }

        private void IntroHighlight()
        {
            double[] startTime = new double[] { 9475, 10362, 10808, 11706, 12141, 12808, 12919, 13030, 13475, 14808, 20141, };
            foreach (var time in startTime)
            {
                var hitobject = Beatmap.HitObjects.FirstOrDefault(hitobj => hitobj.StartTime == time);
                if (hitobject == null) continue;

                var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.Centre, hitobject.Position);
                sprite.Scale(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + BeatDuration * 2, 0, 30);

            }
        }

        private void KiaiHighlight(double startTime, double endTime)
        {
            using (var pool = new OsbSpritePool(GetLayer("kiai"), "sb/hl.png", OsbOrigin.Centre, (sprite, starttime, endtime) => { }))
            {
                foreach (var hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 <= hitObject.StartTime)
                        continue;

                    var starttime = hitObject.StartTime;
                    var endtime = hitObject.EndTime + BeatDuration * 2;

                    var sprite = pool.Get(starttime, endtime);
                    sprite.Move(starttime, hitObject.Position);

                    if (hitObject.StartTime != hitObject.EndTime)
                    {
                        sprite.Scale(starttime, 0.8);
                        sprite.Fade(starttime, 1);
                    }

                    sprite.Scale(hitObject.EndTime, endtime, 0.8, 0.2);
                    sprite.Fade(hitObject.EndTime, endtime, 1, 0);
                    sprite.Color(starttime, hitObject.Color);
                    sprite.Additive(starttime, endtime);

                    if (hitObject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / 8;
                        var start_time = hitObject.StartTime;

                        while (true)
                        {
                            var end_time = start_time + timestep;
                            var complete = hitObject.EndTime - end_time < 5;
                            if (complete) end_time = hitObject.EndTime;

                            var startPosition = hitObject.PositionAtTime(start_time);
                            sprite.Move(start_time, end_time, startPosition, hitObject.PositionAtTime(end_time));

                            if (complete)
                                break;

                            start_time += timestep;
                        }
                    }
                }
            }
        }
    }
}
