using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Sans : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var sprite = GetLayer("Animation").CreateAnimation("sb/undertale/sans/idle.png", 43, 35, OsbLoopType.LoopForever, OsbOrigin.Centre, new Vector2(320, 140));
            sprite.Fade(770897, 771897, 0, 1);
            sprite.Fade(785144, 1);

            var top = GetLayer("Box").CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(320, 240));
            var left = GetLayer("Box").CreateSprite("sb/pixel.png", OsbOrigin.TopCentre, new Vector2(158, 238));
            var right = GetLayer("Box").CreateSprite("sb/pixel.png", OsbOrigin.TopCentre, new Vector2(482, 238));
            var bottom = GetLayer("Box").CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(320, 360));

            top.Fade(770897, 771897, 0, 1);
            left.Fade(770897, 771897, 0, 1);
            right.Fade(770897, 771897, 0, 1);
            bottom.Fade(770897, 771897, 0, 1);

            top.Fade(785634, 1);
            left.Fade(785634, 1);
            right.Fade(785634, 1);
            bottom.Fade(785634, 1);

            top.ScaleVec(770897, 320, 4);
            left.ScaleVec(770897, 4, 124);
            right.ScaleVec(770897, 4, 124);
            bottom.ScaleVec(770897, 320, 4);

            var sans = GetLayer("Sans").CreateSprite("sb/undertale/sans/idle16.png", OsbOrigin.Centre, new Vector2(320, 140));
            sans.Fade(785144, 1);
            sans.Fade(785634, 1);

            var blackLeftEye = GetLayer("Eyes").CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(309, 98));
            var blackRightEye = GetLayer("Eyes").CreateSprite("sb/pixel.png", OsbOrigin.Centre, new Vector2(337, 98));

            blackLeftEye.Color(785144, new Color4(0, 0, 0, 255));
            blackRightEye.Color(785144, new Color4(0, 0, 0, 255));

            blackLeftEye.Scale(785144, 6);
            blackRightEye.Scale(785144, 6);

            blackLeftEye.Fade(785144, 1);
            blackLeftEye.Fade(785634, 1);
            blackRightEye.Fade(785144, 1);
            blackRightEye.Fade(785634, 1);

            var blueEye = GetLayer("Eyes").CreateSprite("sb/undertale/sans eye.png", OsbOrigin.Centre, new Vector2(338, 97.5f));
            blueEye.Color(785144, new Color4(26, 218, 247, 255));

            blueEye.Scale(785144, 0.55);

            blueEye.Fade(785144, 1);
            blueEye.Fade(785634, 1);
        }
    }
}
