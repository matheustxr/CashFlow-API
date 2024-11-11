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
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestedLanguages = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var languageInfo = new CultureInfo("en");

        if (string.IsNullOrEmpty(requestedLanguages) == false && supportedLanguages.Exists(language => language.Name.Equals(requestedLanguages)))
        {
            languageInfo = new CultureInfo(requestedLanguages);
        }

        CultureInfo.CurrentCulture = languageInfo;
        CultureInfo.CurrentUICulture = languageInfo;

        await _next(context);
    }
}
