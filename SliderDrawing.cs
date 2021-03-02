using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class SliderDrawing : StoryboardObjectGenerator
    {
        public override void Generate()
        {
			using (var pool = new OsbSpritePool(GetLayer("slider"), "sb/hl.png", OsbOrigin.Centre, (sprite, starttime, endtime) => 
			{ }))
			{
				foreach (var hitobject in Beatmap.HitObjects)
				{
					if (hitobject.StartTime < 298493 - 5 || 309534 - 5 <= hitobject.StartTime || !(hitobject is OsuSlider)) continue;

					var beatDuration = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration;
					var timestep = beatDuration / 6;
					var startTime = hitobject.StartTime;
					while (true)
					{
						var endTime = startTime + timestep;

						var complete = hitobject.EndTime - endTime < 5;
						if (complete) endTime = hitobject.EndTime;

						var sprite = pool.Get(startTime, startTime + beatDuration * 5);
						sprite.Move(startTime, startTime + beatDuration * 5, hitobject.PositionAtTime(startTime), hitobject.PositionAtTime(startTime));
						sprite.Fade(startTime, startTime + beatDuration / 4, 0, 1);
						sprite.Fade(startTime, startTime + beatDuration * 5, 1, 0);
						sprite.Scale(startTime, startTime + beatDuration / 4, 0.25, 0.5);
						sprite.Additive(startTime, startTime + beatDuration * 5);
						sprite.Color(startTime, hitobject.Color);

						if (complete) break;
						startTime += timestep;
					}
				}
			}
        }
    }
}
