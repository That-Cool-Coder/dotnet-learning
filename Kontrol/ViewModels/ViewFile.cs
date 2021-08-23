using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontrol.ViewModels
{
    public struct ViewFile
    {
        public Helpers.FileReadState fileReadState;
        public Helpers.FileType fileType;
        public string filePath;
        public byte[] fileContent;
    }
}
