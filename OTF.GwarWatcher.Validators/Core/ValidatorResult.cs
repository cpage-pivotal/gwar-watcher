using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{
    public class ValidatorResult
    {
        public ValidatorResult() { }

        public ValidatorResult(IEnumerable<ValidatorResult> results) : this()
        {
            this.Messages = results.SelectMany(r => r.Messages);
            this.IsValid = results.All(r => r.IsValid);
        }
        public bool IsValid { get; set; } = true;
        public IEnumerable<string> Messages { get; set; } = new List<string>();

        public void Concat(IEnumerable<ValidatorResult> results)
        {
            if(results != null && results.Any())
            {
                this.Messages = this.Messages.Concat(results.SelectMany(r => r.Messages));
                this.IsValid = this.IsValid && results.All(r => r.IsValid);
            }
        }

        public void Concat(ValidatorResult result)
        {
            if(result != null)
            {
                this.Messages = this.Messages.Concat(result.Messages);
                this.IsValid = this.IsValid && result.IsValid;
            }
        }
    }
}
