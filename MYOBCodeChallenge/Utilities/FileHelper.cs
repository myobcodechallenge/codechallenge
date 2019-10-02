using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MYOBCodeChallenge.Utilities
{
    public static class FileHelper
    {
        public static string GetContentType(string fineName)
        {

            var ext = Path.GetExtension(fineName).ToLowerInvariant();
            return ext;
        }
    }
}
