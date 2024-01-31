global using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Practice_ThreeD.Game.Graphics;
using Practice_ThreeD.Game.Scenes;
using Practice_ThreeD.Game.Textures;

namespace Practice_ThreeD;
public class Game1 : Microsoft.Xna.Framework.Game
{
    public static Game1 Instance;
    public Director director;

    public Game1()
    {
        Instance = this;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        GraphicsDeviceManager graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1920,
            PreferredBackBufferHeight = 1080,
            IsFullScreen = true,
            HardwareModeSwitch = false,
            GraphicsProfile = GraphicsProfile.HiDef
        };
        graphics.ApplyChanges();
        director = new Director(new GraphicsMgr(graphics, new SpriteBatch(graphics.GraphicsDevice)));
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        TextureMgr.Init();
        TextureMgr.LoadTextures();
    }

    protected override void Update(GameTime gameTime)
    {
        director.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        director.Draw();
        base.Draw(gameTime);
    }
}
