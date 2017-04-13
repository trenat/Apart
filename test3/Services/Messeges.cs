using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test3.Services
{
    public interface IMessages
    {
        string NotFound();
    }

    public class Messages : IMessages
    {
        public string NotFound()
        {
            return "Tu nie ma zadnej strony! :)";
        }
    }
}
