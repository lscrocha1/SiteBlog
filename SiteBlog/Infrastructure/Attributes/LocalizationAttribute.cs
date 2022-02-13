using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace SiteBlog.Infrastructure.Attributes;

public class LocalizationAttribute : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains(Constants.Constants.Language.TwoLetterEnglish) 
            && !CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains(Constants.Constants.Language.TwoLetterPortuguese))
            CultureInfo.CurrentCulture = new CultureInfo(Constants.Constants.Language.English);

        var header = context.HttpContext.Request.Headers;

        var language = header["Content-Language"];

        if (string.IsNullOrEmpty(language))
            return;

        if (language.ToString() != Constants.Constants.Language.Portuguese 
            && language.ToString() != Constants.Constants.Language.TwoLetterEnglish)
            language = Constants.Constants.Language.English;

        CultureInfo.CurrentCulture = new CultureInfo(language);

    }
}