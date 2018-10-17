namespace Phc_Layout.Controllers
{
    public class HierarchicalViewModel
    {
        public int ID { get; set; }
        public int ParendID { get; set; }
        public bool HasChildren { get; set; }
        public string Name { get; set; }
        public object Expanded { get; internal set; }
        public object ImageUrl { get; internal set; }
    }
}