using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Transcoding.Domain.Interfaces
{
    public interface ITranscodeService : IDenpendencyScope
    {
        Task TranscodeFileToTarget(string sourceUrl, string targetUrl, CancellationToken ct = default);
    }
}