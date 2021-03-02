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
    public class Flash : StoryboardObjectGenerator
    {
        private double beatDuration;

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;

            AddKiaiFlash(43310, 58310);
            AddKiaiFlash(95810, 103310);
            AddKiaiFlash(133310, 148310);
            AddKiaiFlash(170810, 178310);
            AddKiaiFlash(189560, 193310);
            AddKiaiFlash(208310, 223310);
            AddKiaiFlash(247685, 255185);
            AddKiaiFlash(270185, 283310);

            AddNormalFlash(58310, 0.25, 0.5);
            AddNormalFlash(73310, 0.25, 0.5);
            AddNormalFlash(88310, 0.25, 0.5);
            AddNormalFlash(103310, 0.25, 0.5);
            AddNormalFlash(110810, 0.25, 0.5);
            AddNormalFlash(114560, 0.375, 0.3);
            AddNormalFlash(115498, 0.375, 0.3);
            AddNormalFlash(116435, 0.375, 0.3);
            AddNormalFlash(117373, 0.375, 0.3);
            AddNormalFlash(118310, 0.25, 1);
            AddNormalFlash(125810, 0.25, 0.5);
            AddNormalFlash(148310, 0.25, 0.5);
            AddNormalFlash(163310, 0.25, 0.5);
            AddNormalFlash(178310, 0.25, 0.5);
            AddNormalFlash(185810, 0.25, 0.5);
            AddNormalFlash(193310, 0.25, 1);
            AddNormalFlash(200810, 0.25, 0.5);
            AddNormalFlash(223310, 0.25, 1);
            AddNormalFlash(255185, 0.25, 0.5);
            AddNormalFlash(283310, 0.25, 1);
        }

        private void AddNormalFlash(double startTime, double beatDivisor, double startOpacity)
        {
            var sprite = GetLayer("normal").CreateSprite("sb/p.png");
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Fade(startTime, startTime + beatDuration / beatDivisor, startOpacity, 0);
        }

        private void AddKiaiFlash(double startTime, double endTime, bool strongFlashAtFirst = false)
        {
            var sprite = GetLayer("kiai").CreateSprite("sb/p.png");
            sprite.ScaleVec(startTime, 854, 480);
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime)) continue;

                if (hitobject.Additions.HasFlag(HitSoundAddition.Finish))
                {
                    if (strongFlashAtFirst)
                        sprite.Fade(hitobject.StartTime, hitobject.StartTime + beatDuration * 1.5, 1, 0);
                    else
                        sprite.Fade(hitobject.StartTime, hitobject.StartTime + beatDuration * 1.5, 0.5, 0);
                }

                if (hitobject is OsuSlider)
                {
                    var sliderObject = (OsuSlider)hitobject;
                    foreach (var node in sliderObject.Nodes)
                    {
                        if (sliderObject.Additions.HasFlag(HitSoundAddition.Finish) || node.Additions.HasFlag(HitSoundAddition.Finish))
                        {
                            if (strongFlashAtFirst)
                                sprite.Fade(hitobject.StartTime, hitobject.StartTime + beatDuration * 1.5, 1, 0);
                            else
                                sprite.Fade(hitobject.StartTime, hitobject.StartTime + beatDuration * 1.5, 0.5, 0);
                        }
                    }
                }
            }
        }
    }
}
