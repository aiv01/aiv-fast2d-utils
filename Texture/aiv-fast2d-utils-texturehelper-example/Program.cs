using System;
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
            Window window = new Window(800, 600, "Test", false, 16, 4);
            window.EnableDepthTest();
            PerspectiveCamera cam = new PerspectiveCamera(Vector3.Zero, new Vector3(0, 0, 0), 60, 0.1f, 1000);
            #endregion

            #region DecompressTexture
            /* 
             * Decompress Texture from single file
             * This method has to be called only once,
             * because it could slows down the game,
             * do decompression in single project which is not
             * a part of your own working project.
             */

            string filepth = "Assets/SubAssets/Child01/drum1_roughness.png";
            TextureHelper.GenerateDecompressedTextureFromFile(filepth, "Assets/SubAssets/Child01/AssetsNew");

            //Decompress Textures from list of files (Has to be called only once)
            //TextureHelper.GenerateDecompressedTexturesFromFolder("Assets", "AssetsNew");
            #endregion

            #region LoadDecompressedTexture
            //Return decompressed texture and save it.
            Texture loadedTexture = TextureHelper.LoadDecompressedTexture("Assets/SubAssets/Child01/AssetsNew/drum1_roughness.tex");
            #endregion

            #region LoadOBJModel
            //Create a Mesh3[] to load custo ".obj"'s model
            Mesh3[] meshes = ObjLoader.Load("Assets/barrels_obj.obj", Vector3.One);

            foreach (var item in meshes)
            {
                item.Position3 += new Vector3(0, 0, 200);
            }
            #endregion

            #region GameLoop
            while (window.opened)
            {
                //Loop through array and draw texture.
                foreach (var item in meshes)
                {
                    item.DrawTexture(loadedTexture);
                }

                //Break while loop
                if (window.GetKey(KeyCode.Esc))
                    break;

                window.Update();
            }
            #endregion
        }
    }
}
