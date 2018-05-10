using System;
using System.Collections.Generic;
using System.IO;

namespace Aiv.Fast2D.Utils.TextureHelper
{
    public static class TextureHelper
    {
        private static DirectoryInfo dirInfo;
        private static string extension;

        /// <summary>
        /// It will generates a single decompressed texture inside giving it's source name path.
        /// </summary>
        /// <param name="fileName">Name of the file that will be decompressed.</param>
        /// <param name="newDirectoryName">Name of the new directory that the decompressed file will be put into.</param>
        /// <param name="targetExtensionName">Extension name of the new file that will be created. Def is 'tex' - stands for 'Texture'</param>
        public static void GenerateDecompressedTextureFromFile(string fileName, string newDirectoryName, string targetExtensionName = "tex")
        {
            Validate(fileName, newDirectoryName);

            dirInfo = Directory.CreateDirectory(newDirectoryName);
            extension = targetExtensionName;

            string singleFileName = Path.GetFileNameWithoutExtension(fileName);

            if (File.Exists(dirInfo.Name + "/" + singleFileName + "." + extension))
                return;

            WriteFile(fileName);
        }

        /// <summary>
        /// It will generates a list of decompressed textures inside a new directory giving it's folder path.
        /// </summary>
        /// <param name="targetDirectory">Directory which contains list of textures to decompress</param>
        /// <param name="newDirectoryName">Directory Which has to be created in order to place all the new decompressed textures</param>
        /// <param name="recursive">A recursive loop
        /// <param name="targetExtensionName">Extension which the file will be created</param>
        /// checking through all sub directories</param>
        public static void GenerateDecompressedTexturesFromFolder(string targetDirectory, string newDirectoryName, bool recursive = false, string targetExtensionName = "tex")
        {
            Validate(targetDirectory, newDirectoryName);

            string[] entriesFiles = GetFiles(targetDirectory, "*.png|*.jpeg|*.bmp");
            dirInfo = Directory.CreateDirectory(newDirectoryName);
            extension = targetExtensionName;

            foreach (string fileName in entriesFiles)
            {
                string fileNameNoExt = Path.GetFileNameWithoutExtension(fileName);

                if (File.Exists(dirInfo.Name + "/" + fileNameNoExt + "." + extension))
                    return;

                WriteFile(fileName);
            }

            if (!recursive)
                return;

            string[] subDir = Directory.GetDirectories(targetDirectory);
            foreach (string subDirectory in subDir)
            {
                GenerateDecompressedTexturesFromFolder(subDirectory, newDirectoryName, recursive, targetExtensionName);
            }
        }

        /// <summary>
        /// Load a texture after GenerateDecompressedTextureFromFile and return a new decompressed Texture
        /// </summary>
        /// <param name="fileName">Name of the file that will be loaded.</param>
        /// <returns></returns>
        public static Texture LoadDecompressedTexture(string fileName)
        {
            ReadFile(fileName, out Texture toReturn);
            return toReturn;
        }

        /// <summary>
        /// Just loads a bunch of decompressed textures 
        /// </summary>
        /// <param name="directoryName">Name of the directory containing all the textures that needs to be loaded.</param>
        /// <returns></returns>
        public static List<Texture> LoadDecompressedTextures(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
                throw new ArgumentNullException();

            string[] files = Directory.GetFiles(directoryName);

            List<Texture> textures = new List<Texture>();

            foreach (var file in files)
            {
                ReadFile(file, out Texture outTex);
                textures.Add(outTex);
            }

            return textures;
        }

        #region Private_Methods

        private static void Validate(params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
                if (string.IsNullOrEmpty(values[i]))
                    throw new ArgumentNullException("Value is null or file is not found!!");
        }
        private static void ReadFile(string fileName, out Texture outTex)
        {
            if (!File.Exists(fileName))
                throw new ArgumentException("File not found");

            outTex = null;

            using (FileStream fs = File.OpenRead(fileName))
            {
                byte[] hashCode = new byte[4];

                fs.Read(hashCode, 0, sizeof(int));
                int hash = BitConverter.ToInt32(hashCode, 0);

                byte[] toRead = new byte[4];

                fs.Read(toRead, 0, sizeof(int));
                int width = BitConverter.ToInt32(toRead, 0);

                fs.Read(toRead, 0, sizeof(int));
                int height = BitConverter.ToInt32(toRead, 0);

                byte[] bitmap = new byte[fs.Length - 12];

                fs.Read(bitmap, 0, bitmap.Length);

                outTex = new Texture(width, height);

                outTex.Update(bitmap);
            }
        }
        private static void WriteFile(string fileName)
        {
            Texture texture = new Texture(fileName);
            string singleFileName = Path.GetFileNameWithoutExtension(fileName);

            using (FileStream fs = File.OpenWrite(dirInfo.Name + "/" + singleFileName + "." + extension))
            {
                byte[] data = new byte[sizeof(int) * 3 + texture.Bitmap.Length];

                byte[] hashCode = BitConverter.GetBytes(texture.GetHashCode());
                byte[] widthAsBytes = BitConverter.GetBytes(texture.Width);
                byte[] heightAsBytes = BitConverter.GetBytes(texture.Height);

                Array.Copy(hashCode, 0, data, 0, hashCode.Length);
                Array.Copy(widthAsBytes, 0, data, 4, widthAsBytes.Length);
                Array.Copy(heightAsBytes, 0, data, 8, heightAsBytes.Length);
                Array.Copy(texture.Bitmap, 0, data, 12, texture.Bitmap.Length);

                fs.Write(data, 0, data.Length);
            }
        }
        private static string[] GetFiles(string path, string searchPattern)
        {
            uint index = 0;
            string[] searchPatterns = searchPattern.Split('|');
            List<string> files = new List<string>();
            while (index != searchPatterns.Length)
            {
                string sp = searchPatterns[index];
                files.AddRange(Directory.GetFiles(path, sp));
                index++;
            }
            files.Sort();
            return files.ToArray();
        }

        #endregion
    }
}