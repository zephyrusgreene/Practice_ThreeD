using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Practice_ThreeD.Game.Graphics;
using Practice_ThreeD.Game.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_ThreeD.Game.Scenes;
public class CubeScene : IScene
{
    Model cubeModel;
    Texture2D boxTexture;
    Vector3 camTarget;
    Vector3 camPosition;
    Matrix projectionMatrix;
    Matrix viewMatrix;
    Matrix worldMatrix;

    Vector2[] cubes = new Vector2[10]; 
    Vector2 playerLoc = new Vector2();
    Vector2 playerTraj = new Vector2();
    float ground = GraphicsMgr.Instance.deviceManager.PreferredBackBufferHeight / 2 + 125f;
    const float GRAVITY = 2000f;
    public CubeScene()
    {
        TextureMgr.models.TryGetValue("cube", out cubeModel); 
        TextureMgr.textures.TryGetValue("1x1", out boxTexture);
        camTarget = new Vector3(0f, 0f, 0f);
        camPosition = new Vector3(0f, 0f, -5f);
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i] = new Vector2(0f + 2f * i, -3.5f);
        }
        playerLoc = new Vector2(GraphicsMgr.Instance.deviceManager.PreferredBackBufferWidth / 2f - 50f,
            GraphicsMgr.Instance.deviceManager.PreferredBackBufferHeight/2 + 125f);
    }

    public void Update(float frameTime)
    {
        float cameraSpeed = 0.08f;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            camTarget.X -= cameraSpeed;
            camPosition.X -= cameraSpeed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            camTarget.X += cameraSpeed;
            camPosition.X += cameraSpeed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && playerLoc.Y == ground)
            playerTraj.Y = -1000f;

        playerLoc += playerTraj * frameTime;
        playerTraj.Y += GRAVITY * frameTime;

        if (playerLoc.Y > ground)
        {
            playerLoc.Y = ground;
            playerTraj.Y = 0f;
        }

        viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
        projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Director.graphics.device.Viewport.AspectRatio,
                           1f, 50f);
        //projectionMatrix = Matrix.CreatePerspective(Director.graphics.GetScreenSize().X, Director.graphics.GetScreenSize().Y, 1, 25f);
    }

    public void Draw(GraphicsMgr graphics)
    {
        graphics.device.Clear(Color.White);

        graphics.device.RasterizerState = RasterizerState.CullCounterClockwise;
        graphics.device.DepthStencilState = DepthStencilState.Default;

        foreach (var cube in cubes)
        {

            foreach (ModelMesh mesh in cubeModel.Meshes)
            {
                worldMatrix = Matrix.CreateWorld(new Vector3(cube.X, cube.Y, 5f), Vector3.Forward, Vector3.Up);
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.AmbientLightColor = new Vector3(1f, 0, 0);
                    effect.View = viewMatrix;
                    effect.World = worldMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }
        }

        graphics.batch.Begin();
        graphics.batch.Draw(boxTexture, new Rectangle((int)playerLoc.X, (int)playerLoc.Y, 100, 200), Color.Black);
        graphics.batch.End();

    }
}
