using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Title
{
    Texture2D TitleSreen;

    public Title()
    {
        TitleSreen = Utils.TextureLoader(@"vaporsky.png");
    }

    public void Draw(AD2SpriteBatch sb)
    {
        sb.DrawTexture(TitleSreen, 0, 0, Breakout.BaseWidth, Breakout.BaseHeight);

        Utils.DefaultFont.Draw(sb, "PRESS ENTER TO BEGIN", 35, 160, Color.White, 2, true);
    }

    public Breakout.State Update(KeyboardState ks)
    {
        if (ks.IsKeyDown(Keys.Enter))
            return Breakout.State.InGame;

        return Breakout.State.Title;
    }
}
