namespace Demkin.Listen.WebApi.Admin.ViewModels
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string CoverUrl { get; set; }

        public int SequenceNumber { get; set; }

        public DateTime CreateTime { get; set; }
    }
}