namespace Demkin.FileOperation.WebApi.ViewModels.UploadFile
{
    public class UploadFileInfoViewModel
    {
        public long Id { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public Uri RemoteUrl { get; set; }

        public DateTime CreateTime { get; set; }
    }
}