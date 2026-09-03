using System.Text;
using System.Text.RegularExpressions;

namespace Aiursoft.HowToCookViewer.Services;

public class OgTagMiddleware
{
    private readonly RequestDelegate _next;

    public OgTagMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBody = context.Response.Body;

        using var newBody = new MemoryStream();
        context.Response.Body = newBody;

        await _next(context);

        if (context.Response.ContentType?.Contains("text/html") == true
            && context.Items.TryGetValue("OgTitle", out var ogTitleObj)
            && ogTitleObj is string ogTitle)
        {
            newBody.Seek(0, SeekOrigin.Begin);
            var html = await new StreamReader(newBody).ReadToEndAsync();

            var metaTags = new StringBuilder();
            metaTags.AppendLine($"    <meta property=\"og:title\" content=\"{EscapeHtml(ogTitle)}\">");

            if (context.Items.TryGetValue("OgDescription", out var descObj) && descObj is string description)
                metaTags.AppendLine($"    <meta property=\"og:description\" content=\"{EscapeHtml(description)}\">");

            if (context.Items.TryGetValue("OgImageUrl", out var imgObj) && imgObj is string imageUrl)
            {
                metaTags.AppendLine($"    <meta property=\"og:image\" content=\"{EscapeHtml(imageUrl)}\">");
                metaTags.AppendLine($"    <meta name=\"twitter:card\" content=\"summary_large_image\">");
                metaTags.AppendLine($"    <meta name=\"twitter:image\" content=\"{EscapeHtml(imageUrl)}\">");
            }

            metaTags.AppendLine($"    <meta property=\"og:type\" content=\"article\">");
            metaTags.AppendLine($"    <meta property=\"og:site_name\" content=\"HowToCook\">");
            metaTags.AppendLine($"    <meta name=\"twitter:title\" content=\"{EscapeHtml(ogTitle)}\">");
            if (context.Items.TryGetValue("OgDescription", out var twDescObj) && twDescObj is string twDesc)
                metaTags.AppendLine($"    <meta name=\"twitter:description\" content=\"{EscapeHtml(twDesc)}\">");

            html = html.Replace("</head>", metaTags.ToString() + "</head>");

            var buffer = Encoding.UTF8.GetBytes(html);
            newBody.SetLength(0);
            await newBody.WriteAsync(buffer);
        }

        newBody.Seek(0, SeekOrigin.Begin);
        await newBody.CopyToAsync(originalBody);
    }

    private static string EscapeHtml(string text) =>
        text.Replace("&", "&amp;")
            .Replace("\"", "&quot;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;");
}
