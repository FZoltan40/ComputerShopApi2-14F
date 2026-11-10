namespace ComputerShopApi.Models.DTOs
{
    public class NewComputerDto
    {
        public string Brand { get; set; }
        public float Display { get; set; }
        public int Memory { get; set; }
        public Guid OsId { get; set; }
    }
}
