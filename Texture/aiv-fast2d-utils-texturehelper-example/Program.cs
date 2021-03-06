﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast3D;
using OpenTK;

namespace Aiv.Fast2D.Utils.TextureHelper.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //Object scale
            float size = 4f;

            #region InstanceWindow
            //Instance new window
            Window window = new Window(1024, 768, "Test", false, 16, 4);
            #endregion

            #region DecompressTexture
            /* 
             * Decompress Texture from single file
             * This method has to be called only once,
             * because it could slows down the game,
             * do decompression in single project which is not
             * a part of your own working project.
             */
            TextureHelper.GenerateDecompressedTextureFromFile("Assets/drum1_ambient.png", "NewAssets");

            //Decompress Textures from list of files (Has to be called only once)
            //bool bRecursive = false;
            //string sExtension = "tex";
            //TextureHelper.GenerateDecompressedTexturesFromFolder("Assets", "NewAssets", bRecursive, sExtension);
            #endregion

            #region LoadDecompressedTexture
            //Return decompressed texture and save it.
            Texture loadedTexture = TextureHelper.LoadDecompressedTexture("GasFuel.txt");
            #endregion

            #region LoadOBJModel
            //Create a Mesh3[] to load custo ".obj"'s model
            Mesh3 mesh = ObjLoader.Load("Assets/GasFuel.obj", Vector3.One * size)[0];
            #endregion

            #region GameLoop
            while (window.opened)
            {
                //draw the mesh
                mesh.DrawTexture(loadedTexture);

                //Break while loop
                if (window.GetKey(KeyCode.Esc))
                    break;

                window.Update();
            }
            #endregion
        }
    }
}
