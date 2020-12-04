using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;

namespace StorybrewScripts
{
    public class YouAreHereForever : StoryboardObjectGenerator
    {
        double beatDuration;
        Vector2 scale = new Vector2(0.35f, 80);
        int logScale = 200;
        float minimalHeight = 1;

        FontGenerator font;
        float fontScale = 0.45f;

        public override void Generate()
        {
            beatDuration = Beatmap.GetTimingPointAt(317854).BeatDuration;
            font = SetupFont();

            GenerateSpectrum(317854, 319653, 60);
            GenerateIntroTitle("welcome", 317854, 319653);
        }

        private void GenerateIntroTitle(string title, double startTime, double endTime)
        {
            var texture = font.GetTexture(title);

            var position = new Vector2(320 - texture.BaseWidth * fontScale * 0.5f, 240) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
            var sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, new Vector2(position.X, 240));

            sprite.ScaleVec(OsbEasing.OutSine, startTime, startTime + beatDuration, fontScale, 0, fontScale, fontScale);
            sprite.ScaleVec(startTime + beatDuration, endTime, fontScale, fontScale, fontScale * 1.15f, fontScale * 1.15f);
            sprite.Fade(startTime, startTime + beatDuration, 0, 1);
            sprite.Fade(endTime, 0);

        }

        private void GenerateSpectrum(double startTime, double endTime, int barCount)
        {
            var heightKeyframes = getKeyframes(startTime, endTime, barCount);
            var barWidth = 854f / barCount;
            for (var i = 0; i < barCount; i++)
            {
                var keyframes = heightKeyframes[i];
                keyframes.Simplify1dKeyframes(0.2, h => h);

                var angle = MathHelper.DegreesToRadians((360 * i / barCount));
                var position = new Vector2(
                        320 + 120 * (float)Math.Cos(angle),
                        240 + 120 * (float)Math.Sin(angle)
                        );

                var bar = GetLayer("").CreateSprite("sb/p.png", OsbOrigin.BottomCentre, position);
                bar.Color(startTime, "#3366FF");
                bar.Additive(startTime, endTime);
                bar.Rotate(startTime, angle + Math.PI * 0.5);
                bar.Fade(startTime, startTime + beatDuration * 3, 0, 1);
                bar.Fade(endTime, 0);


                var scaleX = scale.X * barWidth;
                scaleX = (float)Math.Floor(scaleX * 10) / 10.0f;

                var hasScale = false;
                keyframes.ForEachPair(
                    (start, end) =>
                    {
                        hasScale = true;
                        bar.ScaleVec(start.Time, end.Time,
                            scaleX, start.Value,
                            scaleX, end.Value);
                    },
                    minimalHeight,
                    s => (float)Math.Round(s, 1)
                );
                if (!hasScale) bar.ScaleVec(startTime, scaleX, minimalHeight);
            }
        }


        private FontGenerator SetupFont()
        {
            return LoadFont("sb/f", new FontDescription()
            {
                FontPath = "Raleway-Light.ttf",
                FontSize = 60,
                Color = Color4.White,
                Padding = new Vector2(20, 0),
            }, new FontGlow()
            {
                Radius = 50,
                Color = new Color4(51, 102, 255, 255),
            });
        }

        private KeyframedValue<float>[] getKeyframes(double startTime, double endTime, int barCount)
        {
            var heightKeyframes = new KeyframedValue<float>[barCount];
            for (var i = 0; i < barCount; i++)
                heightKeyframes[i] = new KeyframedValue<float>(null);

            var fftTimeStep = Beatmap.GetTimingPointAt((int)startTime).BeatDuration / 4;
            var fftOffset = fftTimeStep * 0.2;

            for (var time = (double)startTime; time < endTime; time += fftTimeStep)
            {
                var fft = GetFft(time + fftOffset, barCount, null, OsbEasing.InExpo);
                for (var i = 0; i < barCount; i++)
                {
                    var height = (float)Math.Log10(1 + fft[i] * logScale) * scale.Y;
                    if (height < minimalHeight) height = (float)Random(30, 100f);


                    heightKeyframes[i].Add(time, height);
                }
            }
            return heightKeyframes;
        }
    }
}
