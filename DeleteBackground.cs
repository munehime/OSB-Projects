using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class DeleteBackground : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var bg = GetLayer("").CreateSprite(Beatmap.BackgroundPath);
            bg.Scale(0, 0);
            bg.Fade(0, 0);
        }
    }
}
