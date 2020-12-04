using StorybrewCommon.Scripting;

namespace StorybrewScripts
{
    public class Vignette : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var bitmap = GetMapsetBitmap("sb/vig.png");
            var sprite = GetLayer("").CreateSprite("sb/vig.png");
            sprite.Scale(0, 480f / bitmap.Height);
            sprite.Fade(0, 1);
            sprite.Fade(AudioDuration, 0);
        }
    }
}
