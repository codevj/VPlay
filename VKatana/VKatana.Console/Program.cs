namespace VKatana.Console
{
    using System;
    using Microsoft.Owin;
    using Microsoft.Owin.Hosting;
    using Owin;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<Program>("http://localhost:8080"))
            {
                Console.WriteLine("Server started");
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();
                Console.WriteLine("Server stopped!");
            }
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseShowEnvironmentInResponseBody();
            app.UseShowEnvironmentInConsole();
        }
    }
}
