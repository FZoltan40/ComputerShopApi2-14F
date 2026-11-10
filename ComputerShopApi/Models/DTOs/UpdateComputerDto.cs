namespace ComputerShopApi.Models.DTOs
{
    public class UpdateComputerDto
    {
        public string Brand { get; set; }
        public float Display { get; set; }
        public int Memory { get; set; }
        public Guid OsId { get; set; }
    }
}
