# EasywebshopProductFeedAdapter
Web service to adapt an Easywebshop product list to a Google Merchant Center Feed, with REST API to retrieve the feed file.

## Configuration
Configuration of the API is done via environment variables.

| Environment variable | Explanation |
| ------ | ------ |
| EWPFA_Username | The username (email) associated with your EasyWebshop API account |
| EWPFA_Password | The password of your EasyWebshop API account |

Non-sensitive information is configured via appdata.json

| Field | Explanation |
| ------ | ------ |
| Brand | The brand under which you publish your products. See caveats. |

## Caveats
A great deal of information is pulled from the EasyWebshop feed. However, EasyWebshop does not send brand information in it's feed. (AFAIK it doesn't even support brands in it's webshop software). So you will have to provide your own brand in the configuration, which will be applied globally (to all products).
