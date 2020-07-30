using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core.PayloadProperty
{
    public class VideoUrlPropertyValidator : UrlPropertyValidator
    {
        public override string PropertyName => "VideoUrl";
        public override UriKind UriKindType => UriKind.Absolute;
    }
}
