using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phc_Layout.Models
{
    public class DivisionsLocationOption
    {
        public string parkerDivId { get; set; }
        public string securityLocCD { get; set; }
        public string parkerDivName { get; set; }
        public string parkerLocName { get; set; }
        public string primary { get; set; }
        public string masterId { get; set; }
    }

    public class DivisionLoctionModel
    {
        public String WebId { get; set; }
        public String MasterId { get; set; }
    }

    public class Item
    {
        public string text { get; set; }
        //public string value { get; set; }
    }

    public class RootObject
    {
        public string text { get; set; }
        public bool expanded { get; set; }
        public List<Item> items { get; set; }
    }
}
