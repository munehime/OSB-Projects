using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;

namespace StorybrewScripts
{
    public class Snow : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            using (var snow = new OsbSpritePool(GetLayer(""), "sb/menu-snow.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            { }))
            {
                var timestep = 5000 / 50;
                for (var startTime = 685681; startTime <= 765650; startTime += timestep)
                {
                    var endTime = startTime + 2500;

                    var sprite = snow.Get(startTime, endTime);

                    var startX = Random(-107, 747);
                    var endX = Random(-145, 145);

                    sprite.Move(startTime, endTime, startX, -20, startX + endX, 500);
                    sprite.Scale(startTime, endTime, Random(0.1, 0.3), Random(0.15, 0.4));

                    var startR = Random(-380, 380);
                    var endR = Random(-380, 380);

                    do
                    {
                        startR = Random(-380, 380);
                        endR = Random(-380, 380);
                    } while (Math.Abs(endR - startR) < 35 || 400 < Math.Abs(endR - startR));
                    sprite.Rotate(startTime, endTime, MathHelper.DegreesToRadians(startR), MathHelper.DegreesToRadians(endR));

                    sprite.Fade(startTime, startTime + 165, 0, 1);
                    sprite.Fade(endTime - 100, endTime, 1, 0);
                    sprite.Fade(685681, 697049, 0, 0);
                    sprite.Fade(697049, 1);
                }
            }
        }
    }
}
