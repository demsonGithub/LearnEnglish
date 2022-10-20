namespace Demkin.Transcoding.Domain.Events
{
    public class TranscodeFileFailDomainEvent : IDomainEvent
    {
        public TranscodeFileFailDomainEvent(TranscodeFile transcodeFile)
        {
            TranscodeFile = transcodeFile;
        }

        public TranscodeFile TranscodeFile { get; }
    }
}