using Microsoft.Xna.Framework.Graphics;

public class Background
{
    //Background's texture.
    Texture2D BackgroundTexture;

    public Background()
    {
        //Load the texture from the file.
        BackgroundTexture = Utils.TextureLoader(@"vaporsky.png");
    }

    public void Draw(AD2SpriteBatch sb)
    {
        //Draw the background.
        sb.DrawTexture(BackgroundTexture, 0, 0);
    }
}
