using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Background
{
    Texture2D BackgroundTexture;

    public Background()
    {
        BackgroundTexture = Utils.TextureLoader(@"vaporsky.png");
    }

    public void Draw(AD2SpriteBatch sb)
    {
        sb.DrawTexture(BackgroundTexture, 0, 0);
    }
}
