using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontrol.Helpers
{
    public enum FileType
    {
        JPG,
        PNG,
        MP4,
        MOV,
        WMV,
        AVI,
        MKV,
        Text
    }

    public enum FileReadState
    {
        NOT_FOUND,
        PERMISSION_DENIED,
        READ
    }

    public class FileHelpers
    {
        private static Dictionary<string, FileType> extensionToFileType = new ()
        {
            { "jpg", FileType.JPG },
            { "png", FileType.PNG },
            { "mp4", FileType.MP4 },
            { "mov", FileType.MOV },
            { "wmv", FileType.WMV },
            { "avi", FileType.AVI },
            { "mkv", FileType.MKV}
        };

        private static Dictionary<FileType, string> fileTypeToBase64Prefix = new()
        {
            { FileType.JPG, "data:image/jpg;base64," },
            { FileType.PNG, "data:image/png;base64," },
            { FileType.MP4, "data:video/mp4;base64," },
            { FileType.MOV, "data:video/mov;base64," },
        };

        public static FileType FileTypeFromExtension(string filePath)
        {
            string extension = filePath.Split('.').LastOrDefault().ToLower();
            if (extension == null) return FileType.Text;
            try
            {
                return extensionToFileType[extension];
            }
            catch (KeyNotFoundException)
            {
                return FileType.Text;
            }
        }

        public static string Base64EncodeFile(FileType fileType, string content)
        {
            return Base64EncodeFile(fileType, Encoding.ASCII.GetBytes(content));
        }

        public static string Base64EncodeFile(FileType fileType, byte[] content)
        {
            string base64String = Convert.ToBase64String(content);
            string prefix;
            try
            {
                prefix = fileTypeToBase64Prefix[fileType];
            }
            catch
            {
                prefix = "";
            }
            base64String = prefix + base64String;
            return base64String;
        }
    }
}
