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
using System.Drawing;
using System.Linq;

namespace StorybrewScripts
{
    public class BackgroundsManager : StoryboardObjectGenerator
    {
        private Bitmap Bitmap;
        private double BeatDuration;
        private double Offset;

        public override void Generate()
        {
            BeatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
            Offset = Beatmap.GetTimingPointAt(0).Offset;

            Bitmap = GetMapsetBitmap(Beatmap.BackgroundPath);

            ScaleInBackground();

            var sprite = GetLayer("").CreateSprite(Beatmap.BackgroundPath);
            sprite.Scale(OsbEasing.OutExpo, 20141, 20141 + BeatDuration * 8, (480f / Bitmap.Height) * 1.3, 480f / Bitmap.Height * 1.065f);
            sprite.Fade(20141, 0.5);
            sprite.Fade(118808, 0);
            FloatBackground(sprite, 20141, 118808);

            // Kiai
            sprite = GetLayer("").CreateSprite(Beatmap.BackgroundPath);
            sprite.Scale(OsbEasing.OutExpo, 118808, 118808 + BeatDuration * 8, (480f / Bitmap.Height) * 1.3, 480f / Bitmap.Height * 1.065f);
            sprite.Fade(118808, 0.8);
            sprite.Fade(150808, 154808, 0.8, 0);
            Parallax(sprite, 118808, 154808, 0.05f);

            sprite = GetLayer("").CreateSprite(Beatmap.BackgroundPath);
            sprite.Scale(161475, 480f / Bitmap.Height * 1.065f);
            sprite.Fade(161475, 0.5);
            sprite.Fade(225475, 0);
            FloatBackground(sprite, 161475, 225475);

            // Kiai 2
            sprite = GetLayer("").CreateSprite(Beatmap.BackgroundPath);
            sprite.Scale(OsbEasing.OutExpo, 225475, 225475 + BeatDuration * 8, (480f / Bitmap.Height) * 1.3, 480f / Bitmap.Height * 1.065f);
            sprite.Fade(225475, 0.8);
            sprite.Fade(257475, 262808, 0.8, 0);
            Parallax(sprite, 225475, 262808, 0.05f);
        }

        private void ScaleInBackground()
        {
            var sprite = GetLayer("scale in").CreateSprite(Beatmap.BackgroundPath);
            sprite.Fade(9475, 13475, 0, 0.8);
            sprite.Scale(OsbEasing.InQuad, 9475, 18808, 1.5, 1);
            sprite.Move(OsbEasing.InQuad, 9475, 18808, -690, 850, 100, 600);
            sprite.Scale(OsbEasing.OutCirc, 18808, 20141, 1, (480f / Bitmap.Height) * 1.3);
            sprite.Move(OsbEasing.OutCirc, 18808, 20141, 100, 600, 320, 240);
            sprite.Fade(20141, 0.8);
        }

        private void FloatBackground(OsbSprite sprite, double startTime, double endTime)
        {
            Vector2 moveDistance = new Vector2(Random(-3, 3f), Random(-3, 3f));
            Vector2 startPos = new Vector2(320, 240);
            Vector2 endPos = new Vector2(startPos.X + moveDistance.X, startPos.Y + moveDistance.Y);
            double startRadian = 0;
            double endRadian = startRadian + Random(-Math.PI / 120, Math.PI / 120);
            int a = 1;

            sprite.Scale(startTime, 480f / Bitmap.Height * 1.065f);

            for (double starttime = startTime; starttime < endTime; starttime += BeatDuration * 8)
            {
                sprite.Move(OsbEasing.InOutSine, starttime, starttime + BeatDuration * 8, startPos, endPos);
                sprite.Rotate(OsbEasing.InOutSine, starttime, starttime + BeatDuration * 8, startRadian, endRadian);

                startPos = endPos;
                endPos = startPos + new Vector2(Random(-3, 3f), Random(-3, 3f));
                startRadian = endRadian;
                endRadian = startRadian + Random(0, Math.PI / 320) * Math.Pow(-1, ++a);
            }
        }

        private void Parallax(OsbSprite sprite, double startTime, double endTime, float moveAmount)
        {
            sprite.Fade(startTime - 1, 0);
            OsuHitObject lastHitobject = null;
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if (hitobject.StartTime < startTime - 5 || endTime - 5 < hitobject.StartTime)
                {
                    lastHitobject = hitobject;
                    continue;
                }

                if (!IsDownbeat(hitobject.StartTime)) continue;

                var oldVec = lastHitobject.PositionAtTime(lastHitobject.StartTime);
                var oldPos = GetTrackedLocation(oldVec.X, oldVec.Y, moveAmount);
                var newVec = hitobject.PositionAtTime(hitobject.StartTime);
                var newPos = GetTrackedLocation(newVec.X, newVec.Y, moveAmount);
                sprite.Move(OsbEasing.InOutSine, lastHitobject.StartTime, hitobject.StartTime, oldPos, newPos);

                lastHitobject = hitobject;
            }
        }

        private bool IsDownbeat(double time)
        {
            var dist = (time - Offset - BeatDuration * 4) % (BeatDuration * 4);
            return dist < 5 || dist > BeatDuration * 4 - 5;
        }

        private Vector2 GetTrackedLocation(float x, float y, float moveAmount)
        {
            var newX = -(320 - x) * moveAmount + 320;
            var newY = -(240 - y) * moveAmount + 240;

            return new Vector2(newX, newY);
        }
    }
}
