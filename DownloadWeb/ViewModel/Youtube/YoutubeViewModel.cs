using DownloadWeb.Utils;
using System.ComponentModel.DataAnnotations;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace DownloadWeb.ViewModel.Youtube {
    public class YoutubeViewModel {
        [RegularExpression(RegexTemplate.YoutubeUrl, ErrorMessage = "Link Loi roi")]
        [MaxLength(2048)]
        [Required]
        [Display(Name = "Link")]
        public string Link { get; set; }

        public string VideoCode { get; set; }
        public Video Metadata { get; set; }
        public IEnumerable<IVideoStreamInfo> StreamInfos { get; set; }


        public async Task SetSourceVideoInfo(Video video, IEnumerable<IVideoStreamInfo> videoStreamInfos) {
            Metadata = video;
            StreamInfos = videoStreamInfos;
            VideoCode = videoStreamInfos.FirstOrDefault()?.VideoCodec;
        }
    }
}
