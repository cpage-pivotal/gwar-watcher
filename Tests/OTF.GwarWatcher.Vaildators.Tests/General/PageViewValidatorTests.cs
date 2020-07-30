using Microsoft.VisualStudio.TestTools.UnitTesting;
using OTF.GwarWatcher.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using OTF.GwarWatcher.Common.Extensions;
using OTF.GwarWatcher.Validators.Core;
using Should;
using System.Linq;

namespace OTF.GwarWatcher.Vaildators.Tests.General
{
    [TestClass]
    public class PageViewValidatorTests : ValidatorTestsBase
    {
        [TestMethod]
        public void Validate_Valid()
        {
            ValidatorProvider p = new ValidatorProvider();
            this.GetMessages(Data.General.PageView_Valid).ForEach(m =>
            {
                ValidatorResult result = p.Validate(m);
                m.EventId.ShouldEqual(0);
                result.ShouldNotBeNull();
                result.IsValid.ShouldBeTrue();
                result.Messages.Any().ShouldBeFalse();
            });
        }

        [TestMethod]
        public void Validate_Invalid()
        {
            ValidatorProvider p = new ValidatorProvider();
            this.GetMessages(Data.General.PageView_Invalid).ForEach(m =>
            {
                ValidatorResult result = p.Validate(m);
                m.EventId.ShouldEqual(0);
                result.ShouldNotBeNull();
                result.IsValid.ShouldBeFalse();
                result.Messages.Any().ShouldBeTrue();
            });
        }

    }
}
