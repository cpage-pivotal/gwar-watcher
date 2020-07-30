using System;
using System.Collections.Generic;
using System.Text;

namespace OTF.GwarWatcher.Validators.Core
{

    public interface IValidator
    {

    }
    public interface IValidator<T> : IValidator
    {
        ValidatorResult Validate(T message);
    }
}
