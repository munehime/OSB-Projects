using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Linq;

namespace StorybrewScripts
{
    public class ExplodingParticles : StoryboardObjectGenerator
    {
        double beatDuration;
        double startTime = 1316;
        double endTime = 11983;

        Vector2 center = new Vector2(320, 240);

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            for (int i = 0; i < 100; i++)
            {
                double angle = Random(0, Math.PI * 2);
                var radiusX = Random(-472, 472f);
                var radiusY = Random(-240, 240f);

                var endPosition = new Vector2(
                    (float)(center.X + Math.Cos(angle) * radiusX),
                    (float)(center.Y + Math.Sin(angle) * radiusY)
                );

                var size = Random(0.02f, 0.05f);
                var sprite = GetLayer("").CreateSprite("sb/d.png", OsbOrigin.Centre, center);
                var hitobject = Beatmap.HitObjects.FirstOrDefault(hitobj => hitobj.StartTime == endTime);

                sprite.Move(OsbEasing.OutExpo, startTime, endTime - beatDuration * 4, center, endPosition);
                sprite.Move(OsbEasing.InExpo, endTime - beatDuration * 4, endTime, endPosition, hitobject.Position);
                sprite.Scale(startTime, size);
                sprite.Additive(startTime, endTime);
            }
        }
    }
}
