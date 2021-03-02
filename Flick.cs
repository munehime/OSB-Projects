using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Flick : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var sprite = GetLayer("").CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(320, 220));

            sprite.ScaleVec(785173, 400, 320);
            sprite.Color(785173, new Color4(0, 0, 0, 255));
            sprite.StartLoopGroup(785173, 8);
            sprite.Fade(0, 29, 1, 1);
            sprite.Fade(0, 58, 0, 0);
            sprite.EndGroup();
        }
    }
}
