using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Util
{
    //In their infinite wisdom, EasyWebshop has forgotten to add a link to the product in their feed.
    //This helper class attempts to form a valid URL from the product category and the product code.
    //Not how I'd prefer to do this, but I don't have many options so it's the best I've got.
    //If you've setup rewrite rules for your webshop, your best bet is modifying this class to accomodate for your custom path.
    public class EasyWebshopUrlFormatter
    {
        private readonly string _fqdn;

        public EasyWebshopUrlFormatter(string fqdn)
        {
            _fqdn = fqdn.TrimEnd(new char[] { '/' });
        }
        public string Format(string category, string productName)
        {
            var formattedCategory = category.Replace(" - ", "-").Replace("&", "");
            var formattedProductName = productName.Replace(" - ", "-").Replace("&", "and");
            return _fqdn + "/" + formattedCategory.Replace(' ', '-') + "/" + formattedProductName.Replace(' ', '-');
        }
    }
}
