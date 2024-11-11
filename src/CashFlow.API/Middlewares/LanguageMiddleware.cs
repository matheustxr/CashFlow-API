using System.Globalization;

namespace CashFlow.API.Middlewares;

public class LanguageMiddleware
{
    private readonly RequestDelegate _next;
    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke (HttpContext context)
    {
        var language = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var languageInfo = new CultureInfo("en");

        if (string.IsNullOrEmpty(language) == false)
        {
            languageInfo = new CultureInfo(language);
        }

        CultureInfo.CurrentCulture = languageInfo;
        CultureInfo.CurrentUICulture = languageInfo;

        await _next(context);
    }
}
