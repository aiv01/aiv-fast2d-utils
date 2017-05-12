using System;
using System.IO;

namespace Aiv.Fast2D.Utils.TextureHelper
{
    public static class TextureHelper
    {
        /// <summary>
        /// It will generates a single decompressed texture inside giving it's source name path.
        /// </summary>
        /// <param name="sourceFileName">Name of the Texture to Decompress</param>
        /// <param name="targetExtensionName">Name of extension the file will be created with</param>
        public static void GenerateDecompressedTextureFromFile(string sourceFileName, string targetExtensionName = "txt")
        {
            if (!File.Exists(sourceFileName))
                throw new ArgumentException("File not found");

            if (string.IsNullOrEmpty(targetExtensionName))
                throw new ArgumentException("Target Extension Name can't be null or empty");

            Texture texture = new Texture(sourceFileName);
            string name = Path.GetFileNameWithoutExtension(sourceFileName);

            File.WriteAllBytes(name + "." + targetExtensionName, texture.Bitmap);
        }

        /// <summary>
        /// It will generates a list of decompressed textures inside a new directory giving it's folder path.
        /// </summary>
        /// <param name="targetDirectory">Directory which contains list of textures to decompress</param>
        /// <param name="newDirectoryName">Directory Which has to be created in order to place all the new decompressed textures</param>
        /// <param name="targetExtensionName">Extension which the file will be created</param>
        /// <param name="recursiveCheck">A recursive loop checking through all sub directories</param>
        public static void GenerateDecompressedTexturesFromFolder(string targetDirectory, string newDirectoryName, string targetExtensionName = "txt", bool recursiveCheck = false)
        {
            if (string.IsNullOrEmpty(targetDirectory) || string.IsNullOrEmpty(newDirectoryName))
                throw new Exception("targetDir or newDirName can't be null or empty");

            string[] entriesFiles = Directory.GetFiles(targetDirectory);
            Directory.CreateDirectory(newDirectoryName);
            foreach (string fileName in entriesFiles)
            {
                Texture texture = new Texture(fileName);
                string singleFileName = Path.GetFileNameWithoutExtension(fileName);
                File.WriteAllBytes(newDirectoryName + "/" + singleFileName + "." + targetExtensionName, texture.Bitmap);
            }

            if (!recursiveCheck)
                return;

            string[] subDir = Directory.GetDirectories(targetDirectory);
            foreach (string subDirectory in subDir)
            {
                GenerateDecompressedTexturesFromFolder(subDirectory, newDirectoryName, targetExtensionName, true);
            }
        }

        /// <summary>
        /// It will loads a decompressed texture.
        /// </summary>
        /// <param name="width">Texture width</param>
        /// <param name="height">Texture height</param>
        /// <param name="fileName">Name of decompressed file</param>
        /// <returns></returns>
        public static Texture LoadDecompressedTexture(int width, int height, string fileName)
        {
            if (!File.Exists(fileName))
                throw new ArgumentException("File not found");

            Texture texture = new Texture(width, height);
            texture.Update(File.ReadAllBytes(fileName));
            return texture;
        }
    }
}