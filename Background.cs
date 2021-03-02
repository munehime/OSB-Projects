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
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace StorybrewScripts
{
    public class Data
    {
        [JsonProperty()]
        public ICollection<int> timeList { get; set; }
    }

    public class Background : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var path = "sb/vig.png";
            var bitmap = GetMapsetBitmap(path);
            var sprite = GetLayer("vig").CreateSprite(path, OsbOrigin.Centre);

            sprite.Scale(688, 480f / bitmap.Height);
            sprite.Fade(688, 1);
            sprite.Fade(247545, 1);

            path = Beatmap.BackgroundPath;
            bitmap = GetMapsetBitmap(path);
            sprite = GetLayer("bg").CreateSprite(path, OsbOrigin.Centre);

            sprite.Scale(OsbEasing.OutExpo, 688, 688 + 800, 480f / bitmap.Height * 1.2f, 480f / bitmap.Height);
            sprite.Fade(688, 1);
            sprite.Fade(22631, 0);
            sprite.Fade(25374, 1);
            sprite.Fade(47316, 0);
            sprite.Fade(69259, 1);
            sprite.Fade(91202, 0);
            sprite.Fade(135088, 1);
            sprite.Fade(154374, 0);
            sprite.Fade(157031, 1);
            sprite.Fade(247545, 1);

            path = "sb/bg/bw.jpg";
            sprite = GetLayer("bw").CreateSprite(path, OsbOrigin.Centre);
            sprite.Scale(113145, 480f / bitmap.Height);
            sprite.Fade(113145, 1);
            sprite.Fade(135088, 0);

            BlurGlowSat();
            Fog(47316, 69259);
            Fog(157031, 178974);
            Fog(200917, 225602);
        }

        private void BlurGlowSat()
        {
            var scale = 1.01f;
            var path = "sb/bg/blur_glow_sat.jpg";
            var bitmap = GetMapsetBitmap(path);
            var size = 480f / bitmap.Height;
            var sprite = GetLayer("blur_glow_sat").CreateSprite(path, OsbOrigin.Centre);

            sprite.Fade(11659, 11659 + tick(11659, 0.5), 1, 0);
            sprite.Scale(11659, 11659 + tick(11659, 0.5), size * scale, size);
            sprite.Fade(17145, 17145 + tick(17145, 0.5), 1, 0);
            sprite.Scale(17145, 17145 + tick(17145, 0.5), size * scale, size);
            sprite.StartLoopGroup(19888, 4);
            sprite.Fade(0, tick(19888, 1), 1, 0);
            sprite.Scale(0, tick(19888, 1), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(21259, 4);
            sprite.Fade(0, tick(21259, 2), 1, 0);
            sprite.Scale(0, tick(21259, 2), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(21945, 4);
            sprite.Fade(0, tick(21945, 4), 1, 0);
            sprite.Scale(0, tick(21945, 4), size * scale, size);
            sprite.EndGroup();
            sprite.Fade(22288, 22288 + tick(22288, 2), 1, 0);
            sprite.Scale(22288, 22288 + tick(22288, 2), size * scale, size);
            sprite.StartLoopGroup(22459, 2);
            sprite.Fade(0, tick(22459, 4), 1, 0);
            sprite.Scale(0, tick(22459, 4), size * scale, size);
            sprite.EndGroup();

            sprite.Color(36345, 1, 1, 1);
            sprite.Fade(36345, 36345 + tick(36345, 0.5), 1, 0);
            sprite.Scale(36345, 36345 + tick(36345, 0.5), size * scale, size);
            sprite.StartLoopGroup(41831, 4);
            sprite.Fade(0, tick(41831, 0.5), 1, 0);
            sprite.Scale(0, tick(41831, 0.5), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(44574, 4);
            sprite.Fade(0, tick(44574, 2), 1, 0);
            sprite.Scale(0, tick(44574, 2), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(45259, 8);
            sprite.Fade(0, tick(45259, 4), 1, 0);
            sprite.Scale(0, tick(45259, 4), size * scale, size);
            sprite.EndGroup();
            sprite.Fade(45945, 45945 + tick(45945, 1), 1, 0);
            sprite.Scale(45945, 45945 + tick(45945, 1), size * scale, size);

            sprite.StartLoopGroup(47316, 4);
            sprite.Fade(0, 343, 1, 0);
            sprite.Scale(0, 343, size * scale, size);
            sprite.Fade(OsbEasing.Out, 343, 1029, 1, 0);
            sprite.Scale(OsbEasing.Out, 343, 1029, size * scale, size);
            sprite.Fade(1029, 2743, 0, 0);
            sprite.Scale(1029, 2743, size, size);
            sprite.EndGroup();

            sprite.StartLoopGroup(58288, 2);
            sprite.Fade(0, tick(58288, 1), 1, 0);
            sprite.Scale(0, tick(58288, 1), size * scale, size);
            sprite.Fade(tick(58288, 1), tick(58288, 0.125), 0, 0);
            sprite.Scale(tick(58288, 1), tick(58288, 0.125), size, size);
            sprite.EndGroup();
            sprite.Fade(63774, 65145, 1, 0);
            sprite.Scale(63774, 65145, size * scale, size);
            sprite.StartLoopGroup(65145, 2);
            sprite.Fade(0, tick(65145, 0.5), 1, 0);
            sprite.Scale(0, tick(65145, 0.5), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(66516, 4);
            sprite.Fade(0, tick(66516, 1), 1, 0);
            sprite.Scale(0, tick(66516, 1), size * scale, size);
            sprite.EndGroup();

            sprite.Color(69259, new Color4(0, 255, 64, 0));
            sprite.StartLoopGroup(69259, 55);
            sprite.Fade(0, tick(69259, 1), 1, 0);
            sprite.Scale(0, tick(69259, 1), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(88116, 4);
            sprite.Fade(0, tick(88116, 4), 1, 0);
            sprite.Scale(0, tick(88116, 4), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(88459, 4);
            sprite.Fade(0, tick(88459, 2), 1, 0);
            sprite.Scale(0, tick(88459, 2), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(89145, 8);
            sprite.Fade(0, tick(89145, 4), 1, 0);
            sprite.Scale(0, tick(89145, 4), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(89831, 6);
            sprite.Fade(0, tick(89831, 2), 1, 0);
            sprite.Scale(0, tick(89831, 2), size * scale, size);
            sprite.EndGroup();
            sprite.Fade(90859, 91202, 1, 0);
            sprite.Scale(90859, 91202, size * scale, size);
            sprite.Color(91202, 1, 1, 1);

            sprite.Fade(113145, 113831, 1, 0);
            sprite.Scale(113145, 113831, size * scale, size);
            sprite.Fade(124116, 124802, 1, 0);
            sprite.Scale(124116, 124802, size * scale, size);
            sprite.Fade(135088, 135774, 1, 0);
            sprite.Scale(135088, 135774, size * scale, size);

            sprite.StartLoopGroup(146059, 16);
            sprite.Fade(0, tick(146059, 1), 0.5, 0);
            sprite.Scale(0, tick(146059, 1), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(151545, 12);
            sprite.Fade(0, tick(151545, 2), 1, 0);
            sprite.Scale(0, tick(151545, 2), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(153602, 8);
            sprite.Fade(0, tick(153602, 4), 1, 0);
            sprite.Scale(0, tick(153602, 4), size * scale, size);
            sprite.EndGroup();

            sprite.Color(178974, new Color4(0, 255, 64, 0));
            sprite.StartLoopGroup(178974, 55);
            sprite.Fade(0, tick(178974, 1), 1, 0);
            sprite.Scale(0, tick(178974, 1), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(197831, 4);
            sprite.Fade(0, tick(197831, 4), 1, 0);
            sprite.Scale(0, tick(197831, 4), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(198174, 4);
            sprite.Fade(0, tick(198174, 2), 1, 0);
            sprite.Scale(0, tick(198174, 2), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(198859, 8);
            sprite.Fade(0, tick(198859, 4), 1, 0);
            sprite.Scale(0, tick(198859, 4), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(199545, 6);
            sprite.Fade(0, tick(199545, 2), 1, 0);
            sprite.Scale(0, tick(199545, 2), size * scale, size);
            sprite.EndGroup();
            sprite.Fade(200574, 200917, 1, 0);
            sprite.Scale(200574, 200917, size * scale, size);
            sprite.Color(200917, 1, 1, 1);
            sprite.StartLoopGroup(200917, 48);
            sprite.Fade(0, tick(200917, 1), 0.75, 0);
            sprite.Scale(0, tick(200917, 1), size * scale, size);
            sprite.EndGroup();

            for (int i = 0; i < Beatmap.HitObjects.Count(); i++)
            {
                var hitobject = Beatmap.HitObjects.ToArray();
                if (hitobject[i].StartTime < 217374 - 5 || 225259 < hitobject[i].StartTime - 5)
                {
                    continue;
                }

                sprite.Fade(hitobject[i].StartTime, hitobject[i + 1].StartTime, 0.75, 0);
                sprite.Scale(hitobject[i].StartTime, hitobject[i + 1].StartTime, size * 1.01f, size);
            }

            sprite.Fade(236574, 236574 + tick(236574, 0.5), 1, 0);
            sprite.Scale(236574, 236574 + tick(236574, 0.5), size * scale, size);
            sprite.Fade(242059, 242059 + tick(242059, 0.5), 1, 0);
            sprite.Scale(242059, 242059 + tick(242059, 0.5), size * scale, size);
            sprite.StartLoopGroup(244802, 4);
            sprite.Fade(0, tick(244802, 1), 1, 0);
            sprite.Scale(0, tick(244802, 1), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(246174, 4);
            sprite.Fade(0, tick(246174, 2), 1, 0);
            sprite.Scale(0, tick(246174, 2), size * scale, size);
            sprite.EndGroup();
            sprite.StartLoopGroup(246859, 4);
            sprite.Fade(0, tick(246859, 4), 1, 0);
            sprite.Scale(0, tick(246859, 4), size * scale, size);
            sprite.EndGroup();
            sprite.Fade(247202, 247202 + tick(247202, 2), 1, 0);
            sprite.Scale(247202, 247202 + tick(247202, 2), size * scale, size);
            sprite.StartLoopGroup(247374, 2);
            sprite.Fade(0, tick(247374, 4), 1, 0);
            sprite.Scale(0, tick(247374, 4), size * scale, size);
            sprite.EndGroup();
        }

        private void Fog(int startTime, int endTime)
        {
            using (var pool = new OsbSpritePool(GetLayer("fog"), "sb/fog.png", OsbOrigin.Centre, (sprite, starttime, endtime) =>
            { }))
            {
                var duration = 10000;
                var amount = 4;
                var timeStep = duration / amount;
                for (var starttime = (double)startTime; starttime <= endTime - duration; starttime += timeStep)
                {
                    var endtime = starttime + duration;
                    var sprite = pool.Get(starttime, endtime);
                    var Y = Random(140, 500);

                    sprite.Move(starttime, endtime, -107, Y, 777, Y);
                    sprite.Scale(starttime, Random(1.2f, 2.5f));
                    var opacity = Random(0.2f, 0.5f);
                    sprite.Fade(starttime, starttime + 2000, 0, opacity);
                    sprite.Fade(endtime - 2000, endtime, opacity, 0);
                    sprite.Additive(starttime, endtime);

                }
            }
        }

        double tick(double time, double divisor)
        {
            return Beatmap.GetTimingPointAt((int)time).BeatDuration / divisor;
        }
    }
}