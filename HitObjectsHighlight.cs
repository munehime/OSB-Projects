using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;
using System.Linq;

namespace StorybrewScripts
{
    public class HitObjectsHighlight : StoryboardObjectGenerator
    {
        double beatDuration;

        public override void Generate()
        {
            GenerateSingleHighlight();
        }

        private void GenerateSingleHighlight()
        {
            using (OsbSpritePool dots = new OsbSpritePool(GetLayer("hl"), "sb/d.png", OsbOrigin.Centre, (dot, starttime, endtime) =>
            { }), rings = new OsbSpritePool(GetLayer("hl"), "sb/c.png", OsbOrigin.Centre, (strike, starttime, endtime) =>
            { }))
            {
                int i = 1;
                foreach (var bookmark in Beatmap.Bookmarks)
                {
                    Log($"{i++}. {bookmark}");
                    var hitobject = Beatmap.HitObjects.FirstOrDefault(hitobj => (bookmark - 5 < hitobj.StartTime && hitobj.StartTime <= bookmark + 5) || (bookmark - 5 < hitobj.EndTime && hitobj.EndTime <= bookmark + 5));

                    if (hitobject == null) continue;

                    beatDuration = Beatmap.GetTimingPointAt(bookmark).BeatDuration;
                    GenerateHighlight(hitobject, rings, dots);
                }
            }
        }

        private void GenerateHighlight(OsuHitObject hitobject, OsbSpritePool rings, OsbSpritePool dots)
        {

            for (int i = 0; i < Random(20, 25); i++)
            {
                double angle = Random(0, Math.PI * 2);
                var radius = Random(10, 80);

                var endPosition = new Vector2(
                    (float)(hitobject.Position.X + Math.Cos(angle) * radius),
                    (float)(hitobject.Position.Y + Math.Sin(angle) * radius)
                );

                var particleDuration = Random(beatDuration * 2, beatDuration * 4);
                var dot = dots.Get(hitobject.StartTime, hitobject.StartTime + particleDuration);
                dot.Fade(hitobject.StartTime, hitobject.StartTime + particleDuration, 1, 0);
                dot.Scale(hitobject.StartTime, hitobject.StartTime + particleDuration, radius * 0.001, 0);
                dot.Move(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + particleDuration, hitobject.Position, endPosition);
                dot.Additive(hitobject.StartTime, hitobject.StartTime + particleDuration);
            }

            var ring = rings.Get(hitobject.StartTime, hitobject.StartTime + beatDuration * 2);
            ring.Move(hitobject.StartTime, hitobject.Position);
            ring.Fade(hitobject.StartTime, hitobject.StartTime + beatDuration * 2, 1, 0);
            ring.Scale(OsbEasing.Out, hitobject.StartTime, hitobject.StartTime + beatDuration * 2, 0, 0.25);
            ring.Additive(hitobject.StartTime, hitobject.StartTime + beatDuration * 2);
        }
    }
}
