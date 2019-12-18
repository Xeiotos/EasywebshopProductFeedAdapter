# EasywebshopProductFeedAdapter
Web service to adapt an Easywebshop product list to a Google Merchant Center Feed, with REST API to retrieve the feed file.

## Configuration
Configuration of the API is done via environment variables.

| Environment variable | Explanation |
| ------ | ------ |
| EWPFA_Username | The username (email) associated with your EasyWebshop API account |
| EWPFA_Password | The password of your EasyWebshop API account |

Non-sensitive information is configured via appdata.json

| Field | Explanation | Format | Default
| ------ | ------ | ------ | ------ |
| Easywebshop:API_URL | The easywebshop api url. | FQDN + Path | https://www.easywebshop.com/api/ |
| Easywebshop:Url | The fully qualified domain name of your webshop | FQDN | N/A |
| Easywebshop:Brand | The brand under which you publish your products. See caveats. | String | N/A |
| Easywebshop:CountryCode | The country of which your webshop uses the currency. | ISO 3166-1 Alpha-2 | BE |

Shipping locations can be added as a list to appdata.json
| Field | Explanation | Format | Default
| ------ | ------ | ------ | ------ |
| Easywebshop:Shipping:Country | The country to which you ship. | FQDN + Path | BE |
| Easywebshop:Shipping:Service | Shipping service | String | Standard |
| Easywebshop:Shipping:Price | The price for shipping with this service to this location | Price (x.xx) + ISO 4217 currency | N/A |



## Caveats
A great deal of information is pulled from the EasyWebshop feed. However, EasyWebshop does not send brand information in it's feed. (AFAIK it doesn't even support brands in it's webshop software). So you will have to provide your own brand in the configuration, which will be applied globally (to all products).
