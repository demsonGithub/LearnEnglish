namespace Demkin.Listen.WebApi.Admin.Application.Models
{
    public class Sentence
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Content { get; set; }
    }
}