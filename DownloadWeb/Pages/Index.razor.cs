using DownloadWeb.Shared.Component;
using DownloadWeb.Utils;
using DownloadWeb.ViewModel.Youtube;
using Microsoft.AspNetCore.Components;
using System;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace DownloadWeb.Pages {
    public partial class Index {

        [Inject]
        ILogger<YoutubeViewModel> Logger { get; set; }
        private YoutubeViewModel ViewModel { get; set; }

        private CustomFormValidator _customFormValidator;
        private YoutubeClient _youtube { get; set; }

        protected override async Task OnInitializedAsync() {
            ViewModel = new();
            _youtube = new();
        }

        private async Task HandleValidSubmit() {
            Logger.LogInformation("HandleValidSubmit called");
            _customFormValidator.ClearFormErrors();
            try {

            }
            catch (Exception ex) {
                Logger.LogError(ex.Message);
            }
        }


        private async Task OnBlurLink() {
            if (_customFormValidator.IsValid()) {

                // get video id
                var videoId = VideoId.Parse(ViewModel.Link ?? "");

                // get metadata video
                var video = await _youtube.Videos.GetAsync(videoId);

                var title = video.Title; // "Collections - Blender 2.80 Fundamentals"
                var author = video.Author.ChannelTitle; // "Blender"
                var duration = video.Duration; // 00:07:20

                var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoId);
                var streamInfo = streamManifest.GetVideoStreams();
                ViewModel.SetSourceVideoInfo(video, streamInfo);
                if (streamInfo is null) {
                    
                }
            }
        }
    }
}
