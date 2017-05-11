# aiv-fast2d-utils-texturehelper
The *TextureHelper.cs* static class is an Editor Utility that optimizes texture files (such as *.png, *.bmp, *.jpg...) by decompressing them into a new smart format;

In order to work with this, you have to add Fast2D and OpenTK libraries as dependencies in your project
(you can download them from NuGet or you can find them at these repositories: [Fast2D](https://github.com/aiv01/aiv-fast2d) and [OpenTK](https://github.com/aiv01/opentk)).

#How to use it:
+ You have to create a new empty project and import this utility (use it in Editor mode)
+ Your file will be generated and placed inside <yourproject>//bin//Debug folder
+ Following an example of use:

```cs
TextureHelper.GenerateDecompressedTextureFromFile("MyTexture.png");
```