using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace EasywebshopProductFeedAdapter.Extensions
{
    public static class XmlExtensions
    {
        public static XmlNode GetChild(this XmlNode xmlNode, string name)
        {
            return xmlNode.ChildNodes?.Cast<XmlNode>().Where(n => n.Name == name).FirstOrDefault();
        }
    }
}
