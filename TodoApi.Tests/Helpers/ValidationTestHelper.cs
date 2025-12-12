using System.ComponentModel.DataAnnotations;

namespace TodoApi.Tests.Helpers;

public static class ValidationTestHelper
{
    public static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        var ctx = new ValidationContext(model);
        Validator.TryValidateObject(model, ctx, results, validateAllProperties: true);
        return results;
    }
}