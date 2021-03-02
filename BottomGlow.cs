using OpenTK;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class BottomGlow : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var sprite = GetLayer("glow").CreateSprite("sb/bottom.png", OsbOrigin.BottomCentre, new Vector2(320, 480));
            sprite.ScaleVec(90038, 1.08, 1);
            sprite.Fade(90038, 0);

            /*
            //Moonscraper
            sprite.StartLoopGroup(90038, 2);
            sprite.Fade(0, 293, 1, 0);
            sprite.EndGroup();
            sprite.Fade(90584, 91084, 1, 0);

            sprite.Fade(94402, 94902, 1, 0);

            sprite.StartLoopGroup(98765, 2);
            sprite.Fade(0, 410, 1, 0);
            sprite.EndGroup();
            sprite.Fade(99584, 100084, 1, 0);

            sprite.Fade(100947, 101447, 1, 0);
            sprite.Fade(105311, 105811, 1, 0);
            sprite.Fade(107220, 107492, 1, 0);
            sprite.Fade(107493, 107993, 1, 0);
            sprite.Fade(109675, 110175, 1, 0);
            sprite.Fade(109675, 110175, 1, 0);
            sprite.Fade(111856, 112356, 1, 0);
            sprite.Fade(117311, 117811, 1, 0);
            sprite.Fade(118402, 118902, 1, 0);

            //Bye Bye YESTERDAY
            sprite.Fade(243755, 244755, 1, 0);
            sprite.Fade(254422, 255422, 1, 0);

            //Dear Brave
            sprite.Fade(365160, 366160, 1, 0);
            sprite.Fade(392072, 393072, 1, 0);

            //Caffein Fighter
            sprite.Fade(459129, 460129, 1, 0);

            //Sakura no Zenya
            sprite.Fade(497021, 498021, 1, 0);
            sprite.Fade(510504, 511504, 1, 0);
            sprite.Fade(532077, 533077, 1, 0);

            //SANTA SAN
            sprite.Fade(659154, 660154, 1, 0);
            sprite.Fade(665470, 666470, 1, 0);
            sprite.Fade(722312, 723312, 1, 0);
            sprite.Fade(732418, 733418, 1, 0);
            sprite.Fade(738733, 739733, 1, 0);

            //Reality Check Through The Skull
            sprite.Fade(800404, 801404, 1, 0);


            //Resposibility Response
            sprite.Fade(887852, 888852, 1, 0);

            //Can't Defeat Airman
            sprite.Fade(911598, 912598, 1, 0);

            //Monochrome butterfly
            sprite.Fade(958011, 959011, 1, 0);
            */

            //FREEDOM DIVE
            sprite.Fade(1022443, 1023443, 1, 0);
            sprite.Fade(1024443, 1025443, 1, 0);
            sprite.Fade(1026443, 1027443, 1, 0);

            sprite.StartLoopGroup(1027443, 3);
            sprite.Fade(0, 500, 1, 0);
            sprite.EndGroup();

            sprite.StartLoopGroup(1028943, 6);
            sprite.Fade(0, 250, 1, 0);
            sprite.EndGroup();

            sprite.StartLoopGroup(1037693, 3);
            sprite.Fade(0, 250, 1, 0);
            sprite.EndGroup();
            sprite.Fade(1038443, 1038943, 1, 0);

            sprite.StartLoopGroup(1045693, 3);
            sprite.Fade(0, 250, 1, 0);
            sprite.EndGroup();
            sprite.Fade(1046443, 1046943, 1, 0);

            sprite.StartLoopGroup(1053693, 3);
            sprite.Fade(0, 250, 1, 0);
            sprite.EndGroup();
            sprite.Fade(1054443, 1054943, 1, 0);
        }
    }
}
