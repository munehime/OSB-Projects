using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;

namespace StorybrewScripts
{
    public class BackgroundsManager : StoryboardObjectGenerator
    {
        double beatDuration;
        public override void Generate()
        {
            var path = "sb/bg/parousia/bw.jpg";
            var bitmap = GetMapsetBitmap(path);
            var sprite = GetLayer("").CreateSprite(path);

            sprite.Scale(0, 480f / bitmap.Height);
            sprite.Fade(0, 11982, 0, 0.4);
            sprite.Fade(11982, 0);

            path = "sb/bg/parousia/blur.jpg";
            sprite = GetLayer("").CreateSprite(path);

            sprite.Scale(11982, 480f / bitmap.Height);
            sprite.Fade(11982, 0.8);
            sprite.Fade(25316, 0);

            path = "sb/bg/parousia/default.jpg";
            sprite = GetLayer("").CreateSprite(path);

            sprite.Scale(25316, 480f / bitmap.Height);
            sprite.Fade(25316, 1);
            sprite.Fade(46627, 0);

            AddBackground("anima", 46627, 75579);
            AddBackground("akasha", 75579, 117092);
            AddBackground("happyend", 120984, 178259);
            AddBackground("halcyon", 178259, 217654);
            AddBackground("ascension", 217654, 256053);
            AddBackground("wish", 260854, 284854);
            AddBackground("bluezenith", 284854, 310053);
            AddBackground("overthetop", 319653, 351653);
            AddBackground("freedomdvie", 351653, 388743);
            AddBackground("gloriouscrown", 388743, 412743);
        }

        private void AddBackground(string name, double startTime, double endTime)
        {
            var path = $"sb/bg/{name}/default.jpg";
            var bitmap = GetMapsetBitmap(path);
            var sprite = GetLayer("").CreateSprite(path);
            var beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

            sprite.Scale(OsbEasing.OutExpo, startTime, startTime + beatDuration * 16, (480f / bitmap.Height) * 1.65f, 480f / bitmap.Height);
            sprite.Rotate(OsbEasing.OutExpo, startTime, startTime + beatDuration * 16, Math.PI / 8, 0);

            switch (name)
            {
                case "akasha":
                    sprite.Fade(startTime, 1);
                    sprite.Fade(endTime, endTime + beatDuration * 13, 1, 0);
                    break;

                case "happyend":
                    sprite.Fade(startTime, startTime + beatDuration * 16, 0, 1);
                    sprite.Fade(endTime, 0);
                    break;

                case "ascension":
                    sprite.Fade(startTime, 1);
                    sprite.Fade(endTime, endTime + beatDuration * 16, 1, 0);
                    break;

                case "bluezenith":
                    sprite.Fade(startTime, 1);
                    sprite.Fade(endTime, endTime + beatDuration * 20, 1, 0);
                    break;

                case "gloriouscrown":
                    sprite.Fade(startTime, 1);
                    sprite.Fade(endTime, endTime + beatDuration * 4, 1, 0);
                    break;

                default:
                    sprite.Fade(startTime, 1);
                    sprite.Fade(endTime, 0);
                    break;
            }
        }
    }
}
