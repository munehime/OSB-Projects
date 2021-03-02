using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace StorybrewScripts
{
    public class BG : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            List<string> spritePaths = new List<string>(Directory.GetFiles(@"E:/osu!/Songs/973362 Various Artists - Patt!Memories/sb/bg/"));
            List<int> height = new List<int>();
            List<int> startTime = new List<int>() { 1260, 45388, 74281, 122765, 189894, 204228, 233089, 275880, 333564, 402725, 442041, 475447, 535447, 605560, 643987, 785635, 814942, 866465, 901346, 930504, 950728, 965728, 1014352, };
            List<OsbSprite> sprites = new List<OsbSprite>();

            foreach (var spritepath in spritePaths)
            {
                var spriteCreate = GetLayer("").CreateSprite(spritepath.Remove(0, 53), OsbOrigin.Centre);
                var bitmap = GetMapsetBitmap(spritepath.Remove(0, 53));
                sprites.Add(spriteCreate);
                height.Add(bitmap.Height);
            }

            #region oddloop

            sprites[0].Scale(startTime[0], 480.0 / (height[0] - 190));
            sprites[0].Fade(startTime[0], startTime[0] + 100, 0, 1);
            sprites[0].Rotate(startTime[0], startTime[0] + 100, MathHelper.DegreesToRadians(-10), 0);
            sprites[0].MoveY(startTime[0], startTime[1] - 100, 284, 200);
            sprites[0].MoveY(startTime[1] - 100, startTime[1] + 100, 200, 160);
            sprites[0].Fade(startTime[1] - 100, startTime[1] + 100, 1, 0);

            #endregion

            #region Vidro Moyou

            sprites[1].Scale(startTime[1], startTime[1] + 200, 480.0 / (height[1] - 600), 480.0 / (height[1] - 220));
            sprites[1].Fade(startTime[1], startTime[1] + 200, 0, 1);
            sprites[1].Scale(startTime[1] + 200, startTime[2] - 100, 480.0 / (height[1] - 220), 480.0 / (height[1] - 120));
            sprites[1].Scale(startTime[2] - 100, startTime[2] + 100, 480.0 / (height[1] - 120), 480.0 / (height[1] + 75));
            sprites[1].Rotate(startTime[2] - 100, startTime[2] + 100, 0, MathHelper.DegreesToRadians(3));
            sprites[1].Fade(startTime[2] - 100, startTime[2] + 100, 1, 0);

            #endregion

            #region Moonscraper

            sprites[2].Scale(startTime[2], startTime[3] + 100, 480.0 / (height[2] + 200), 480.0 / (height[2] - 60));
            sprites[2].Fade(startTime[2], startTime[2] + 200, 0, 1);
            sprites[2].Rotate(startTime[2], startTime[3] + 100, MathHelper.DegreesToRadians(15), MathHelper.DegreesToRadians(-6));
            sprites[2].MoveY(startTime[3] - 100, startTime[3] + 100, 240, 300);
            sprites[2].Fade(startTime[3] - 100, startTime[3] + 100, 1, 0);

            #endregion

            #region Sunshine

            sprites[3].Scale(startTime[3], startTime[4] + 100, 480.0 / (height[3] - 130), 480.0 / (height[3] - 183));
            sprites[3].Fade(startTime[3], startTime[3] + 200, 0, 1);
            sprites[3].MoveY(startTime[3], startTime[4] + 100, 242, 278);
            sprites[3].Rotate(startTime[3], startTime[4] + 100, MathHelper.DegreesToRadians(-3), MathHelper.DegreesToRadians(4));
            sprites[3].Fade(166482, 166747, 1, 0);
            sprites[3].Fade(167982, 1);
            sprites[3].MoveX(startTime[4] - 100, startTime[4] + 100, 360, 300);
            sprites[3].Fade(startTime[4] - 100, startTime[4] + 100, 1, 0);


            #endregion

            #region Acid Burst

            sprites[4].Scale(startTime[4], 480.0 / (height[4] - 100));
            sprites[4].Fade(startTime[4], startTime[4] + 100, 0, 1);
            sprites[4].MoveX(startTime[4], startTime[4] + 100, 410, 380);
            sprites[4].MoveX(startTime[4] + 100, startTime[5] - 50, 380, 260);
            sprites[4].MoveY(startTime[5] - 50, startTime[5] + 50, 240, 180);
            sprites[4].Fade(startTime[5] - 50, startTime[5] + 50, 1, 0);

            #endregion

            #region Necro Fantasia

            sprites[5].Scale(startTime[5], startTime[6] - 50, 480.0 / (height[5] - 130), 480.0 / (height[5] + 80));
            sprites[5].Rotate(startTime[5], startTime[5] + 200, MathHelper.DegreesToRadians(-30), MathHelper.DegreesToRadians(12));
            sprites[5].Fade(startTime[5], startTime[5] + 200, 0, 1);
            sprites[5].Rotate(startTime[5] + 200, startTime[6] - 50, MathHelper.DegreesToRadians(12), MathHelper.DegreesToRadians(20));
            sprites[5].Move(startTime[5], startTime[5] + 200, 650, 20, 360, 240);
            sprites[5].Fade(startTime[6] - 100, startTime[6] + 100, 1, 0);

            #endregion

            #region Bye Bye YESTERDAY

            var white = GetLayer("whiteLayer").CreateSprite("sb/pixel.png", OsbOrigin.Centre);
            white.ScaleVec(startTime[6], 854, 480);
            white.Fade(startTime[7] - 100, startTime[7] + 100, 1, 0);

            sprites[6].ScaleVec(startTime[6], startTime[6] + 100, 0.7, 0.95, 0.625, 0.625);
            sprites[6].Fade(startTime[6], startTime[6] + 100, 0, 1);
            sprites[6].MoveY(startTime[6], startTime[6] + 100, 300, 240);
            sprites[6].Rotate(startTime[6], startTime[7] + 100, MathHelper.DegreesToRadians(12), MathHelper.DegreesToRadians(-8));
            sprites[6].ScaleVec(startTime[6] + 100, startTime[7] + 100, 0.625, 0.625, 0.7, 0.7);
            sprites[6].Fade(startTime[7] - 100, startTime[7] + 100, 1, 0);

            #endregion

            #region Liblume

            sprites[7].Scale(startTime[7], startTime[8] + 100, 480.0 / (height[7] - 50), 480.0 / (height[7] - 150));
            sprites[7].MoveX(startTime[7], startTime[7] + 100, 250, 295);
            sprites[7].Fade(startTime[7], startTime[7] + 100, 0, 1);
            sprites[7].MoveX(startTime[7] + 100, startTime[8] - 100, 295, 390);
            sprites[7].MoveX(startTime[8] - 100, startTime[8] + 100, 390, 435);
            sprites[7].Fade(startTime[8] - 100, startTime[8] + 100, 1, 0);


            #endregion

            #region Dear Brave

            sprites[8].Scale(startTime[8], 480.0 / (height[8] - 100));
            sprites[8].Fade(startTime[8], startTime[8] + 200, 0, 1);
            sprites[8].MoveX(startTime[8], startTime[8] + 200, 400, 380);
            sprites[8].MoveX(startTime[8] + 200, startTime[9] - 100, 380, 260);
            sprites[8].MoveY(startTime[9] - 100, startTime[9] + 100, 240, 300);
            sprites[8].Fade(startTime[9] - 100, startTime[9] + 100, 1, 0);

            #endregion

            #region Synesthesia

            sprites[9].Scale(startTime[9], 480.0 / (height[9] - 114));
            sprites[9].MoveY(startTime[9], startTime[9] + 200, 320, 280);
            sprites[9].Fade(startTime[9], startTime[9] + 200, 0, 1);
            sprites[9].MoveY(startTime[9] + 200, startTime[10] - 100, 280, 200);
            sprites[9].MoveY(startTime[10] - 100, startTime[10] + 100, 200, 140);
            sprites[9].Fade(startTime[10] - 100, startTime[10] + 100, 1, 0);

            #endregion

            #region Caffeine Fighter

            sprites[10].Scale(startTime[10] - 100, startTime[11] + 100, 480.0 / (height[10] + 125), 480.0 / (height[10] + 100));
            sprites[10].Rotate(startTime[10] - 100, startTime[11] - 100, MathHelper.DegreesToRadians(-30), MathHelper.DegreesToRadians(30));
            sprites[10].Fade(startTime[10] - 100, startTime[10] + 100, 0, 1);
            sprites[10].Fade(startTime[11] - 100, startTime[11] + 100, 1, 0);
            sprites[10].MoveX(startTime[11] - 100, startTime[11] + 100, 320, 200);
            sprites[10].Rotate(startTime[11] - 100, startTime[11] + 100, MathHelper.DegreesToRadians(30), MathHelper.DegreesToRadians(55));

            #endregion

            #region Sakura no Zenya

            sprites[11].Scale(startTime[11] - 100, startTime[12] + 100, 480.0 / (height[11] - 130), 480.0 / (height[11] - 130));
            sprites[11].Fade(startTime[11] - 100, startTime[11] + 100, 0, 1);
            sprites[11].MoveX(startTime[11] - 100, startTime[11] + 100, 193, 233);
            sprites[11].MoveX(startTime[11] + 100, startTime[12] - 100, 233, 407);
            sprites[11].MoveX(startTime[12] - 100, startTime[12] + 100, 407, 447);
            sprites[11].Fade(startTime[12] - 100, startTime[12] + 100, 1, 0);

            #endregion

            #region Cycle Hit

            sprites[12].Scale(startTime[12] - 100, startTime[13] + 100, 480.0 / (height[12] - 270), 480.0 / (height[12] - 270));
            sprites[12].MoveY(startTime[12] - 100, startTime[12] + 100, 350, 320);
            sprites[12].MoveY(startTime[12] + 100, startTime[13] - 100, 320, 160);
            sprites[12].MoveY(startTime[13] - 100, startTime[13] + 100, 160, 130);
            sprites[12].Fade(startTime[12] - 100, startTime[12] + 100, 0, 1);
            sprites[12].Fade(startTime[13] - 100, startTime[13] + 100, 1, 0);

            #endregion

            #region Ascension to Heaven

            sprites[13].Scale(startTime[13] - 100, startTime[14] + 100, 480.0 / (height[13] - 130), 480.0 / (height[13] - 130));
            sprites[13].MoveX(startTime[13] - 100, startTime[13] + 100, 195, 235);
            sprites[13].MoveX(startTime[13] + 100, startTime[14] - 100, 235, 405);
            sprites[13].MoveX(startTime[14] - 100, startTime[14] + 100, 405, 445);
            sprites[13].Fade(startTime[13] - 100, startTime[13] + 100, 0, 1);
            sprites[13].Fade(startTime[14] - 50, startTime[14] + 50, 1, 0);

            #endregion

            #region SANTA SAN

            sprites[14].Scale(startTime[14] - 50, 480.0 / (height[14] - 138));
            sprites[14].Move(startTime[14] - 50, startTime[14], 320, 160, 320, 190);
            sprites[14].Move(startTime[14], 697048, 320, 190, 320, 290);
            sprites[14].Move(697049, 320, 240);
            sprites[14].Scale(697049, 480.0 / (height[14] - 38));
            sprites[14].Fade(startTime[14] - 50, startTime[14] + 50, 0, 1);
            sprites[14].Fade(762589, 762639, 1, 0);

            var lastHitObject = Beatmap.HitObjects.First();
            var oldRad = .0;
            var newRad = .0;

            foreach (var hitObject in Beatmap.HitObjects)
            {
                if (hitObject.StartTime < 697207 - 5 || 762639 - 5 <= hitObject.StartTime)
                {
                    lastHitObject = hitObject;
                    continue;
                }
                var oldVec = lastHitObject.PositionAtTime(lastHitObject.EndTime);
                var oldPos = GetTrackedLocation(oldVec.X, oldVec.Y);
                var newVec = hitObject.PositionAtTime(hitObject.StartTime);
                var newPos = GetTrackedLocation(newVec.X, newVec.Y);

                newRad = MathHelper.DegreesToRadians(Random(-2.0, 2.0));

                sprites[14].Move(lastHitObject.EndTime, hitObject.StartTime, oldPos.X, oldPos.Y, newPos.X, newPos.Y);
                sprites[14].Rotate(lastHitObject.EndTime, hitObject.StartTime, oldRad, newRad);

                lastHitObject = hitObject;
                oldRad = newRad;

                if (hitObject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / 4;
                    var starttime = hitObject.StartTime;
                    while (true)
                    {
                        var endTime = starttime + timestep;

                        var complete = hitObject.EndTime - endTime < 5;
                        if (complete) endTime = hitObject.EndTime;

                        oldVec = hitObject.PositionAtTime(starttime);
                        oldPos = GetTrackedLocation(oldVec.X, oldVec.Y);
                        newVec = hitObject.PositionAtTime(endTime);
                        newPos = GetTrackedLocation(newVec.X, newVec.Y);

                        sprites[14].Move(starttime + 1, endTime, oldPos.X, oldPos.Y, newPos.X, newPos.Y);

                        if (complete) break;
                        starttime += timestep;
                    }
                }
            }

            #endregion

            #region Reality Check Through The Skull

            sprites[15].Scale(startTime[15], startTime[16] + 100, 480.0 / (height[16] - 0), 480.0 / (height[16] - 0));
            sprites[15].Fade(startTime[15], 1);
            sprites[15].Fade(813342, 813442, 1, 0);

            sprites[16].Scale(startTime[15], startTime[15] + 1000, 480.0 / height[15], 480.0 / (height[15] - 125));
            sprites[16].Fade(startTime[15], startTime[15] + 1000, 1, 0);

            #endregion

            #region Miracle 5ympho X

            sprites[17].Scale(816041, 816141, 480.0 / (height[17] - 150), 480.0 / (height[17] - 75));
            sprites[17].Fade(816041, 816141, 0, 1);
            sprites[17].Scale(816141, 864399, 480.0 / (height[17] - 75), 480.0 / (height[17] - 0));
            sprites[17].Fade(864399, 1);

            #endregion

            #region Responsibility Response

            sprites[18].Scale(866459, startTime[18] + 100, 480.0 / (height[18] - 90), 480.0 / (height[18] - 90));
            sprites[18].Fade(866459, 1);
            sprites[18].MoveX(866459, 899436, 360, 280);
            sprites[18].MoveX(899436, 899536, 280, 240);
            sprites[18].Fade(899436, 899536, 1, 0);

            #endregion

            #region Can't Defeat Airman

            sprites[19].Scale(901697, 480.0 / (height[19] - 193));
            sprites[19].MoveY(901697, 901797, 350, 310);
            sprites[19].MoveY(901797, 930197, 310, 170);
            sprites[19].MoveY(930197, 930297, 170, 130);
            sprites[19].Fade(901697, 901797, 0, 1);
            sprites[19].Fade(930197, 930297, 1, 0);

            #endregion

            #region Mikazuki

            sprites[20].Scale(930470, 480.0 / (height[20] - 193));
            sprites[20].MoveY(930470, 930570, 160, 200);
            sprites[20].MoveY(930570, 949518, 200, 280);
            sprites[20].MoveY(949518, 949618, 280, 320);
            sprites[20].Fade(930470, 930570, 0, 1);
            sprites[20].Fade(949518, 949618, 1, 0);

            #endregion

            #region Monochrome Butterfly

            sprites[21].Scale(950697, 965306, 480.0 / (height[21] - 67), 480.0 / (height[21] - 120));
            sprites[21].Scale(965306, 965406, 480.0 / (height[21] - 120), 480.0 / (height[21] - 230));
            sprites[21].Rotate(950697, 965406, MathHelper.DegreesToRadians(9), MathHelper.DegreesToRadians(-9));
            sprites[21].Fade(950697, 1);
            sprites[21].Fade(965306, 965406, 1, 0);

            #endregion

            #region Sukisuki Zecchoushou

            sprites[22].ScaleVec(OsbEasing.OutExpo, 965562, 965762, 0.12, 0.446, 0.446, 0.446);
            sprites[22].ScaleVec(OsbEasing.In, 965662, 1014280, 0.446, 0.446, 0.452, 0.452);
            sprites[22].Fade(965562, 965662, 0, 1);

            sprites[22].Fade(972357, 972613, 0.7, 0.2);
            sprites[22].Fade(972613, 972870, 0.7, 0.2);
            sprites[22].Fade(972870, 973511, 0.7, 0.2);
            sprites[22].Fade(973511, 1);

            sprites[22].Fade(980562, 980818, 0.7, 0.2);
            sprites[22].Fade(980818, 981075, 0.7, 0.2);
            sprites[22].Fade(981075, 981716, 0.7, 0.2);
            sprites[22].Fade(981716, 1);

            sprites[22].Fade(1013511, 1013895, 0.7, 0.2);
            sprites[22].Fade(1013895, 1014280, 0.7, 0.2);
            sprites[22].Fade(1014280, 0);

            var sprite = GetLayer("").CreateSprite("sb/bg/w.jpg", OsbOrigin.Centre);
            sprite.ScaleVec(973511, 974511, 0.446, 0.446, 0.51, 0.51);
            sprite.Fade(973511, 974511, 1, 0);
            sprite.ScaleVec(981716, 982716, 0.446, 0.446, 0.51, 0.51);
            sprite.Fade(981716, 982716, 1, 0);



            #endregion

            #region FREEDOM DiVE

            sprites[23].ScaleVec(startTime[22] - 100, 1, 0.95);
            sprites[23].ScaleVec(1030318, 1030443, 1, 0.95, 1.335, 1.335);
            sprites[23].Rotate(startTime[22] - 100, 1030318, MathHelper.DegreesToRadians(-3), MathHelper.DegreesToRadians(-15));
            sprites[23].Rotate(1030318, 1030443, MathHelper.DegreesToRadians(-15), 0);
            sprites[23].Fade(startTime[22] - 100, startTime[22] + 100, 0, 1);
            sprites[23].Fade(1030443, 0.85);
            sprites[23].Fade(1066443, 0);

            #endregion
        }

        public Vector2 GetTrackedLocation(float x, float y)
        {
            var MoveAmount = 0.01f;
            var midX = 320f;
            var midY = 240f;

            var newX = -(midX - x) * MoveAmount + midX;
            var newY = -(midY - y) * MoveAmount + midY;

            return new Vector2(newX, newY);
        }
    }
}
