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
using System.IO;
using System.Linq;

namespace StorybrewScripts
{
    public class Highlight : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            Spotlight(25374, 36345);
            Hit(47316, 68916);
            Hit(157031, 178631);
            KiaiHighlight(69259, 91202);
            KiaiHighlight(178974, 225602);
            VHighlight(154288, 157031);
            Ring();
        }

        private void Spotlight(int startTime, int endTime)
        {
            var path = new string[] { "sb/spotlightH.png", "sb/spotlightV.png" };
            var sprite = new List<OsbSprite> { };
            for (int i = 0; i < 2; i++)
            {
                sprite.Add(GetLayer("spotlight").CreateSprite(path[i], OsbOrigin.Centre));
            }

            for (int i = 0; i < 2; i++)
            {
                sprite[i].Fade(startTime, 0.75);
                sprite[i].Fade(endTime, 0);
                sprite[i].Color(startTime, 0, 0, 0);
            }

            var lastHitobject = Beatmap.HitObjects.First();
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < startTime - 5 || endTime < hitobject.StartTime - 5)
                {
                    lastHitobject = hitobject;
                    continue;
                }

                for (int i = 0; i < 2; i++)
                {
                    var startPosition = sprite[i].PositionAt(lastHitobject.EndTime);
                    sprite[i].Move(OsbEasing.Out, lastHitobject.EndTime, hitobject.StartTime, startPosition, hitobject.Position);
                }

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                    var starttime = hitobject.StartTime;
                    while (true)
                    {
                        var endtime = starttime + timestep;

                        var complete = hitobject.EndTime - endtime < 5;
                        if (complete) endtime = hitobject.EndTime;

                        for (int i = 0; i < 2; i++)
                        {
                            var startPosition = sprite[i].PositionAt(starttime);
                            sprite[i].Move(starttime, endtime, startPosition, hitobject.PositionAtTime(endtime));
                        }

                        if (complete) break;
                        starttime += timestep;
                    }
                }

                lastHitobject = hitobject;
            }
        }

        private void Hit(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < startTime - 5 || endTime < hitobject.StartTime - 5) continue;
                var sprite = GetLayer("hit").CreateSprite("sb/hit.png", OsbOrigin.Centre, hitobject.Position);

                //sprite.Scale(OsbEasing.In, hitobject.StartTime, hitobject.EndTime + 200, 1, 1 * 0.2);
                sprite.Fade(OsbEasing.In, hitobject.StartTime, hitobject.EndTime + 100, 1, 0);
                sprite.Additive(hitobject.StartTime, hitobject.EndTime + 100);
                sprite.Color(hitobject.StartTime, hitobject.Color);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                    var starttime = hitobject.StartTime;
                    while (true)
                    {
                        var endtime = starttime + timestep;

                        var complete = hitobject.EndTime - endtime < 5;
                        if (complete) endtime = hitobject.EndTime;

                        var startPosition = sprite.PositionAt(starttime);
                        sprite.Move(starttime, endtime, startPosition, hitobject.PositionAtTime(endtime));

                        if (complete) break;
                        starttime += timestep;
                    }
                }
            }
        }

        private void KiaiHighlight(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < startTime - 5 || endTime < hitobject.StartTime - 5) continue;
                var sprite = GetLayer("hl").CreateSprite("sb/glow.png", OsbOrigin.Centre, hitobject.Position);

                sprite.Scale(OsbEasing.In, hitobject.StartTime, hitobject.EndTime + 200, 0.45, 0.45 * 0.2);
                sprite.Fade(OsbEasing.In, hitobject.StartTime, hitobject.EndTime + 200, 1, 0);
                sprite.Additive(hitobject.StartTime, hitobject.EndTime + 200);
                sprite.Color(hitobject.StartTime, hitobject.Color);

                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                    var starttime = hitobject.StartTime;
                    while (true)
                    {
                        var endtime = starttime + timestep;

                        var complete = hitobject.EndTime - endtime < 5;
                        if (complete) endtime = hitobject.EndTime;

                        var startPosition = sprite.PositionAt(starttime);
                        sprite.Move(starttime, endtime, startPosition, hitobject.PositionAtTime(endtime));

                        if (complete) break;
                        starttime += timestep;
                    }
                }
            }
        }

        private void Ring()
        {
            var time = File.ReadAllLines($"{ProjectPath}/ring.txt");

            for (int i = 0; i < time.Length - 1; i++)
            {
                var t = int.Parse(time[i]);
                var hitobj = Beatmap.HitObjects.FirstOrDefault(hitobject => hitobject.StartTime > int.Parse(time[i]) - 5 && hitobject.StartTime < int.Parse(time[i]) + 5
                                                                            || hitobject.EndTime > int.Parse(time[i]) - 5 && hitobject.EndTime < int.Parse(time[i]) + 5);

                if (hitobj == null) continue;

                var position = hitobj.PositionAtTime(t);
                var sprite = GetLayer("ring").CreateSprite("sb/c.png", OsbOrigin.Centre, position);
                sprite.Scale(t, t + 750, 0.2, 0.6);
                sprite.Fade(t, t + 750, 1, 0);
                sprite.Additive(t, t + 750);

                /*                 sprite = GetLayer("strikes").CreateSprite("sb/pl.png", OsbOrigin.Centre, position);
                                sprite.ScaleVec(OsbEasing.OutExpo, t - 15, t + 500, 3, 0.2, 0, 0);
                                sprite.MoveX(OsbEasing.OutExpo, t - 15, t + 500, position.X - 350, position.X + 720);
                                sprite.Fade(t - 15, t + 500, 1, 0);
                                sprite.Additive(t - 15, t + 500); */

                for (int j = 0; j < 10; j++)
                {
                    sprite = GetLayer("ring_pariticles").CreateSprite("sb/smol.png", OsbOrigin.Centre);
                    sprite.Move(OsbEasing.Out, t, t + 750, position, position.X + Random(-50, 50), position.Y + Random(-50, 50));
                    sprite.Scale(t, t + 750, 0.07, 0);
                    sprite.Fade(t, t + 750, 1, 0);
                    sprite.Additive(t, t + 750);
                }
            }
        }

        private void VHighlight(int startTime, int endTime)
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < startTime - 5 || endTime - 5 < hitobject.StartTime) continue;

                var sprite = GetLayer("vertical").CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(hitobject.Position.X, 240));
                sprite.ScaleVec(OsbEasing.OutExpo, hitobject.StartTime - 15, hitobject.StartTime + 500, 50, 480, 0, 480);
                sprite.Fade(hitobject.StartTime - 15, hitobject.StartTime + 500, 1, 0);
            }
        }

        public Vector2 GetTrackedLocation(float x, float y)
        {
            var midX = 320;
            var midY = 240;

            var newX = -(midX - x) + midX;
            var newY = -(midY - y) + midY;

            return new Vector2(newX, newY);
        }
    }
}
