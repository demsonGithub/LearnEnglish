namespace Demkin.Transcoding.Domain.Events
{
    public class TranscodeFileStartDomainEvent : IDomainEvent
    {
        public TranscodeFileStartDomainEvent(TranscodeFile transcodeFile)
        {
            TranscodeFile = transcodeFile;
        }

        public TranscodeFile TranscodeFile { get; }
    }
}