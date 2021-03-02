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
    public class Particles2 : StoryboardObjectGenerator
    {   
        [Configurable]
        public int FadeInDuration = 200;

        [Configurable]
        public int FadeOutDuration = 200;    

        [Configurable]
        public Color4 Color = new Color4(1, 1, 1, 0.6f);

        [Configurable]
        public OsbEasing EasingFade;

        [Configurable]
        public OsbEasing EasingMove;

        public override void Generate()
        {
            var layer = GetLayer("");
            var StartTime = 177610;
            var EndTime = 216529;
            var particleDuration = 2000;
            var ParticleAmount = 16;
            var particleSize = 0.5; 
            var Path = "SB/dot.png";

            using (var pool = new OsbSpritePool(layer, Path, OsbOrigin.Centre, (sprite, startTime, endTime) =>
            {
                if (Color.R < 1 || Color.G < 1 || Color.B < 1)
                    sprite.Color(startTime, Color);

                if (Color.A < 1)
                    sprite.Fade(startTime, Color.A);
            }))
            {
                var timeStep = particleDuration / ParticleAmount;
                for (var startTime = (double)StartTime; startTime <= EndTime - particleDuration; startTime += timeStep)
                {
                    var endTime = startTime + particleDuration;
                    var sprite = pool.Get(startTime, endTime);
                    
                    var easingFade = EasingFade;
                    var easingMove = EasingMove;
                    var startX = Random(-130, 765);
                    var endX = Random(-130, 765);
                    var startY = Random(-20, 500);
                    var endY = Random(-20, 500);
                    var fadeInTime = startTime + FadeInDuration;
                    var fadeOutTime = endTime - FadeOutDuration;
                    
                    sprite.Move(easingMove, startTime, endTime, startX, startY, endX, endY);
                    sprite.Scale(startTime, endTime, particleSize, particleSize);
                    sprite.Fade(easingFade, startTime, Math.Max(startTime, fadeInTime), 0, 1);
                    sprite.Fade(easingFade, Math.Min(fadeOutTime, endTime), endTime, 1, 0);
                }
            }
        }
    }
}
