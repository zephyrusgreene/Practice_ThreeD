using Microsoft.Xna.Framework.Input;
using Practice_ThreeD.Game.Graphics;

namespace Practice_ThreeD.Game.Scenes;
public class Director
{
    public static GraphicsMgr graphics { get; private set; }
    KeyboardState keyboard;
    KeyboardState pKeyboard;

    int sceneId;
    const int SCENE_CUBE = 0;
    const int SCENE_TRACTOR = 1;

    IScene scene;


    public Director(GraphicsMgr graphics)
    {
        Director.graphics = graphics;
    }

    public void Update(float frameTime)
    {
        if (scene == null)
        {
            scene = new TractorScene();
            sceneId = SCENE_TRACTOR;
        }

        pKeyboard = keyboard;
        keyboard = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Game1.Instance.Exit();

        if (keyboard.IsKeyDown(Keys.F3) && !pKeyboard.IsKeyDown(Keys.F3)) 
        {
            if (sceneId == SCENE_CUBE) {
                sceneId = SCENE_TRACTOR;
                scene = new TractorScene();
            }
            else {
                sceneId = SCENE_CUBE;
                scene = new CubeScene();
            }
        } 

        scene.Update(frameTime);
    }

    public void Draw()
    {
        scene.Draw(graphics);
    }
}
