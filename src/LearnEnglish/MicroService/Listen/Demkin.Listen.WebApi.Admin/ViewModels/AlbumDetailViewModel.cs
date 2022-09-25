namespace Demkin.Listen.WebApi.Admin.ViewModels
{
    public class AlbumDetailViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string CoverUrl { get; set; }

        public DateTime CreateTime { get; set; }
    }
}