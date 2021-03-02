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
            sprite.Fade(0, 1);
            sprite.Fade(AudioDuration - beatDuration * 8, AudioDuration + beatDuration * 8, 1, 0);
        }
    }
}
