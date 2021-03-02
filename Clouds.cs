using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using System;

namespace StorybrewScripts
{
    public class Clouds : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            using (var clouds = new OsbSpritePool(GetLayer(""), "sb/cloud.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            { }))
            {
                var timestep = 50000 / 12;
                for (var startTime = 567197; startTime <= 644016; startTime += timestep)
                {
                    var endTime = startTime + 50000;
                    var sprite = clouds.Get(startTime, endTime);


                    sprite.MoveY(startTime, 430);
                    sprite.MoveX(startTime, endTime, -347, 1155);

                    var startR = 0;
                    var endR = 0;
                    do
                    {
                        startR = Random(-720, 720);
                        endR = Random(-720, 720);
                    } while (Math.Abs(endR - startR) < 15 || 112 < Math.Abs(endR - startR));
                    sprite.Rotate(startTime, endTime, MathHelper.DegreesToRadians(startR), MathHelper.DegreesToRadians(endR));
                    sprite.Scale(startTime, endTime, Random(1, 1.24), Random(1, 1.25));

                    sprite.Fade(605460, 605660, 0, 1);
                    sprite.Fade(643937, 644037, 1, 0);
                }
            }
        }
    }
}
