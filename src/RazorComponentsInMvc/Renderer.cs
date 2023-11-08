using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RazorComponentsInMvc;

public static class Renderer
{
  public static async Task<string> RenderAsync<TComponent>(
    IHtmlHelper helper,
    HttpContext httpContext,
    object parameters
  ) where TComponent : IComponent
  {
    if (helper is IViewContextAware viewContextAware)
    {
      viewContextAware.Contextualize(
        new ViewContext()
        {
          HttpContext = httpContext
        }
      );
    }

    var content = await helper.RenderComponentAsync<TComponent>(RenderMode.Static, parameters);
    var writer = new StringWriter();
    content.WriteTo(writer, HtmlEncoder.Default);
    return writer.ToString();
  }

  public static HttpContext CreateDefaultContext(
    IServiceProvider serviceProvider
  )
  {
    return new DefaultHttpContext
    {
      RequestServices = serviceProvider,
      Request =
      {
        Scheme = "http",
        Host = new HostString("localhost"),
        PathBase = "/base",
        Path = "/path",
        QueryString = QueryString.FromUriComponent("?query=value")
      }
    };
  }
}
