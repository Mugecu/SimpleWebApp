namespace SimpleWebApp.Domain.DTOs
{
    public class GoodsValuesDTO
    {
        public List<KeyValuePair<KeyValuePair<Guid, string>, uint>> Goods { get; set; }
    }
}
