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
public class TractorScene : IScene
{ 
    public Model tractorModel;
    Vector3 camTarget;
    Vector3 camPosition;
    Matrix projectionMatrix;
    Matrix viewMatrix;
    Matrix worldMatrix;

    Vector2 tractor = new Vector2();
    float shovelFrame;
    float scrollVal;
    float pScrollVal;

    public TractorScene()
    {
        camTarget = new Vector3(0f, 0f, 0f);
        camPosition = new Vector3(0f, 0f, 25f);
        tractor = new Vector2(0f, 0f);
        TextureMgr.models.TryGetValue("tractor", out tractorModel);
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
        pScrollVal = scrollVal;
        scrollVal = Mouse.GetState().ScrollWheelValue;
        if (scrollVal > pScrollVal)
        {
            camPosition.Z -= cameraSpeed * 5f;            
        }
        if (scrollVal < pScrollVal)
        {
            camPosition.Z += cameraSpeed * 5f;
        }

        viewMatrix = Matrix.CreateLookAt(camPosition, camTarget, Vector3.Up);
        projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), Director.graphics.device.Viewport.AspectRatio,
                            1f, 25f);

        shovelFrame += frameTime;
        if (shovelFrame > 1) shovelFrame = 0f;
    }

    public void Draw(GraphicsMgr graphics)
    {
        graphics.device.Clear(Color.White);
        graphics.device.RasterizerState = RasterizerState.CullCounterClockwise;
        // Always reset StencilState for 3D if using spritebatch during draw.
        graphics.device.DepthStencilState = DepthStencilState.Default;


        foreach (ModelMesh mesh in tractorModel.Meshes)
        {
            worldMatrix = Matrix.CreateWorld(new Vector3(tractor.X, tractor.Y, 10f), Vector3.Forward, Vector3.Up);
            foreach (BasicEffect effect in mesh.Effects)
            {
                //effect.EnableDefaultLighting();
                effect.AmbientLightColor = new Vector3(1f, 0, 0);
                effect.View = viewMatrix;
                effect.World = worldMatrix;
                effect.Projection = projectionMatrix;
                if (mesh.Name.Contains("shovel"))
                {
                    worldMatrix = Matrix.CreateWorld(new Vector3(tractor.X, tractor.Y + shovelFrame * -0.2f, 10f), Vector3.Forward, Vector3.Up);
                    effect.World = Matrix.CreateRotationX(MathHelper.ToRadians(-50 * shovelFrame)) * worldMatrix;
                }
            }
            mesh.Draw();
        }

    }
}
