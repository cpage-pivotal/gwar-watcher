using Newtonsoft.Json;
using OTF.GwarWatcher.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OTF.GwarWatcher.Vaildators.Tests
{
    public abstract class ValidatorTestsBase
    {
#nullable enable
        protected MessageModel GetMessage(byte[] fileContents, Encoding? enc = null) => this.GetMessage((enc ?? Encoding.UTF8).GetString(fileContents));
        protected IEnumerable<MessageModel> GetMessages(byte[] fileContents, Encoding? enc = null) => this.GetMessages((enc ?? Encoding.UTF8).GetString(fileContents));
#nullable disable
        protected MessageModel GetMessage(string fileContents) => JsonConvert.DeserializeObject<MessageModel>(fileContents);
        protected IEnumerable<MessageModel> GetMessages(string fileContents) => JsonConvert.DeserializeObject<IEnumerable<MessageModel>>(fileContents);
    }
}
