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
    public class Timeline : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            var sprite = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.CentreLeft, new Vector2(0, 420));
            sprite.ScaleVec(OsbEasing.OutExpo, 0, 2722, 0, 3, 640, 3);
            sprite.Fade(AudioDuration, AudioDuration + beatDuration * 2, 1, 0);

            sprite = GetLayer("").CreateSprite("sb/d.png", OsbOrigin.Centre, new Vector2(0, 420));
            sprite.MoveX(0, AudioDuration, 0, 640);
            sprite.Scale(0, 0.2);
            sprite.Fade(AudioDuration, AudioDuration + beatDuration * 2, 1, 0);
        }
    }
}
