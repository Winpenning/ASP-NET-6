using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Extensions;
// por padrao as classes de extensao no c# devem ser estaticas
public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
    {
        var result = new List<string>();
        foreach (var VARIABLE in modelState.Values)
            result.AddRange(VARIABLE.Errors.Select(error => error.ErrorMessage));
        return result;
    }
}