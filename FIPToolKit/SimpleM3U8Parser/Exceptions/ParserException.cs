/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * 
 * License: MIT
 * 
 */

using System;

namespace SimpleM3U8Parser.Exceptions
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message) { }
    }
}
