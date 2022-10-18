using Demkin.Listen.WebApi.Admin.Application.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Demkin.Listen.WebApi.Admin.Application.Commands
{
    public class AnalysisSubtitlesCommand : IRequest<string>
    {
        public IFormFile File { get; set; }
    }

    public class AnalysisSubtitlesCommandHandler : IRequestHandler<AnalysisSubtitlesCommand, string>
    {
        public AnalysisSubtitlesCommandHandler()
        {
        }

        public async Task<string> Handle(AnalysisSubtitlesCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;

            //var result = new StringBuilder();

            List<Sentence> result = new List<Sentence>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    string tempString = await reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(tempString) || Regex.IsMatch(tempString, @"^[+-]?\d*$"))
                    {
                        continue;
                    }

                    if (tempString.Contains("-->"))
                    {
                        string[] strArr = tempString.Split("-->");

                        TimeSpan startTime = ConvertToTs(strArr[0]);
                        TimeSpan endTime = ConvertToTs(strArr[1]);
                        string content = await reader.ReadLineAsync();

                        Sentence item = new Sentence
                        {
                            StartTime = startTime,
                            EndTime = endTime,
                            Content = content
                        };
                        result.Add(item);
                    }
                }
            }
            var resultToJson = JsonConvert.SerializeObject(result);

            return resultToJson;
        }

        private TimeSpan ConvertToTs(string strValue)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("hr-HR");

            TimeSpan ts = TimeSpan.Parse(strValue.Trim());

            return ts;
        }
    }
}