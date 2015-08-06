using RazorEngine;
using System;
using System.IO;
using System.Net.Http;
using RazorEngine.Templating;

namespace Zmmn.Infrastructure.View
{
    public class RazorHtmlRender : IHtmlRender
    {
        public HttpContent Render(string name)
        {
            ArraySegment<byte> result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using ( TextWriter textWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8, 1024, true))
                {
                    Engine.Razor.RunCompile(name, textWriter);
                }
                result = new ArraySegment<byte>(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            return new ByteArrayContent(result.Array, result.Offset, result.Count); ;
        }
    }
}
