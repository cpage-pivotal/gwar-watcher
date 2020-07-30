using OTF.GwarWatcher.Validators;
using OTF.GwarWatcher.Validators.Core;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OTF.GwarWatcher.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OTF.GwarWatcher.Vaildators.Tests.Checkout
{
    [TestClass]
    public class OrderPlacedValidatorTests : ValidatorTestsBase
    {
        [TestMethod]
        public void Validate_Valid()
        {
            ValidatorProvider p = new ValidatorProvider();
            this.GetMessages(Data.Checkout.OrderPlaced_Valid).ForEach(m =>
            {
                ValidatorResult result = p.Validate(m);
                m.EventId.ShouldEqual(28);
                result.ShouldNotBeNull();
                result.IsValid.ShouldBeTrue();
                result.Messages.Any().ShouldBeFalse();
            });
        }

        [TestMethod]
        public void Validate_Invalid()
        {
            //ValidatorProvider p = new ValidatorProvider();
            //this.GetMessages(Data.Pdp.ViewProductPdf_Invalid).ForEach(m =>
            //{
            //    ValidatorResult result = p.Validate(m);
            //    m.EventId.ShouldEqual(12);
            //    result.ShouldNotBeNull();
            //    result.IsValid.ShouldBeFalse();
            //    result.Messages.Any().ShouldBeTrue();
            //});
        }
    }
}
