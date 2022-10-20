namespace Demkin.FileOperation.WebApi.ViewModels
{
    public class UploadFileInfoDto
    {
        public long Id { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string RemoteUrl { get; set; }

        public DateTime CreateTime { get; set; }
    }
}