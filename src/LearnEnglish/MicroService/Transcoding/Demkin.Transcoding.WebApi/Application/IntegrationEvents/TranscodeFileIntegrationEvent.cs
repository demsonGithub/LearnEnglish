using Demkin.Transcoding.Domain;
using Demkin.Transcoding.Domain.Interfaces;
using Demkin.Transcoding.WebApi.Extensions;
using Demkin.Transcoding.WebApi.Models;
using DotNetCore.CAP;
using Newtonsoft.Json;

namespace Demkin.Transcoding.WebApi.IntegrationEvents
{
    public class TranscodeFileIntegrationEvent : ICapSubscribe
    {
        private readonly ITranscodeFileRepository _transcodeFileRepository;

        public TranscodeFileIntegrationEvent(ITranscodeFileRepository transcodeFileRepository)
        {
            _transcodeFileRepository = transcodeFileRepository;
        }

        /// <summary>
        /// 将收到的转码任务保存到数据库
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [CapSubscribe("Transcoding.Audio")]
        public async Task HandleTransodeFile(object parameters)
        {
            // 1. 将json字符转换为实体对象
            TranscodeFileInputParams? inputParams = JsonConvert.DeserializeObject<TranscodeFileInputParams>(Convert.ToString(parameters));

            // 2. 将任务添加到数据库
            string sourceUrl = "http://localhost:8083" + inputParams.FileSourceUrl;
            TranscodeFile entity = TranscodeFile.Create(inputParams.RedisKey, inputParams.FileTitle, sourceUrl, inputParams.OutputFormat);

            await _transcodeFileRepository.AddAsync(entity);
            await _transcodeFileRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}