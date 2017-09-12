using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebOptimizer.NodeServices;

namespace WebOptimizer.Less
{
    /// <summary>
    /// A LESS compiler
    /// </summary>
    /// <seealso cref="WebOptimizer.NodeServices.NodeProcessor" />
    public class LessProcessor : NodeProcessor
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => "LESS";

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        public override async Task ExecuteAsync(IAssetContext context)
        {
            var env = (IHostingEnvironment)context.HttpContext.RequestServices.GetService(typeof(IHostingEnvironment));
            var fileProvider = context.Asset.GetFileProvider(env);
            var content = new Dictionary<string, byte[]>();

            if (!EnsureNodeFiles("WebOptimizer.Less.node_files.zip"))
                return;

            string module = Path.Combine(InstallDirectory, "index.js");

            foreach (string route in context.Content.Keys)
            {
                var file = fileProvider.GetFileInfo(route);
                var input = context.Content[route].AsString();
                var result = await NodeServices.InvokeAsync<string>(module, input, file.PhysicalPath);

                content[route] = result.AsByteArray();
            }

            context.Content = content;
        }
    }
}
