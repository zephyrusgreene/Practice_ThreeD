using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Practice_ThreeD.Game.Textures;
public class TextureMgr
{
    public static Dictionary<string, Model> models;
    public static Dictionary<string, Texture2D> textures;
    static ContentManager Content;

    public static void Init()
    {
        Content = Game1.Instance.Content;
        models = new Dictionary<string, Model>();
        textures = new Dictionary<string, Texture2D>();
    }

    public static void LoadTextures()
    {
        models.Add("cube", Content.Load<Model>("3d_gfx/MonoCube"));
        models.Add("tractor", Content.Load<Model>("3d_gfx/TractorShovel"));
        textures.Add("1x1", Content.Load<Texture2D>("gfx/1x1"));
    }
}
