using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class HL : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 20000;

        [Configurable]
        public int FadeTime = 1000;

        [Configurable]
        public string ParticlesPath = "sb/d.png";

        [Configurable]
        public string RingPath = "sb/c.png";

        [Configurable]
        public double ParticlesAmount = 25;

        [Configurable]
        public double ParticlesExplosionRadius = 80;

        [Configurable]
        public double ParticlesScale = 1;

        [Configurable]
        public double RingScale = 1;

        [Configurable]
        public double RingScaleMultuply = 2;

        public override void Generate()
        {
            var layer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) &&
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;

                for (int i = 0; i < ParticlesAmount; i++)
                {
                    var radiusX = Random(-ParticlesExplosionRadius, ParticlesExplosionRadius);
                    var radiusY = Random(-ParticlesExplosionRadius, ParticlesExplosionRadius);
                    var particle = layer.CreateSprite(ParticlesPath);

                    particle.Move(OsbEasing.OutExpo, hitobject.StartTime, hitobject.StartTime + FadeTime, hitobject.Position, hitobject.Position.X + radiusX, hitobject.Position.Y + radiusY);
                    particle.Fade(hitobject.StartTime, hitobject.StartTime + FadeTime, 1, 0);
                    particle.Scale(hitobject.StartTime, hitobject.StartTime + FadeTime, ParticlesScale * 0.1, 0);
                    particle.Additive(hitobject.StartTime, hitobject.StartTime + FadeTime);
                }


                var ring = layer.CreateSprite(RingPath, OsbOrigin.Centre, hitobject.Position);
                ring.Fade(hitobject.StartTime, hitobject.StartTime + FadeTime, 1, 0);
                ring.Scale(OsbEasing.Out, hitobject.StartTime, hitobject.StartTime + FadeTime, RingScale, RingScale * RingScaleMultuply);
                ring.Additive(hitobject.StartTime, hitobject.StartTime + FadeTime);

            }
        }
    }
}
