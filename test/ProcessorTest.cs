using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WebOptimizer.Less.Test
{
    public class ProcessorTest
    {
        [Fact]
        public async Task Compile_Success()
        {
            var processor = new LessProcessor();
            var pipeline = new Mock<IAssetPipeline>().SetupAllProperties();
            var context = new Mock<IAssetContext>().SetupAllProperties();
            var asset = new Mock<IAsset>().SetupAllProperties();
            var env = new Mock<IHostingEnvironment>();
            var fileProvider = new Mock<IFileProvider>();

            string temp = Path.GetTempPath();
            string path = Path.Combine(temp, "foo.less");
            File.WriteAllText(path, "@foo: 1px; * {margin: @foo}");

            var inputFile = new PhysicalFileInfo(new FileInfo(path));

            context.Object.Content = new Dictionary<string, byte[]> {
                { "/file.less", "@foo: 1px; * {margin: @foo}".AsByteArray() },
            };

            context.Setup(s => s.HttpContext.RequestServices.GetService(typeof(IHostingEnvironment)))
                  .Returns(env.Object);

            context.SetupGet(s => s.Asset)
                        .Returns(asset.Object);

            env.SetupGet(e => e.WebRootFileProvider)
                 .Returns(fileProvider.Object);

            fileProvider.Setup(f => f.GetFileInfo(It.IsAny<string>()))
                   .Returns(inputFile);

            await processor.ExecuteAsync(context.Object);
            var result = context.Object.Content.First().Value;

            Assert.Equal("* {\n  margin: 1px;\n}", result.AsString().Trim());
        }
    }
}
