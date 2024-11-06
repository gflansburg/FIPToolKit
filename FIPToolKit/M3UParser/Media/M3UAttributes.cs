using System;
using System.Collections.Generic;
using System.Linq;

namespace M3U.Media
{
    public class M3UAttrubutes
    {
        internal M3UAttrubutes(IEnumerable<KeyValuePair<string, string>> attributes)
        {
            Attributes = attributes;
            TvgName = GetOrNull(nameof(TvgName));
            TvgLogo = GetOrNull(nameof(TvgLogo));
            TvgCountry = GetOrNull(nameof(TvgCountry));
            TvgLanguage = GetOrNull(nameof(TvgLanguage));
            TvgType = GetOrNull(nameof(TvgType));
            TvgGroup = GetOrNull(nameof(TvgGroup));
            TvgId = GetOrNull(nameof(TvgId));
            TvgUrl = GetOrNull(nameof(TvgUrl));
            TvgEpgid = GetOrNull(nameof(TvgEpgid));
            TvgEpgurl = GetOrNull(nameof(TvgEpgurl));
            TvgEpgshift = ConvertIntOrNull(GetOrNull(nameof(TvgEpgshift)));
            if (ConvertIntOrNull(GetOrNull(nameof(TvgTimeshift))).HasValue)
            {
                TvgTimeshift = TimeSpan.FromHours(Convert.ToDouble(ConvertIntOrNull(GetOrNull(nameof(TvgTimeshift))).Value));
            }
            if (ConvertIntOrNull(GetOrNull(nameof(TvgShift))).HasValue)
            {
                TvgShift = TimeSpan.FromHours(Convert.ToDouble(ConvertIntOrNull(GetOrNull(nameof(TvgShift))).Value));
            }
            TvgArchive = GetOrNull(nameof(TvgArchive));
            TvgTvgplaylist = GetOrNull(nameof(TvgTvgplaylist));
            TvgPlaylist = GetOrNull(nameof(TvgPlaylist));
            TvgPlaylistType = GetOrNull(nameof(TvgPlaylistType));
            TvgAspectRatio = GetOrNull(nameof(TvgAspectRatio));
            TvgAudioTrack = GetOrNull(nameof(TvgAudioTrack));
            TvgFramerate = GetOrNull(nameof(TvgFramerate));
            if (ConvertIntOrNull(GetOrNull(nameof(TvgDuration))).HasValue)
            {
                TvgDuration = TimeSpan.FromSeconds(Convert.ToDouble(ConvertIntOrNull(GetOrNull(nameof(TvgDuration))).Value));
            }
            TvgCopyright = GetOrNull(nameof(TvgCopyright));
            TvgMedia = GetOrNull(nameof(TvgMedia));
            TvgMediaSequence = ConvertIntOrNull(GetOrNull(nameof(TvgMediaSequence)));
            TvgResolution = GetOrNull(nameof(TvgResolution));
            if (ConvertIntOrNull(GetOrNull(nameof(TvgStart))).HasValue)
            {
                TvgStart = TimeSpan.FromSeconds(Convert.ToDouble(ConvertIntOrNull(GetOrNull(nameof(TvgStart))).Value));
            }
            TvgClosedCaptions = GetOrNull(nameof(TvgClosedCaptions));
            TvgClosedCaptionsLanguage = GetOrNull(nameof(TvgClosedCaptionsLanguage));
            TvgClosedCaptionsType = GetOrNull(nameof(TvgClosedCaptionsType));
            TvgContentType = GetOrNull(nameof(TvgContentType));
            if (ConvertIntOrNull(GetOrNull(nameof(TvgGap))).HasValue)
            {
                TvgGap = TimeSpan.FromSeconds(Convert.ToDouble(ConvertIntOrNull(GetOrNull(nameof(TvgGap))).Value));
            }
            if (ConvertIntOrNull(GetOrNull(nameof(TvgTargetduration))).HasValue)
            {
                TvgTargetduration = TimeSpan.FromSeconds(Convert.ToDouble(ConvertIntOrNull(GetOrNull(nameof(TvgTargetduration))).Value));
            }
            TvgIndependentSegments = GetOrNull(nameof(TvgIndependentSegments));
            TvgXByterange = GetOrNull(nameof(TvgXByterange));
            TvgXEndlist = GetOrNull(nameof(TvgXEndlist));
            TvgXKey = GetOrNull(nameof(TvgXKey));
            TvgXMediaSequence = ConvertIntOrNull(GetOrNull(nameof(TvgXMediaSequence)));
            TvgXProgramDateTime = GetOrNull(nameof(TvgXProgramDateTime));
            TvgXVersion = GetOrNull(nameof(TvgXVersion));
            TvgExtXEndlist = GetOrNull(nameof(TvgExtXEndlist));
            TvgExtXKey = GetOrNull(nameof(TvgExtXKey));
            TvgExtXMediaSequence = ConvertIntOrNull(GetOrNull(nameof(TvgExtXMediaSequence)));
            TvgExtXProgramDateTime = GetOrNull(nameof(TvgExtXProgramDateTime));
            TvgExtXVersion = GetOrNull(nameof(TvgExtXVersion));
            TvgExtXDiscontinuity = GetOrNull(nameof(TvgExtXDiscontinuity));

            AudioTrack = GetOrNull(nameof(AudioTrack));
            GroupTitle = GetOrNull(nameof(GroupTitle));
            M3UAutoLoad = ConvertIntOrNull(GetOrNull(nameof(M3UAutoLoad)));
            Cache = ConvertIntOrNull(GetOrNull(nameof(Cache)));
            Deinterlace = ConvertIntOrNull(GetOrNull(nameof(Deinterlace)));
            Refresh = ConvertIntOrNull(GetOrNull(nameof(Refresh)));
            ChannelNumber = ConvertIntOrNull(GetOrNull(nameof(ChannelNumber)));

            FipLatitude = ConvertDoubleOrNull(GetOrNull(nameof(FipLatitude)));
            FipLongitude = ConvertDoubleOrNull(GetOrNull(nameof (FipLongitude)));
        }

        string GetOrNull(string name)
        {
            return Attributes?.FirstOrDefault(w => w.Key?.ToLower() == name.ToLower()).Value;
        }

        int? ConvertIntOrNull(string value)
        {
            int num;
            if (int.TryParse(value, out num))
            {
                return num;
            }
            return null;
        }

        double? ConvertDoubleOrNull(string value)
        {
            double num;
            if (double.TryParse(value, out num))
            {
                return num;
            }
            return null;
        }

        private IEnumerable<KeyValuePair<string, string>> Attributes { get; set; } = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Specifies the version of the M3U8 playlist format being used. This tag is used to indicate the version of the playlist.
        /// </summary>
        public string TvgXVersion { get; private set; }

        /// <summary>
        /// Specifies the date and time of the current media file. This tag is used to synchronize the media file with the program guide.
        /// </summary>
        public string TvgXProgramDateTime { get; private set; }

        /// <summary>
        /// Specifies the sequence number for the media file. This tag is used to indicate the order of the media files in the playlist.
        /// </summary>
        public int? TvgXMediaSequence { get; private set; }

        /// <summary>
        /// Specifies the encryption key for the media file. This tag is used to encrypt the media file.
        /// </summary>
        public string TvgXKey { get; private set; }

        /// <summary>
        /// Specifies the end of the media file. This tag is used to indicate the end of the playlist.
        /// </summary>
        public string TvgXEndlist { get; private set; }

        /// <summary>
        /// Specifies the version of the M3U8 playlist format being used. This tag is used to indicate the version of the playlist.
        /// </summary>
        public string TvgExtXVersion { get; private set; }

        /// <summary>
        /// Specifies a discontinuity point in the media file. This tag is used to signal a break in the stream.
        /// </summary>
        public string TvgExtXDiscontinuity { get; private set; }

        /// <summary>
        /// Specifies the date and time of the current media file. This tag is used to synchronize the media file with the program guide.
        /// </summary>
        public string TvgExtXProgramDateTime { get; private set; }

        /// <summary>
        /// Specifies the sequence number for the media file. This tag is used to indicate the order of the media files in the playlist.
        /// </summary>
        public int? TvgExtXMediaSequence { get; private set; }

        /// <summary>
        /// Specifies the encryption key for the media file. This tag is used to encrypt the media file.
        /// </summary>
        public string TvgExtXKey { get; private set; }

        /// <summary>
        /// Specifies the end of the media file. This tag is used to indicate the end of the playlist.
        /// </summary>
        public string TvgExtXEndlist { get; private set; }

        /// <summary>
        /// Specifies the byte range of the current media file. This tag is used to specify a byte range within a media file.
        /// </summary>
        public string TvgXByterange { get; private set; }

        /// <summary>
        /// Specifies whether the media files are independent segments. This tag is used to indicate whether the media files can be played independently.
        /// </summary>
        public string TvgIndependentSegments { get; private set; }

        /// <summary>
        /// Specifies the maximum duration (in seconds) of the media files. This tag is used to set the maximum duration for the media files.
        /// </summary>
        public TimeSpan? TvgTargetduration { get; private set; }

        /// <summary>
        /// Specifies the type of playlist being used. This tag is used to indicate whether the playlist is dynamic or static.
        /// </summary>
        public string TvgPlaylistType { get; private set; }

        /// <summary>
        /// Specifies the time gap (in seconds) between the end of the previous media file and the start of the current media file. This tag is used to synchronize the media files.
        /// </summary>
        public TimeSpan? TvgGap { get; private set; }

        /// <summary>
        /// Specifies the content type for the current channel. This tag is used to indicate the type of content being broadcast (e.g. movie, TVshow, documentary).
        /// </summary>
        public string TvgContentType { get; private set; }

        /// <summary>
        /// Specifies the type of the closed captions for the current channel. This tag is used to set the type of the closed captions.
        /// </summary>
        public string TvgClosedCaptionsType { get; private set; }

        /// <summary>
        /// Specifies the language of the closed captions for the current channel. This tag is used to set the language of the closed captions.
        /// </summary>
        public string TvgClosedCaptionsLanguage { get; private set; }

        /// <summary>
        /// Specifies whether the current channel has closed captions. This tag is used to indicate whether the channel offers closed captions.
        /// </summary>
        public string TvgClosedCaptions { get; private set; }

        /// <summary>
        /// Specifies the start time (in seconds) for the current media file. This tag is used to set the start time for the media file.
        /// </summary>
        public TimeSpan? TvgStart { get; private set; }

        /// <summary>
        /// Specifies the resolution of the current media file. This tag is used to set the resolution for the media file.
        /// </summary>
        public string TvgResolution { get; private set; }

        /// <summary>
        /// Specifies the media type for the current media file. This tag is used to indicate the type of media being played (e.g. video, audio).
        /// </summary>
        public string TvgMedia { get; private set; }

        /// <summary>
        /// Specifies the sequence number for the media file. This tag is used to indicate the order of the media files in the playlist.
        /// </summary>
        public int? TvgMediaSequence { get; private set; }

        /// <summary>
        /// Specifies the duration of the current media file. This tag is used to set the duration of the media file.
        /// </summary>
        public TimeSpan? TvgDuration { get; private set; }

        /// <summary>
        /// Specifies the copyright information for the current channel. This tag is used to display the copyright information for the channel.
        /// </summary>
        public string TvgCopyright { get; private set; }

        /// <summary>
        /// Specifies the frame rate of the current media file.
        /// </summary>
        public string TvgFramerate { get; private set; }

        /// <summary>
        /// Specifies the time shift (in hours) of the current channel. This tag is used to adjust the channel's start time for time zone differences.
        /// </summary>
        public TimeSpan? TvgTimeshift { get; private set; }

        /// <summary>
        /// Specifies the time shift (in hours) of the current channel. This tag is used to adjust the channel's start time for time zone differences.
        /// </summary>
        public TimeSpan? TvgShift { get; private set; }

        /// <summary>
        /// Specifies the name of the current channel. This tag is used to display the name of the channel in the IPTV player.
        /// </summary>
        public string TvgName { get; private set; }

        /// <summary>
        /// Specifies the URL of the logo for the current channel. This tag is used to display the logo of the channel in the IPTV player.
        /// </summary>
        public string TvgLogo { get; private set; }

        /// <summary>
        /// Specifies the country code of the current channel. This tag is used to group channels by country in the playlist.
        /// </summary>
        public string TvgCountry { get; private set; }

        /// <summary>
        /// Specifies the language code of the current channel. This tag is used to group channels by language in the playlist.
        /// </summary>
        public string TvgLanguage { get; private set; }

        /// <summary>
        /// Specifies the audio track for the current channel. This tag is used to set the audio track for the channel.
        /// </summary>
        public string TvgAudioTrack { get; private set; }

        /// <summary>
        /// Specifies the aspect ratio of the current channel. This tag is used to set the aspect ratio for the channel.
        /// </summary>
        public string TvgAspectRatio { get; private set; }

        /// <summary>
        /// Specifies the unique identifier of the current media file. This tag is used to identify individual channels in the playlist.
        /// </summary>
        public string TvgId { get; private set; }

        /// <summary>
        /// Specifies the URL of the website for the current channel. This tag is used to provide additional information about the channel.
        /// </summary>
        public string TvgUrl { get; private set; }

        /// <summary>
        /// Specifies the type of the current channel. This tag is used to group channels by type (e.g. news, sports, entertainment) in the playlist.
        /// </summary>
        public string TvgType { get; private set; }

        /// <summary>
        /// Specifies the name of the group that the current channel belongs to. This tag is used to group channels in the playlist.
        /// </summary>
        public string TvgGroup { get; private set; }

        /// <summary>
        /// Specifies the unique identifier of the electronic program guide (EPG) for the current channel. This tag is used to associate the channel with its program guide.
        /// </summary>
        public string TvgEpgid { get; private set; }

        /// <summary>
        /// Specifies the URL of the electronic program guide (EPG) for the current channel. This tag is used to provide the location of the channel's program guide.
        /// </summary>
        public string TvgEpgurl { get; private set; }

        /// <summary>
        /// Specifies the time shift (in hours) of the electronic program guide (EPG) for the current channel. This tag is used to adjust the program guide for time zone differences.
        /// </summary>
        public int? TvgEpgshift { get; private set; }

        /// <summary>
        /// Specifies the URL of the TVG playlist for the current channel. This tag is used to provide additional playlist information.
        /// </summary>
        public string TvgTvgplaylist { get; private set; }

        /// <summary>
        /// Specifies the URL of the TVG playlist for the current channel. This tag is used to provide additional playlist information.
        /// </summary>
        public string TvgPlaylist { get; private set; }

        /// <summary>
        /// Specifies whether the current channel is a radio channel. This tag is used to differentiate between TV and radio channels in the playlist.
        /// </summary>
        public string TvgRadio { get; private set; }

        /// <summary>
        /// Specifies whether the current channel has an archive. This tag is used to indicate whether the channel offers archived content.
        /// </summary>
        public string TvgArchive { get; private set; }

        /// <summary>
        /// Specifies the audio track for the current channel. This tag is used to set the audio track for the channel.
        /// </summary>
        public string AudioTrack { get; private set; }

        /// <summary>
        /// Specifies the name of the group that the current channel belongs to. This tag is used to group channels in the playlist.
        /// </summary>
        public string GroupTitle { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int? M3UAutoLoad { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Cache { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Deinterlace { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Refresh { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ChannelNumber { get; private set; }

        /// <summary>
        /// The latitude of the radio station.
        /// </summary>
        public double? FipLatitude { get; private set; }

        /// <summary>
        /// The longitude of the radio station.
        /// </summary>
        public double? FipLongitude { get; private set; }
    }
}
