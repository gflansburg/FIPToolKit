﻿using FIPToolKit.Drawing;
using FIPToolKit.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace M3U.Media
{
    public class M3UAdornments
    {
        internal M3UAdornments(IEnumerable<string> adornments)
        {
            Adornments = adornments;
            string color = Adornments.FirstOrDefault(o => o.StartsWith("COLOR ", StringComparison.OrdinalIgnoreCase) || o.StartsWith("FORECOLOR ", StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(color))
            {
                try
                {
                    Color = System.Drawing.Color.FromName(color.Substring(color.IndexOf(' ') + 1).ToCamelCase());
                }
                catch(Exception)
                {
                }
            }
            string backColor = Adornments.FirstOrDefault(o => o.StartsWith("BACKCOLOR ", StringComparison.OrdinalIgnoreCase) || o.StartsWith("BACKGROUNDCOLOR ", StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(backColor))
            {
                try
                {
                    BackgroundColor = System.Drawing.Color.FromName(backColor.Substring(backColor.IndexOf(' ') + 1).ToCamelCase());
                }
                catch (Exception)
                {
                }
            }
            if (Adornments.Count(o => o.Equals("UNDERLINE", StringComparison.OrdinalIgnoreCase)) > 0)
            {
                Underline = true;
            }
            if (Adornments.Count(o => o.Equals("STRIKEOUT", StringComparison.OrdinalIgnoreCase)) > 0)
            {
                Strikeout = true;
            }
            if (Adornments.Count(o => o.Equals("BOLD", StringComparison.OrdinalIgnoreCase)) > 0)
            {
                Bold = true;
            }
            if (adornments.Count(o => o.Equals("ITALIC", StringComparison.OrdinalIgnoreCase) || o.Equals("EM", StringComparison.OrdinalIgnoreCase)) > 0)
            {
                Italic = true;
            }
        }

        private IEnumerable<string> Adornments { get; set; }

        public ColorEx? Color { get; private set; }
        public ColorEx? BackgroundColor { get; private set; }
        public bool? Bold { get; private set; }
        public bool? Italic { get; private set; }
        public bool? Underline { get; private set; }
        public bool? Strikeout { get; private set; }
        public FontStyle? FontStyle
        {
            get
            {
                if (Bold.HasValue || Italic.HasValue || Strikeout.HasValue || Underline.HasValue)
                {
                    System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
                    if (Bold.HasValue && Bold.Value)
                    {
                        style |= System.Drawing.FontStyle.Bold;
                    }
                    if (Italic.HasValue && Italic.Value)
                    {
                        style |= System.Drawing.FontStyle.Italic;
                    }
                    if (Strikeout.HasValue && Strikeout.Value)
                    {
                        style |= System.Drawing.FontStyle.Strikeout;
                    }
                    if (Underline.HasValue && Underline.Value)
                    {
                        style |= System.Drawing.FontStyle.Underline;
                    }
                    return style;
                }
                return null;

            }
        }
    }
}
