using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Background
{
    Texture2D background;

    public Background()
    {
        background = Utils.TextureLoader(@"vaporsky.png");
    }

    public void draw(AD2SpriteBatch sb)
    {
        sb.drawTexture(background, 0, 0);
    }
}
