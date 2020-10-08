using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VinotronicsTask1
{
    public class InventoryResult
    {
        [JsonProperty("SellerID")]
        public string SellerID { get; set; }

        [JsonProperty("ItemNumber")]
        public string ItemNumber { get; set; }

        [JsonProperty("AvailableQuantity")]
        public int AvailableQuantity { get; set; }

        [JsonProperty("Active")]
        public string Active { get; set; }

        [JsonProperty("SellerPartNumber")]
        public string SellerPartNumber { get; set; }

        [JsonProperty("ShipByNewegg")]
        public string ShipByNewegg { get; set; }
    }
}
