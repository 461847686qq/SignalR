using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSignalR.Interfases
{
    interface IFileCollection
    {
        void SetFile(string key, string value);
        string GetFile(string key);
    }
}