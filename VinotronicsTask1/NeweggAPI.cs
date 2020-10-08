using Newegg.Marketplace.SDK;
using Newegg.Marketplace.SDK.Base;
using Newegg.Marketplace.SDK.Order;
using Newegg.Marketplace.SDK.Order.Model;
using System;

namespace VinotronicsTask1
{
    public class NeweggAPI
    {
        private OrderCall ordercall;

        public NeweggAPI()
        {
            //Construct an APIConfig with SellerID,  APIKey(Authorization) and SecretKey.
            //APIConfig config = APIConfig.FromJsonFile(@"setting.json");
            APIConfig config = new APIConfig("A006", "720ddc067f4d115bd544aff46bc75634", "21EC2020-3AEA-1069-A2DD-08002B30309D");

            //Create a APIClient with the config
            APIClient client = new APIClient(config);

            //Create the Api Call object with he client.
            ordercall = new OrderCall(client);
        }

        public void GetOrderInfo()
        {
            Console.WriteLine("GetOrderInfo");

            // Create Request
            var orderreq = new GetOrderInformationRequest(new GetOrderInformationRequestCriteria()
            {
                Status = OrderStatus.Unshipped,
                Type = OrderInfoType.All,
                OrderDateFrom = "2016-01-01 09:30:47",
                OrderDateTo = "2017-12-17 09:30:47",
                OrderDownloaded = 0
            });

            // Send your request and get response
            var response = ordercall.GetOrderInformation(null, orderreq).Result;

            // Get data from the response
            GetOrderInformationResponseBody info = response.GetResponseBody();

            // Use the data pre you business
            Console.WriteLine(string.Format("There are {0} order(s) in the result.", info.OrderInfoList.Count.ToString()));

        }
    }
}
