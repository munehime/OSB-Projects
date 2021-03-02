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
    public class Highlight : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 10000;

        [Configurable]
        public int BeatDivisor = 8;

        [Configurable]
        public int FadeTime = 200;

        [Configurable]
        public OsbEasing Easing;

        [Configurable]
        public OsbEasing EasingExplode;

        [Configurable]
        public string SpritePath = "sb/pl.png";

        [Configurable]
        public double SpriteScale = 1;

        public override void Generate()
        {
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime)
                    continue;

                var easing = Easing;
                var easingExplode = EasingExplode;
                            
                var explodeRadius = 100;
                var timeDiff = 200;
                for (var i = 0; i < 16; i++)
                {
                    var particle = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);

                    var randX = Random(-explodeRadius, explodeRadius);
                    var randY = Random(-explodeRadius, explodeRadius);
                    var startPos = hitobject.PositionAtTime(StartTime);
                    var endPos = new Vector2(startPos.X + randX, startPos.Y + randY);

                    particle.Move(easingExplode, hitobject.StartTime, hitobject.StartTime + timeDiff, startPos, endPos);
                    particle.Scale(easing, hitobject.StartTime, hitobject.EndTime + FadeTime, SpriteScale, 0.6);
                    particle.Fade(easing, hitobject.StartTime, hitobject.EndTime + FadeTime, 1, 0);
                    particle.Additive(hitobject.StartTime, hitobject.EndTime + FadeTime);
                    particle.Color(hitobject.StartTime, hitobject.Color);
                }                
            }
        }
    }
}
