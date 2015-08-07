namespace VKatana
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Owin;

    public static class VAppBuilderExtensions
    {
        public static void UseShowEnvironment(
            this IAppBuilder app,
            Func<IDictionary<string, object>, string, Task> logAction,
            string delimiter)
        {
            app.Use(
                async (ctx, next) =>
                {
                    IDictionary<string, object> environment = ctx.Environment;
                    var response = environment["owin.ResponseBody"] as Stream;
                    var sb = new StringBuilder();
                    sb.Append($"Total # of keys:{environment.Keys.Count}{delimiter}");
                    foreach (var key in environment.Keys)
                    {
                        sb.Append($"{key}:{environment[key]}{delimiter}");
                    }

                    await logAction(ctx.Environment, sb.ToString());

                    await next();
                });
        }

        public static void UseShowEnvironmentInResponseBody(this IAppBuilder app)
        {
            app.UseShowEnvironment(
                async (environment, message) =>
                {
                    var response = environment["owin.ResponseBody"] as Stream;
                    using (var writer = new StreamWriter(response))
                    {
                        await writer.WriteAsync(message);
                    }
                },
                "<br/>");
        }

        public static void UseShowEnvironmentInConsole(this IAppBuilder app)
        {
            app.UseShowEnvironment(
                async (environment, message) => { await Task.Run(() => Console.WriteLine(message)); },
                "\n");
        }
    }
}
