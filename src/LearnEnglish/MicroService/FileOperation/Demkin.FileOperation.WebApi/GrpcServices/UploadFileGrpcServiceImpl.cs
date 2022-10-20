using Demkin.Proto;
using Grpc.Core;

namespace Demkin.FileOperation.WebApi.GrpcServices
{
    public class UploadFileGrpcServiceImpl : UploadFileGrpc.UploadFileGrpcBase
    {
        private readonly FileDomainService _domainService;

        public UploadFileGrpcServiceImpl(FileDomainService domainService)
        {
            _domainService = domainService;
        }

        public override async Task<UploadFileReply> UploadFile(IAsyncStreamReader<UploadFileRequest> requestStream, ServerCallContext context)
        {
            try
            {
                var a = requestStream.ReadAllAsync();

                var idx = 0;
                string fileName = string.Empty;
                var acceptBytes = new List<byte>();

                while (await requestStream.MoveNext())
                {
                    if (idx == 0) { }
                    fileName = requestStream.Current.FileName;

                    acceptBytes.AddRange(requestStream.Current.Data);

                    idx++;
                }

                Stream stream = new MemoryStream(acceptBytes.ToArray());

                var result = await _domainService.UploadFileAsync(fileName, stream);

                return new UploadFileReply() { RemoteUrl = result.uploadFileInfo.RemoteUrl.ToString() };
            }
            catch (Exception)
            {
                return new UploadFileReply() { RemoteUrl = "" };
            }
        }
    }
}