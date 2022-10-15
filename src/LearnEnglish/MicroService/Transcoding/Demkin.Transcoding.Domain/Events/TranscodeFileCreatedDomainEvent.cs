using Demkin.Domain.Abstraction;

namespace Demkin.Transcoding.Domain.Events
{
    public class TranscodeFileCreatedDomainEvent : IDomainEvent
    {
        public TranscodeFileCreatedDomainEvent(TranscodeFile transcodeFile)
        {
            TranscodeFile = transcodeFile;
        }

        public TranscodeFile TranscodeFile { get; }
    }
}