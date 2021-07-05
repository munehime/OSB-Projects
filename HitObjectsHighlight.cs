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
    public class HitObjectsHighlight : StoryboardObjectGenerator
    {
        private double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            // PreIntroHighlight(8549, 13602);
            PreIntroHighlight();
            VanillaHighlight();
            Rings();
            Rings(59076, 63971);
            Rings(71707, 74234);
        }

        private void VanillaHighlight(double startTime = 43918, double endTime = 59076)
        {
            using (OsbSpritePool pool = new OsbSpritePool(GetLayer("v"), "sb/hl.png", OsbOrigin.Centre, (sprite, starttime, endtime) => { }))
            {
                foreach (var hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
                        continue;

                    var endtime = hitObject.EndTime + BeatDuration;
                    var sprite = pool.Get(hitObject.StartTime, endtime);

                    sprite.Move(hitObject.StartTime, hitObject.Position);
                    sprite.Scale(OsbEasing.OutQuad, hitObject.StartTime, endtime, 0.5, 0.8);
                    sprite.Fade(OsbEasing.InSine, hitObject.StartTime, endtime, 1, 0);
                    sprite.Additive(hitObject.StartTime, endtime);
                    sprite.Color(hitObject.StartTime, hitObject.Color);

                    if (hitObject is OsuSlider)
                    {
                        var timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / 8;
                        var start_time = hitObject.StartTime;
                        while (true)
                        {
                            var end_time = start_time + timestep;

                            var complete = hitObject.EndTime - end_time < 5;
                            if (complete) end_time = hitObject.EndTime;

                            var startPosition = sprite.PositionAt(start_time);
                            sprite.Move(start_time, end_time, startPosition, hitObject.PositionAtTime(end_time));

                            if (complete) break;
                            start_time += timestep;
                        }
                    }
                }
            }
        }

        private void Rings(double startTime = 8549, double endTime = 13602)
        {
            using (OsbSpritePool rings = new OsbSpritePool(GetLayer("rings"), "sb/c2.png", OsbOrigin.Centre, (sprite, starttime, endtime) => { }))
            {
                foreach (var hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
                        continue;

                    var ring = rings.Get(hitObject.StartTime, hitObject.StartTime + BeatDuration * 3);
                    ring.Move(hitObject.StartTime, hitObject.Position);
                    ring.Scale(OsbEasing.OutQuart, hitObject.StartTime, hitObject.StartTime + BeatDuration * 3, 0, 2);
                    ring.Fade(OsbEasing.OutQuad, hitObject.StartTime, hitObject.StartTime + BeatDuration * 3, 1, 0);
                    ring.Color(hitObject.StartTime, hitObject.Color);
                }
            }
        }

        private void PreIntroHighlight(double startTime = 41392, double endTime = 43918)
        {
            using (OsbSpritePool lines = new OsbSpritePool(GetLayer(""), "sb/p.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            {
                sprite.Color(starttime, Color4.Black);
            }),
            squares = new OsbSpritePool(GetLayer(""), "sb/p.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            {
                sprite.Color(starttime, Color4.Black);
            }),
            dots = new OsbSpritePool(GetLayer(""), "sb/d.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            {
                sprite.Color(starttime, Color4.Black);
            }),
            rings = new OsbSpritePool(GetLayer(""), "sb/c.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            {
                sprite.Color(starttime, Color4.Black);
            })
            )
            {
                foreach (var hitObject in Beatmap.HitObjects)
                {
                    if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
                        continue;

                    var angle = Random(-Math.PI / 32, Math.PI / 32);
                    var line = lines.Get(hitObject.StartTime, hitObject.StartTime + BeatDuration * 2);
                    line.Move(hitObject.StartTime, hitObject.Position);
                    line.ScaleVec(OsbEasing.OutQuart, hitObject.StartTime, hitObject.StartTime + BeatDuration * 2, 3, 960, 0, 960);
                    line.Rotate(hitObject.StartTime, hitObject.StartTime + BeatDuration * 2, angle, angle + Random(0.0002));

                    for (int i = 0; i < 8; i++)
                    {
                        var radius = Random(10, 80f);
                        var radian = Random(2 * Math.PI);
                        var endPos = new Vector2(
                            hitObject.Position.X + radius * (float)Math.Cos(radian),
                            hitObject.Position.Y + radius * (float)Math.Sin(radian)
                        );
                        var duration = Random(BeatDuration * 1.5, BeatDuration * 2.5);

                        var square = squares.Get(hitObject.StartTime, hitObject.StartTime + duration);
                        square.Move(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + duration, hitObject.Position, endPos);
                        square.Scale(hitObject.StartTime, hitObject.StartTime + duration, Random(3, 10f), 0);
                        square.Rotate(hitObject.StartTime, hitObject.StartTime + duration, radian, radian + Random(0.0002));
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        var radius = Random(10, 80f);
                        var radian = Random(2 * Math.PI);
                        var endPos = new Vector2(
                            hitObject.Position.X + radius * (float)Math.Cos(radian),
                            hitObject.Position.Y + radius * (float)Math.Sin(radian)
                        );
                        var duration = Random(BeatDuration * 1.5, BeatDuration * 2.5);

                        var dot = dots.Get(hitObject.StartTime, hitObject.StartTime + duration);
                        dot.Move(OsbEasing.OutExpo, hitObject.StartTime, hitObject.StartTime + duration, hitObject.Position, endPos);
                        dot.Scale(hitObject.StartTime, hitObject.StartTime + duration, Random(0.01, 0.3), 0);
                    }

                    var ring = rings.Get(hitObject.StartTime, hitObject.StartTime + BeatDuration * 2);
                    ring.Move(hitObject.StartTime, hitObject.Position);
                    ring.Scale(OsbEasing.OutQuart, hitObject.StartTime, hitObject.StartTime + BeatDuration * 2, 0.02, 0.23);
                    ring.Fade(hitObject.StartTime, hitObject.StartTime + BeatDuration * 2, 1, 0);
                }
            }
        }
    }
}
