# aiv-fast2d-utils-texturehelper
The *TextureHelper.cs* static class is an Editor Utility that optimizes texture files (such as *.png, *.bmp, *.jpg...) by decompressing them into a new smart format;

In order to work with this, you have to add Fast2D and OpenTK libraries as dependencies in your project
(you can download them from NuGet or you can find them at these repositories: [Fast2D](https://github.com/aiv01/aiv-fast2d) and [OpenTK](https://github.com/aiv01/opentk)).

#How to use it:
+ You have to create a new empty project and import this utility (use it in Editor mode)
+ Your file will be generated and placed inside <yourproject>//bin//Debug folder
+ The new generated file will have ".txt" as default extension;
+ Following an example of use:

```cs
TextureHelper.GenerateDecompressedTextureFromFile("MyTexture.png");
```

#How to load decompressed Texture:
```cs
Window window = new Window("Test");

while(window.opened)
{
	//Load it after you decompressed it.
	Texture loadedTexture = LoadDecompressedTexture(WidthSize, HeightSize, "MyTexture.txt");
	
	//Create a 3D mesh and draw the mesh with loaded texture.
	Mesh3[] newMesh = ObjLoaded.Load("MyModel.obj", size);
	
	//Loop through array and draw the texture.
	foreach(Mesh3[] item in meshes)
	{
	    item.DrawTexture(loadedTexture);
	}
}
```

You can also Decompress a big list of textures inside a folder, Following an example how to do it:

```cs
bool recursiveLoop = false; // this loop will loop through all sub directories under "FolderDirectory" given as entry parameter
TextureHelper.GenerateDecompressedTextureFromFolder("FolderDirectory", "NewFolderName", recursiveLoop);
```

