/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * 
 * License: MIT
 * 
 */

using System;
using System.Collections.Generic;

namespace SimpleM3U8Parser.Media
{
    public class M3u8MediaContainer
    {
        public
#if NET40
           List
#else
           IReadOnlyList
#endif
            <M3u8Media> Medias { get; internal set; } = new List<M3u8Media>();

        public TimeSpan Duration { get; internal set; }
    }
}
