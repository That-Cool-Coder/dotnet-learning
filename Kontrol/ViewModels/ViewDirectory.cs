using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontrol.ViewModels
{
    public class ViewDirectory
    {
        public Helpers.FileReadState directoryReadState;
        public string directoryPath;
        public List<string> childDirectories = new();
        public List<string> childFiles = new();
    }
}
