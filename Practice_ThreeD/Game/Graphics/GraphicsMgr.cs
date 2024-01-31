using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_ThreeD.Game.Graphics;
public class GraphicsMgr
{
    public static GraphicsMgr Instance;
    public SpriteBatch batch;
    public GraphicsDevice device { get => deviceManager.GraphicsDevice; }
    public GraphicsDeviceManager deviceManager;
    public Point screenSize;

    public GraphicsMgr(GraphicsDeviceManager deviceManager, SpriteBatch batch)
    {
        this.deviceManager = deviceManager;
        this.batch = batch;
        Instance = this;
    }

}
