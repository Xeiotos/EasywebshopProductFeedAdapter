﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this String str)
        {
            var strBytes = System.Text.Encoding.UTF8.GetBytes(str);
            return System.Convert.ToBase64String(strBytes);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
