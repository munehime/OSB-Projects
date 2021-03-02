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
    public class HitobjectHighlight : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            GenerateHighlight(18571, 36684);
            GenerateHighlight(63854, 81967);
            GenerateHighlight(109137, 127250);
            GenerateHighlight(145364, 168833, false);
        }

        private void GenerateHighlight(double startTime, double endTime, bool kiai = true)
        {
            using (var pool = new OsbSpritePool(GetLayer("hl"), "sb/hl.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            { }))
            {
                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if (hitobject.StartTime < startTime - 5 || endTime - 5 < hitobject.StartTime) continue;

                    var endtime = (hitobject is OsuSpinner ? hitobject.EndTime : hitobject.StartTime) + BeatDuration;
                    var sprite = pool.Get(hitobject.StartTime, endtime);

                    sprite.Move(hitobject.StartTime, hitobject.Position);
                    sprite.Scale(hitobject.StartTime, endtime, 0.25, 0.5);
                    sprite.Fade(hitobject.StartTime, hitobject.StartTime + BeatDuration / 4, 0, kiai ? 1 : 0.8);
                    sprite.Fade(hitobject is OsuSpinner ? hitobject.EndTime : hitobject.StartTime + BeatDuration / 4, endtime, 0.8, 0);
                    sprite.Color(hitobject.StartTime, hitobject.Color);
                    if (kiai)
                        sprite.Additive(hitobject.StartTime, endtime);


                    if (hitobject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 12;
                        var start_time = hitobject.StartTime;
                        while (true)
                        {
                            var end_time = start_time + timestep;

                            var complete = hitobject.EndTime - end_time < 5;
                            if (complete) end_time = hitobject.EndTime;

                            var sliderSprite = pool.Get(start_time, start_time + BeatDuration);
                            sliderSprite.Move(start_time, hitobject.PositionAtTime(start_time));
                            sliderSprite.Scale(start_time, start_time + BeatDuration, 0.3, 0.6);
                            sliderSprite.Fade(start_time, start_time + BeatDuration / 4, 0, kiai ? 1 : 0.8);
                            sliderSprite.Fade(start_time + BeatDuration / 4, start_time + BeatDuration, 1, 0);
                            sliderSprite.Color(start_time, hitobject.Color);
                            if (kiai)
                                sliderSprite.Additive(start_time, start_time + BeatDuration);

                            if (complete) break;
                            start_time += timestep;
                        }
                    }
                }
            }
        }
    }
}
