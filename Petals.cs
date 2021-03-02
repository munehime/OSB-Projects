using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;

namespace StorybrewScripts
{
    public class Petals : StoryboardObjectGenerator
    {
        private int StartTime = 402725;
        private int EndTime = 442591;
        private string[] Color = new string[] { "#B198B8", "#EACFE5", "#F5C2DD" };

        public override void Generate()
        {
            using (var petals = new OsbSpritePool(GetLayer(""), "sb/petal.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            { }))
            {
                var timestep = 2500 / 20;
                var startY = 296.0;
                var YStep = (216.0 / 39866.0);

                for (var startTime = StartTime; startTime <= EndTime; startTime += timestep)
                {
                    if (442120 <= startTime) break;
                    var endTime = startTime + 2500;
                    startY -= YStep * 50;

                    var sprite = petals.Get(startTime, endTime);

                    var endX = Random(-44, 685);
                    var endY = Random(-10, 245);

                    sprite.Move(OsbEasing.In, startTime, endTime, 46, startY, endX, endY);
                    sprite.Scale(startTime, endTime, Random(0.1, 0.3), Random(0.5, 1));

                    var startR = 0;
                    var endR = 0;

                    do
                    {
                        startR = Random(-380, 380);
                        endR = Random(-380, 380);
                    } while (Math.Abs(endR - startR) < 25 || 360 < Math.Abs(endR - startR));
                    sprite.Rotate(startTime, endTime, MathHelper.DegreesToRadians(startR), MathHelper.DegreesToRadians(endR));

                    sprite.Color(startTime, Color[Random(0, 3)]);

                    sprite.Fade(OsbEasing.In, startTime, startTime + 165, 0, 1);
                    sprite.Fade(endTime - 300, endTime, 1, 0);

                    sprite.Fade(442021, 445027, 0, 0);
                }
            }
        }
    }
}
