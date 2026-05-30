using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Shared.Errors;

public sealed record ValidationError(Error[] Errors)
    : Error("Validation.Failed", "One or more validation errors occurred", ErrorType.Validation);
