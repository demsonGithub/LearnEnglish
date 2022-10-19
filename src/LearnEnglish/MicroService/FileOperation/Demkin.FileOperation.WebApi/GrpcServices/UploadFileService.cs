using Demkin.FileOperation.WebApi.Proto;
using Grpc.Core;

namespace Demkin.FileOperation.WebApi.GrpcServices
{
    public class UploadFileService : UploadFileGrpc.UploadFileGrpcBase
    {
        private readonly FileDomainService _domainService;

        public UploadFileService(FileDomainService domainService)
        {
            _domainService = domainService;
        }

        public override async Task<UploadFileResponseMsg> UploadFile(IAsyncStreamReader<UploadFileRequest> requestStream, ServerCallContext context)
        {
            try
            {
                var a = requestStream.ReadAllAsync();

                var tempData = new List<byte>();
                while (await requestStream.MoveNext())
                {
                    tempData.AddRange(requestStream.Current.Data);
                }

                Stream stream = new MemoryStream(tempData.ToArray());

                var result = await _domainService.UploadFileAsync("test", stream);

                return new UploadFileResponseMsg() { RemoteUrl = result.uploadFileInfo.RemoteUrl.ToString() };
            }
            catch (Exception)
            {
                return new UploadFileResponseMsg() { RemoteUrl = "" };
            }
        }
    }
}