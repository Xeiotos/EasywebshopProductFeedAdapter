using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace EasywebshopProductFeedAdapter.Tests
{
    public class PathHelper
    {
        public static string GetBasePath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
    }
}
