using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace StorybrewScripts
{
    public class Text
    {
        [JsonProperty(Required = Required.Always)]
        public string sentence { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string color { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double startTime { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double endTime { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double startFadeTime { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double endFadeTime { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double X { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double Y { get; set; }

        [JsonProperty(Required = Required.Always)]
        public float size { get; set; }

        [JsonProperty(Required = Required.Always)]
        public double opacity { get; set; }
    }

    public class PlayersAndSongs : StoryboardObjectGenerator
    {
        [Configurable]
        public bool Reload = true;

        [Configurable]
        public bool Debug = true;

        StoryboardLayer creditsLayer, playersLayer, songsLayer, lyricsLayer;
        FontGenerator whiteFont, blackFont, creditFont;

        public override void Generate()
        {
            creditsLayer = GetLayer("credits");
            playersLayer = GetLayer("players");
            songsLayer = GetLayer("songs");
            lyricsLayer = GetLayer("lyrics");

            setupFonts();
            setupText();
        }

        void setupText()
        {

            List<Text> Players = JsonConvert.DeserializeObject<List<Text>>(File.ReadAllText($"{ProjectPath}/json/players.json"));
            List<Text> Songs = JsonConvert.DeserializeObject<List<Text>>(File.ReadAllText($"{ProjectPath}/json/songs.json"));
            List<Text> Lyrics = JsonConvert.DeserializeObject<List<Text>>(File.ReadAllText($"{ProjectPath}/json/lyrics.json"));
            List<Text> Credits = JsonConvert.DeserializeObject<List<Text>>(File.ReadAllText($"{ProjectPath}/json/credits.json"));
            List<float> centerX = new List<float>();

            foreach (var song in Songs)
                generateText(song, whiteFont, "songs", centerX, 0);
            

            int i = 0;
            while (i < Players.Count)
            {
                generateText(Players[i], whiteFont, "players", centerX, i);
                i++;
            }
            Log($"{centerX.Count}");
            foreach (var lyric in Lyrics)
            {
                if (lyric.color == "black") generateText(lyric, blackFont, "lyrics", centerX, 0);
                else generateText(lyric, whiteFont, "lyrics", centerX, 0);
            }

            foreach (var credit in Credits)
                generateText(credit, creditFont, "credits", centerX, 0);
        }

        void generateText(Text text, FontGenerator font, string type, List<float> list, int i)
        {
            var layer = creditsLayer;
            var letterX = (float)text.X;
            var letterY = (float)text.Y;
            var lineHeight = 0f;
            var lineWidth = 0f;

            foreach (var letter in text.sentence)
            {
                var fontTex = font.GetTexture(letter.ToString());
                lineWidth += fontTex.BaseWidth * text.size;
                lineHeight = Math.Max(lineHeight, fontTex.BaseHeight);
            }

            if (type == "credits" || type == "lyrics") letterX = 320 - lineWidth * 0.5f;
            if (type == "songs") list.Add(letterX + (lineWidth / 2));
            if (type == "players")
            {
                if (i < 15)
                    letterX = list[i] - lineWidth * 0.5f;
                else
                    letterX = list[i - 1] - lineWidth * 0.5f;
            }

            var starttime = text.startTime;
            var endtime = text.endTime;
            var timestep = 85;
            var moveY = 0;
            switch (type)
            {
                case "players":
                    {
                        layer = playersLayer;
                        moveY = 15;
                        break;
                    }
                case "songs":
                    {
                        layer = songsLayer;
                        moveY = -15;
                        break;
                    }
                case "lyrics":
                    {
                        layer = lyricsLayer;
                        timestep = 0;
                        break;
                    }
                case "credits":
                    {
                        layer = creditsLayer;
                        timestep = 125;
                        break;
                    }
            }

            foreach (var letter in text.sentence)
            {
                var fontTex = font.GetTexture(letter.ToString());
                if (!fontTex.IsEmpty)
                {
                    var position = new Vector2(letterX, letterY) + fontTex.OffsetFor(OsbOrigin.Centre) * text.size;
                    var sprite = layer.CreateSprite(fontTex.Path, OsbOrigin.Centre, position);

                    sprite.Scale(text.startTime, text.size);
                    sprite.Fade(starttime, starttime + text.startFadeTime, 0, text.opacity);
                    if (type == "credits") sprite.Fade(text.endTime, 0);
                    else sprite.Fade(endtime - text.endFadeTime, endtime, text.opacity, 0);

                    if (type == "lyrics")
                    {
                        sprite.MoveY(text.startTime, text.startTime + text.startFadeTime, position.Y + 25, position.Y + 10);
                        sprite.MoveY(text.startTime + text.startFadeTime, text.endTime - text.endFadeTime, position.Y + 10, position.Y - 10);
                        sprite.MoveY(text.endTime - text.endFadeTime, text.endTime, position.Y - 10, position.Y - 25);
                    }
                    else
                    {
                        sprite.MoveY(OsbEasing.OutBack, starttime, starttime + text.startFadeTime, position.Y + moveY, position.Y);
                        sprite.MoveY(OsbEasing.InBack, endtime - text.endFadeTime, endtime, position.Y, position.Y + moveY);
                    }
                }

                if (type != "lyrics" || type != "credits")
                {
                    starttime += timestep;
                    endtime -= timestep;
                }
                else if (type == "credits")
                {
                    starttime += timestep;
                }
                letterX += fontTex.BaseWidth * text.size;
            }
            letterY += lineHeight;

            if (type == "songs")
            {
                var shadowLine = GetLayer("shadowLine").CreateSprite("sb/pixel.png", OsbOrigin.CentreLeft, new Vector2(-108, (float)401.25));
                var whiteLine = GetLayer("whiteLine").CreateSprite("sb/pixel.png", OsbOrigin.CentreLeft, new Vector2(-108, 400));

                var lineSizeX = lineWidth + (Math.Abs(text.X) - 10);
                var startTimeScale = lineSizeX + timestep + text.startFadeTime;

                shadowLine.ScaleVec(text.startTime, text.startTime + startTimeScale, 0, 3.5, lineSizeX + 1.25, 3.5);
                shadowLine.ScaleVec(text.endTime - (lineSizeX - 4.25) - text.endFadeTime, text.endTime + 10, lineSizeX + 1.25, 3.5, 0, 3.5);
                shadowLine.Color(text.startTime, 0, 0, 0);
                shadowLine.Fade(text.startTime, 0.6);

                whiteLine.ScaleVec(text.startTime, text.startTime + startTimeScale + 1.25, 0, 3.5, lineSizeX, 3.5);
                whiteLine.ScaleVec(text.endTime - lineSizeX - text.endFadeTime, text.endTime + 11.25, lineSizeX, 3.5, 0, 3.5);
                whiteLine.Fade(text.startTime, text.opacity);
            }
        }

        void setupFonts()
        {
            whiteFont = LoadFont("sb/f/w", new FontDescription()
            {
                FontPath = "fonts/SVN-Product Sans Bold.ttf",
                Color = Color4.White,
                Padding = new Vector2(5, 5)
            },
            new FontGlow()
            {
                Radius = 1,
                Color = Color4.White
            },
            new FontShadow()
            {
                Thickness = 5,
                Color = new Color4(0, 0, 0, 100),
            });

            blackFont = LoadFont("sb/f/b", new FontDescription()
            {
                FontPath = "fonts/SVN-Product Sans Bold.ttf",
                Color = Color4.Black,
                Padding = new Vector2(5, 5)
            },
            new FontShadow()
            {
                Thickness = 2,
                Color = new Color4(0, 0, 0, 100),
            });

            creditFont = LoadFont("sb/f/c", new FontDescription()
            {
                FontPath = "fonts/DTM-Sans.otf",
                Color = Color4.White,
                Padding = new Vector2(5, 5)
            },
            new FontGlow()
            {
                Radius = 1,
                Color = Color4.White
            },
            new FontShadow()
            {
                Thickness = 5,
                Color = new Color4(0, 0, 0, 100),
            });
        }
    }
}
