using Practice_ThreeD.Game.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_ThreeD.Game.Scenes;
public interface IScene
{
    public void Update(float frameTime);
    public void Draw(GraphicsMgr graphics);
}
