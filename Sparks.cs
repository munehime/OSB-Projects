using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Sparks : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var lastHitObject = Beatmap.HitObjects.First();
            foreach (var hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime < 1014443 - 5
                || (1030506 - 5 <= hitObject.StartTime && hitObject.StartTime < 1037693 - 5)
                || (1037756 - 5 <= hitObject.StartTime && hitObject.StartTime < 1037943 - 5)
                || (1038006 - 5 <= hitObject.StartTime && hitObject.StartTime < 1038193 - 5)
                || (1038256 - 5 <= hitObject.StartTime && hitObject.StartTime < 1038443 - 5)
                || (1038506 - 5 <= hitObject.StartTime && hitObject.StartTime < 1045693 - 5)
                || (1045756 - 5 <= hitObject.StartTime && hitObject.StartTime < 1045943 - 5)
                || (1046006 - 5 <= hitObject.StartTime && hitObject.StartTime < 1046193 - 5)
                || (1046256 - 5 <= hitObject.StartTime && hitObject.StartTime < 1046443 - 5)
                || (1046506 - 5 <= hitObject.StartTime && hitObject.StartTime < 1053693 - 5)
                || (1053756 - 5 <= hitObject.StartTime && hitObject.StartTime < 1053943 - 5)
                || (1054006 - 5 <= hitObject.StartTime && hitObject.StartTime < 1054193 - 5)
                || (1054256 - 5 <= hitObject.StartTime && hitObject.StartTime < 1054443 - 5)
                || 1054506 < hitObject.StartTime - 5)
                {
                    lastHitObject = hitObject;
                    continue;
                }

                for (var i = 0; i < 35; i++)
                {
                    var sprite = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre, hitObject.Position);

                    var randX = Random(-75, 75);
                    var randY = Random(-75, 75);

                    var deltaPos = new Vector2(0, 0);
                    if (hitObject.StartTime - 5 < 1022443 - 5 || 1027443 - 5 < hitObject.StartTime - 5)
                        deltaPos = hitObject.Position - lastHitObject.Position;

                    var startPos = hitObject.PositionAtTime(1014443);
                    var endPos = new Vector2(startPos.X + randX, startPos.Y + randY) + deltaPos;

                    sprite.Scale(hitObject.StartTime, hitObject.StartTime + 800, Random(4.0, 10.0), Random(4.0, 10.0));
                    sprite.Fade(hitObject.StartTime, hitObject.StartTime + 800, 1, 0);
                    sprite.Move(hitObject.StartTime, hitObject.StartTime + 800, startPos, endPos);
                    sprite.Rotate(hitObject.StartTime, hitObject.StartTime + 800, MathHelper.DegreesToRadians(Random(-100, 100)), MathHelper.DegreesToRadians(Random(-100, 100)));
                }

                lastHitObject = hitObject;
            }
        }
    }
}
