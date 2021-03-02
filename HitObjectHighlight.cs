using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class HitObjectHighlight : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var size = 0.55f;
		    foreach (var hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime < 649049 - 5 
                || (653628 - 5 <= hitObject.StartTime && hitObject.StartTime < 659154 - 5) 
                || (664207 - 5 <= hitObject.StartTime && hitObject.StartTime < 665470 - 5) 
                || (685681 - 5 <= hitObject.StartTime && hitObject.StartTime < 697049 - 5) 
                || (717260 - 5 <= hitObject.StartTime && hitObject.StartTime < 721049 - 5) 
                || (727207 - 5 <= hitObject.StartTime && hitObject.StartTime < 731154 - 5) 
                || (737470 - 5 <= hitObject.StartTime && hitObject.StartTime < 738733 - 5) 
                || 758944 - 5 < hitObject.StartTime)
                    continue;
                
                var sprite = GetLayer("").CreateSprite("sb/pl.png", OsbOrigin.Centre, hitObject.Position);

                sprite.Fade(hitObject.StartTime - 50, hitObject.StartTime, 0, 1);
                sprite.Fade(hitObject.StartTime, hitObject.StartTime + 1000, 1, 0);
                sprite.Scale(hitObject.StartTime - 50, hitObject.StartTime + 1000, size, size - 0.15);
                sprite.Additive(hitObject.StartTime - 50, hitObject.StartTime + 1000);

                if (hitObject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / 8;
                    var startTime = hitObject.StartTime;
                    while (true)
                    {

                        var hsprite = GetLayer("").CreateSprite("sb/pl.png", OsbOrigin.Centre, hitObject.PositionAtTime(startTime));
                        var endTime = startTime + timestep;

                        hsprite.Fade(startTime - 50, startTime, 0, 1);
                        hsprite.Fade(startTime, startTime + 1000, 1, 0);
                        hsprite.Scale(startTime - 50, startTime + 1000, size, size - 0.15);
                        hsprite.Additive(startTime - 50, startTime + 1000);

                        var complete = hitObject.EndTime - endTime < 5;
                        if (complete) endTime = hitObject.EndTime;

                        if (complete)
                            break;
                        startTime += timestep;
                    }
                } 
            } 
        }
    }
}
