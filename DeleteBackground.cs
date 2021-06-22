using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;

namespace StorybrewScripts
{
    public class DeleteBackground : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            GetLayer("").CreateSprite(Beatmap.BackgroundPath).Fade(0, 0);
        }
    }
}
