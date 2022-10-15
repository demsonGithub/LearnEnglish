using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demkin.Transcoding.Domain.Events
{
    public class TranscodeFileCompleteDomainEvent : IDomainEvent
    {
        public TranscodeFileCompleteDomainEvent(TranscodeFile transcodeFile)
        {
            TranscodeFile = transcodeFile;
        }

        public TranscodeFile TranscodeFile { get; }
    }
}