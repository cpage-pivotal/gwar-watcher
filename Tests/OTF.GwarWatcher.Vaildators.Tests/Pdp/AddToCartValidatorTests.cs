﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OTF.GwarWatcher.Validators;
using OTF.GwarWatcher.Validators.Core;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OTF.GwarWatcher.Common.Extensions;

namespace OTF.GwarWatcher.Vaildators.Tests.Pdp
{
    [TestClass]
    public class AddToCartValidatorTests : ValidatorTestsBase
    {
        [TestMethod]
        public void Validate_Valid()
        {
            ValidatorProvider p = new ValidatorProvider();
            this.GetMessages(Data.Pdp.AddToCart_Valid).ForEach(m =>
            {
                ValidatorResult result = p.Validate(m);
                m.EventId.ShouldEqual(1);
                result.ShouldNotBeNull();
                result.IsValid.ShouldBeTrue();
                result.Messages.Any().ShouldBeFalse();
            });
        }

        [TestMethod]
        public void Validate_Invalid()
        {
            ValidatorProvider p = new ValidatorProvider();
            this.GetMessages(Data.Pdp.AddToCart_Invalid).ForEach(m =>
            {
                ValidatorResult result = p.Validate(m);
                m.EventId.ShouldEqual(1);
                result.ShouldNotBeNull();
                result.IsValid.ShouldBeFalse();
                result.Messages.Any().ShouldBeTrue();
            });
        }
    }
}
