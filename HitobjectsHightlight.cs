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
    public class HitobjectsHightlight : StoryboardObjectGenerator
    {
        private double beatDuration;

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            var ringStartTime = new int[] { 4249, 6124, 7686, 9561, 11436, 13311, 15186, 17061, 18936, 20812, 22686 };
            RingsHighlight(ringStartTime);

            Flashlight(22686, 28329);

            GetHitobjects(43310, 58389);
            GetHitobjects(95810, 103389);
            GetHitobjects(133310, 148389);
            GetHitobjects(170810, 178389);
            GetHitobjects(189560, 195264);
            GetHitobjects(208310, 223389);
            GetHitobjects(247685, 255264);
            GetHitobjects(270185, 283310);

            KiaiHighlight(193310, 223310);
        }

        private void RingsHighlight(int[] startTime)
        {
            for (int i = 0; i < startTime.Length; i++)
            {
                var hitobject = Beatmap.HitObjects.First((hitobj) => Math.Abs(hitobj.StartTime - startTime[i]) < 5);
                if (hitobject == null) continue;

                Log($"{hitobject.StartTime}");

                var sprite = GetLayer("ring").CreateSprite("sb/c.png", OsbOrigin.Centre, hitobject.Position);
                sprite.Move(hitobject.StartTime, hitobject.StartTime + beatDuration * 4, hitobject.Position, hitobject.Position);
                sprite.Scale(OsbEasing.OutSine, hitobject.StartTime, hitobject.StartTime + beatDuration * 4, 0.1, 0.5);
                sprite.Fade(hitobject.StartTime, hitobject.StartTime + beatDuration * 4, 1, 0);
                sprite.Additive(hitobject.StartTime, hitobject.StartTime + beatDuration * 4);
            }
        }

        private void Flashlight(double startTime, double endTime)
        {
            var sprite = GetLayer("fl").CreateSprite("sb/fl.png", OsbOrigin.Centre);

            sprite.Fade(startTime, startTime + beatDuration * 12, 0, 0.75);
            var lastHitobject = Beatmap.HitObjects.First();
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime)
                { lastHitobject = hitobject; continue; }

                var startPos = sprite.PositionAt(lastHitobject.EndTime);
                sprite.Move(OsbEasing.InOutSine, lastHitobject.EndTime, hitobject.StartTime, startPos, hitobject.Position);

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
                            var startPosition = sprite.PositionAt(starttime);
                            sprite.Move(starttime, endtime, startPosition, hitobject.PositionAtTime(endtime));
                        }

                        if (complete) break;
                        starttime += timestep;
                    }
                }

                lastHitobject = hitobject;
            }
        }

        private void GetHitobjects(double startTime, double endTime)
        {
            using (OsbSpritePool dots = new OsbSpritePool(GetLayer("hl"), "sb/d.png", OsbOrigin.Centre, (dot, starttime, endtime) =>
            { }), strikes = new OsbSpritePool(GetLayer("hl"), "sb/p.png", OsbOrigin.Centre, (strike, starttime, endtime) =>
            { }))
            {
                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime) continue;

                    if (hitobject.Additions.HasFlag(HitSoundAddition.Finish))
                        GenerateHightlight(hitobject, strikes, dots);

                    if (hitobject is OsuSlider)
                    {
                        var sliderObject = (OsuSlider)hitobject;
                        foreach (var node in sliderObject.Nodes)
                        {
                            if (sliderObject.Additions.HasFlag(HitSoundAddition.Finish) || node.Additions.HasFlag(HitSoundAddition.Finish))
                                GenerateHightlight(sliderObject, strikes, dots);
                        }
                    }
                }
            }
        }

        private void GenerateHightlight(OsuHitObject hitobject, OsbSpritePool strikes, OsbSpritePool dots)
        {
            for (int i = 0; i < Random(20, 25); i++)
            {
                double angle = Random(0, Math.PI * 2);
                var radius = Random(10, 80);

                var endPosition = new Vector2(
                    (float)(hitobject.Position.X + Math.Cos(angle) * radius),
                    (float)(hitobject.Position.Y + Math.Sin(angle) * radius)
                );

                var particleDuration = Random(beatDuration * 2, beatDuration * 3);
                var dot = dots.Get(hitobject.StartTime, hitobject.StartTime + particleDuration);
                dot.Fade(hitobject.StartTime, hitobject.StartTime + particleDuration, 1, 0);
                dot.Scale(hitobject.StartTime, hitobject.StartTime + particleDuration, radius * 0.001, 0);
                dot.Move(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + particleDuration, hitobject.Position, endPosition);
                dot.Additive(hitobject.StartTime, hitobject.StartTime + particleDuration);
            }

            var strike = strikes.Get(hitobject.StartTime, hitobject.StartTime + beatDuration * 2);
            strike.Fade(hitobject.StartTime, hitobject.StartTime + beatDuration * 2, 1, 0);
            strike.ScaleVec(OsbEasing.Out, hitobject.StartTime, hitobject.StartTime + beatDuration * 2, 3, 1500, 0, 1500);
            strike.Move(hitobject.StartTime, hitobject.StartTime + beatDuration * 2, hitobject.Position, hitobject.Position);
            strike.Rotate(hitobject.StartTime, Random(-Math.PI / 3, Math.PI / 3));
            strike.Additive(hitobject.StartTime, hitobject.StartTime + beatDuration * 2);
        }

        private void KiaiHighlight(double startTime, double endTime)
        {
            using (OsbSpritePool pl = new OsbSpritePool(GetLayer("kiai"), "sb/hl.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            {

            }))
            {
                foreach (var hitobject in Beatmap.HitObjects)
                {
                    if ((hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime)) continue;

                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 6;

                    for (var starttime = hitobject.StartTime; starttime <= hitobject.EndTime; starttime += timestep)
                    {
                        var endtime = hitobject.EndTime + beatDuration * 4;
                        var sprite = pl.Get(starttime, endtime);
                        sprite.Move(starttime, hitobject.PositionAtTime(starttime));
                        sprite.Fade(starttime, endtime, 1, 0);
                        sprite.Scale(starttime, starttime + beatDuration / 4, 0.25, 0.5);
                        sprite.Additive(starttime, endtime);
                        sprite.Color(starttime, hitobject.Color);
                    }
                }
            }
        }
    }
}