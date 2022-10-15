namespace Demkin.Transcoding.Domain.Events
{
    internal class TranscodeFileFailDomainEvent : IDomainEvent
    {
        public TranscodeFileFailDomainEvent(TranscodeFile transcodeFile)
        {
            TranscodeFile = transcodeFile;
        }

        public TranscodeFile TranscodeFile { get; }
    }
}