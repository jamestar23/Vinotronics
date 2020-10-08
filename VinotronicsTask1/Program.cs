using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace VinotronicsTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            //NeweggAPI neweggAPI = new NeweggAPI();

            //try
            //{
            //    neweggAPI.GetOrderInfo();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exit with error.");
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine(ex.StackTrace);
            //}

            #region Client
            Console.WriteLine(string.Format("Newegg Marketplace API - Get Inventory request at:{0}", DateTime.Now.ToString()));
            Console.WriteLine("");
            Console.WriteLine("*********************************************************************");
            Console.WriteLine("");
            try
            {
                InventoryResult inventoryResult = null;
                //Determine the correct Newegg Marketplace API endpoint to use.
                // Please make sure your request URL is all in lower case
                string endpoint = @"https://api.newegg.com/marketplace/contentmgmt/item/inventory?sellerid={0}";
                endpoint = String.Format(endpoint, "A006");
                //Create an HttpWebRequest
                System.Net.HttpWebRequest request =
                    System.Net.WebRequest.Create(endpoint) as HttpWebRequest;
                //Remove proxy
                request.Proxy = null;
                //Specify the request method
                request.Method = "POST";
                //Specify the xml/Json request and response content types.
                request.ContentType = "application/xml";
                request.Accept = "application/xml";
                //Attach authorization information
                request.Headers.Add("Authorization", "720ddc067f4d115bd544aff46bc75634");
                request.Headers.Add("Secretkey", "21EC2020-3AEA-1069-A2DD-08002B30309D");
                //Construct the query criteria in the request body
                string requestBody = @"
                                  <ContentQueryCriteria>
                                  <Type>1</Type>
                                  <Value>A006ZX-35833</Value>
                                  </ContentQueryCriteria>";
                byte[] byteStr = Encoding.UTF8.GetBytes(requestBody);
                request.ContentLength = byteStr.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(byteStr, 0, byteStr.Length);
                }
                //Parse the response
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine(String.Format("Code:{0}.Error:{1}",
                            response.StatusCode.ToString(), response.StatusDescription));
                        return;
                    }
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        XmlSerializer serializer =
                            new XmlSerializer(typeof(InventoryResult));
                        inventoryResult =
                            serializer.Deserialize(responseStream) as InventoryResult;
                    }
                }
                string sellerID = inventoryResult.SellerID;
                string itemNumber = inventoryResult.ItemNumber;
                int availableQuantity = inventoryResult.AvailableQuantity;
                string message = String.Format("SellerID:{0} ItemNumber:{1} Availble Quantity:{2} \r\n Active:{3} SellerPartNumber:{4} ShipByNewegg:{5}",
                    inventoryResult.SellerID,
                    inventoryResult.ItemNumber,
                    inventoryResult.AvailableQuantity,
                    inventoryResult.Active,
                    inventoryResult.SellerPartNumber,
                    inventoryResult.ShipByNewegg);
                Console.WriteLine(message);
            }
            catch (WebException we)//Error Handling for Bad Request
            {
                if (((WebException)we).Status == WebExceptionStatus.ProtocolError)
                {
                    WebResponse errResp = ((WebException)we).Response;
                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        Console.WriteLine(String.Format("{0}", reader.ReadToEnd()));
                    }
                }
            }
            catch (Exception ex)//unhandle error
            {
                Console.WriteLine(string.Format("exception: at time:{0}", DateTime.Now.ToString()));
                Console.WriteLine(ex.Message + "---->");
                Console.WriteLine(ex.StackTrace.ToString());
            }
            Console.WriteLine("");
            Console.WriteLine("*********************************************************************");
            Console.WriteLine("");
            Console.WriteLine("Please input any key to exit……");
            Console.ReadLine();
            #endregion
        }
    }
}
