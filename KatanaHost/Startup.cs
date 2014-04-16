using System.Text;
using System.Threading.Tasks;
using Owin;
using Owin.Types;

namespace KatanaHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseFileServer(true);
            app.UseWelcomePage("/welkom");
            
            app.UseHandlerAsync((req, res, next) =>
            {
                if (req.Path == "/owin")
                {
                    res.StatusCode = 302;
                    res.AddHeader("Location", "http://owin.org/");
                    return Task.FromResult(0);
                }
                return next();
            });
            
            app.UseHandlerAsync(RequestDump);
        }

        private static Task RequestDump(OwinRequest request, OwinResponse response)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<h1>Katana demo op TechDays</h1>");
            builder.AppendLine("<h2>request.Dictionary</h2>");

            builder.Append("<table>");
            foreach (var item in request.Dictionary)
            {
                builder.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>",
                    item.Key, item.Value);
            }
            builder.Append("</table>");

            response.ContentType = "text/html";
            return response.WriteAsync(builder.ToString());
        }
    }
}