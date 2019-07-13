// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace AkuBot.Bots
{
    public class Bot : ActivityHandler
    {
        private readonly string[] helloValues = new string[] { "hei", "moi", "hola", "moro", "tere", "terve" };
        private Hessu GetHessu(string name, string json)
        {
            return new Hessu(name, json);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var activity = turnContext.Activity;
            var lower = activity.RemoveRecipientMention().ToLower();

            if (lower.Contains("stat"))
            {
                await GetStats().ConfigureAwait(false);
            }
            else if (lower.Contains("tanaa") || lower.Contains("tänää") || lower.Contains("tänää"))
            {
                await GetStatsTanaan().ConfigureAwait(false);
            }
            else if (lower.ContainsAny(helloValues))
            {
                await KikkiHiiri().ConfigureAwait(false);
            }
            else if (!activity.Text.Contains("hups"))
            {
                await ShowOptions().ConfigureAwait(false);
            }
            else
            {
                await GoobyPls().ConfigureAwait(false);
            }

            async Task ShowOptions()
            {
                var message = MessageFactory.Text("Hessu mitä vittua?");
                //message.RemoveRecipientMention();
                message.TextFormat = TextFormatTypes.Plain;
                message.SuggestedActions = new SuggestedActions()
                {
                    Actions = new List<CardAction>() {
                    new CardAction(){ Title = "Näytä statsit", Type=ActionTypes.ImBack, Value="stats" },
                    new CardAction(){ Title = "Hups", Type=ActionTypes.ImBack, Value="hups" }
                    }
                };

                await turnContext.SendActivityAsync(message, cancellationToken);
            }

            async Task GetStatsTanaan()
            {
                var startDate = ((DateTimeOffset)DateTime.UtcNow.AddHours(-12)).ToUnixTimeSeconds();

                var j = await StatsApiClient.GetAsync($"player/76561198142674318/stats?startDate={startDate}").ConfigureAwait(false);
                var t = await StatsApiClient.GetAsync($"player/76561197963691340/stats?startDate={startDate}").ConfigureAwait(false);
                var m = await StatsApiClient.GetAsync($"player/76561197963257704/stats?startDate={startDate}").ConfigureAwait(false);
                var a = await StatsApiClient.GetAsync($"player/76561197960369776/stats?startDate={startDate}").ConfigureAwait(false);
                var e = await StatsApiClient.GetAsync($"player/76561197965878215/stats?startDate={startDate}").ConfigureAwait(false);
                var hessut = new Hessu[] {
                GetHessu("Juho", j), GetHessu("Topias", t), GetHessu("Mikko", m), GetHessu("Lompsa", a), GetHessu("Epeli", e) };

                const string header = "\t\n\t\n\t\nTänään    HLTV    RWS    KD    ADR\n";

                var textToDraw = $"{header}{string.Join(Environment.NewLine, hessut.OrderByDescending(f => f.Rating).Select(f => f.ToString()))}\n";
                var bytes = DrawText(textToDraw, true);

                var message = MessageFactory.Attachment(new Attachment("image/png", $"data:image/png;base64,{Convert.ToBase64String(bytes)}", "stats.png"));
                await turnContext.SendActivityAsync(message, cancellationToken);
            }

            async Task GetStats()
            {
                var startDate = ((DateTimeOffset)DateTime.UtcNow.AddDays(-28)).ToUnixTimeSeconds();

                var j = await StatsApiClient.GetAsync("player/76561198142674318/stats").ConfigureAwait(false);
                var t = await StatsApiClient.GetAsync("player/76561197963691340/stats").ConfigureAwait(false);
                var m = await StatsApiClient.GetAsync("player/76561197963257704/stats").ConfigureAwait(false);
                var a = await StatsApiClient.GetAsync("player/76561197960369776/stats").ConfigureAwait(false);
                var e = await StatsApiClient.GetAsync("player/76561197965878215/stats").ConfigureAwait(false);
                var hessut = new Hessu[] {
                GetHessu("Juho", j), GetHessu("Topias", t), GetHessu("Mikko", m), GetHessu("Lompsa", a), GetHessu("Epeli", e) };

                var j14 = await StatsApiClient.GetAsync($"player/76561198142674318/stats?startDate={startDate}").ConfigureAwait(false);
                var t14 = await StatsApiClient.GetAsync($"player/76561197963691340/stats?startDate={startDate}").ConfigureAwait(false);
                var m14 = await StatsApiClient.GetAsync($"player/76561197963257704/stats?startDate={startDate}").ConfigureAwait(false);
                var a14 = await StatsApiClient.GetAsync($"player/76561197960369776/stats?startDate={startDate}").ConfigureAwait(false);
                var e14 = await StatsApiClient.GetAsync($"player/76561197965878215/stats?startDate={startDate}").ConfigureAwait(false);
                var hessut14 = new Hessu[] {
                GetHessu("Juho", j14), GetHessu("Topias", t14), GetHessu("Mikko", m14), GetHessu("Lompsa", a14), GetHessu("Epeli", e14) };


                const string header = "\t\n\t\n\t\n4 weeks   HLTV    RWS    KD    ADR\n";

                var textToDraw = $"{header}{string.Join(Environment.NewLine, hessut14.OrderByDescending(f => f.Rating).Select(f => f.ToString()))}\nAll time\n{string.Join(Environment.NewLine, hessut.OrderByDescending(f => f.Rating).Select(f => f.ToString()))}";
                var bytes = DrawText(textToDraw);
                var message = MessageFactory.Attachment(new Attachment("image/png", $"data:image/png;base64,{Convert.ToBase64String(bytes)}", "stats.png"));

                await turnContext.SendActivityAsync(message, cancellationToken);
            }


            async Task KikkiHiiri()
            {
                var message = MessageFactory.Attachment(new Attachment("image/jpeg", $"https://steamuserimages-a.akamaihd.net/ugc/434950296045443533/FFBFC6C4B48577AAC1C3786BF3B25E32A4C94DD7/", "kikki-hiiri.jpeg"));
                await turnContext.SendActivityAsync(message, cancellationToken);
            }

            async Task GoobyPls()
            {
                var message = MessageFactory.Attachment(new Attachment("image/jpeg", $"https://tr.rbxcdn.com/13511aceec125e9863d43a4e94ceb3f8/420/420/Decal/Png", "gooby-pls.jpg"));
                await turnContext.SendActivityAsync(message, cancellationToken);
            }
        }

        public byte[] DrawText(string text, bool x2 = false)
        {
            using (var image = new MagickImage(MagickColors.White, 380, 400))
            {
                var x = new Drawables()
                  .FontPointSize(16)
                  .Font("Courier New")
                  .TextAntialias(true)
                  .StrokeColor(MagickColors.Black)
                  .StrokeWidth(0)
                  .FillColor(MagickColors.Black)
                  .TextAlignment(TextAlignment.Left)
                  .Text(10, 128, text)
                  .Draw(image);

                image.Trim();
                if (x2)
                {
                    image.Extent(new MagickGeometry(-10, -10, image.Width + 20, image.Height * 2 + 20), MagickColors.White);
                }
                else
                {
                    image.Extent(new MagickGeometry(-10, -10, image.Width + 20, image.Height + 20), MagickColors.White);
                }

                return image.ToByteArray(MagickFormat.Png);
            }
        }

    }
}
