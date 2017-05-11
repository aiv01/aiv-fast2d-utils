public static class TextureHelper
    {
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
    }