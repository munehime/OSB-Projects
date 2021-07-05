using StorybrewCommon.Scripting;

namespace StorybrewScripts
{
    public class Vignette : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var beatDuration = Beatmap.GetTimingPointAt((int)AudioDuration).BeatDuration;
            var bitmap = GetMapsetBitmap("sb/vig.png");
            var sprite = GetLayer("").CreateSprite("sb/vig.png");
            sprite.Scale(0, 480f / bitmap.Height);
            sprite.Fade(-beatDuration * 4, 0, 0, 0.15);
            sprite.Fade(78023 + beatDuration * 2, 78023 + beatDuration * 10, 0.15, 0);
        }
    }
}
