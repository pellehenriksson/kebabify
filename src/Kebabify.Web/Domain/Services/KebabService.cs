using System.ComponentModel.DataAnnotations;

using Kebabify.Web.Common;

namespace Kebabify.Domain.Services
{
    public class KebabService
    {
        private readonly SystemClock clock;

        private readonly ILogger<KebabService> logger;

        public KebabService(SystemClock clock, ILogger<KebabService> logger)
        {
            this.clock = clock;
            this.logger = logger;
        }

        protected KebabService()
        {
            // this is just for making mocking possible
        }

        public virtual Result Make(Parameters parameters)
        {
            if (parameters == null)
            {
                this.logger.LogWarning($"{nameof(parameters)} is null");
                return new Result
                {
                    Started = this.clock.Now(),
                    Completed = this.clock.Now()
                };
            }

            var result = new Result
            {
                Input = parameters.Input,
                Started = this.clock.Now()
            };

            if (string.IsNullOrWhiteSpace(parameters.Input))
            {
                this.logger.LogWarning($"{nameof(parameters)} is null");
                return result;
            }

            //// split words
            this.logger.LogDebug($"Split {parameters.Input} into words");
            var words = parameters.Input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            //// clean
            for (var index = 0; index < words.Length; index++)
            {
                this.logger.LogDebug($"Clean {words[index]}");
                words[index] = RemoveSpecialCharacters(words[index]);
            }

            //// if no word left
            if (words.All(string.IsNullOrWhiteSpace))
            {
                this.logger.LogWarning($"There are no words left after cleaning");
                result.Completed = this.clock.Now();
                return result;
            }

            //// join words
            this.logger.LogDebug($"Join words and lower case");
            var kebab = words.Where(x => !string.IsNullOrWhiteSpace(x)).Aggregate((current, aggregated) => $"{current}-{aggregated}").ToLowerInvariant();
            result.Kebab = kebab;
            result.Completed = this.clock.Now();

            this.logger.LogDebug($"Result is {result.Kebab}");

            return result;
        }

        private static string RemoveSpecialCharacters(string word)
        {
            string result = string.Empty;
            foreach (var c in word)
            {
                if (char.IsLetterOrDigit(c))
                {
                    result += c;
                }
            }

            return result;
        }

        public class Parameters
        {
            [Required]
            public string Input { get; set; } = string.Empty;
        }

        public class Result
        {
            public string Kebab { get; set; } = string.Empty;

            public string Input { get; set; } = string.Empty;

            public DateTime Started { get; set; }

            public DateTime Completed { get; set; }
        }
    }
}
