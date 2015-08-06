using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zmmn.Infrastructure.View
{
    public interface IHtmlRender
    {
        HttpContent Render(string file);
    }
}
