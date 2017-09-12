using System.Collections.Generic;
using WebOptimizer;
using WebOptimizer.Less;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions methods for registrating the Less compiler on the Asset Pipeline.
    /// </summary>
    public static class LessPipelineExtensions
    {
        /// <summary>
        /// Compile LESS files on the asset pipeline.
        /// </summary>
        public static IAsset CompileLess(this IAsset asset)
        {
            asset.Processors.Add(new LessProcessor());
            return asset;
        }

        /// <summary>
        /// Compile LESS files on the asset pipeline.
        /// </summary>
        public static IEnumerable<IAsset> CompileLess(this IEnumerable<IAsset> assets)
        {
            var list = new List<IAsset>();

            foreach (IAsset asset in assets)
            {
                list.Add(asset.CompileLess());
            }

            return list;
        }

        /// <summary>
        /// Compile markdown files on the asset pipeline.
        /// </summary>
        /// <param name="pipeline">The asset pipeline.</param>
        /// <param name="route">The route where the compiled markdown file will be available from.</param>
        /// <param name="sourceFiles">The path to the markdown source files to compile.</param>
        public static IAsset AddLessBundle(this IAssetPipeline pipeline, string route, params string[] sourceFiles)
        {
            return pipeline.AddBundle(route, "text/css; charset=UTF-8", sourceFiles)
                           .CompileLess()
                           .Concatenate()
                           .MinifyCss();
        }

        /// <summary>
        /// Compiles LESS files into CSS and makes them servable in the browser.
        /// </summary>
        /// <param name="pipeline">The asset pipeline.</param>
        public static IEnumerable<IAsset> CompileLessFiles(this IAssetPipeline pipeline)
        {
            return pipeline.CompileLessFiles("**/*.less");
        }

        /// <summary>
        /// Compiles the specified LESS files into CSS and makes them servable in the browser.
        /// </summary>
        /// <param name="pipeline">The pipeline object.</param>
        /// <param name="sourceFiles">A list of relative file names of the sources to compile.</param>
        public static IEnumerable<IAsset> CompileLessFiles(this IAssetPipeline pipeline, params string[] sourceFiles)
        {
            return pipeline.AddFiles("text/css; charset=UTF-8", sourceFiles)
                           .CompileLess()
                           .MinifyCss();
        }
    }
}
