using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GUI
{
    //Title Screen's texture.
    Texture2D GUIBackground;

    public GUI()
    {
        //Texture loaded in for the GUI Background.
        GUIBackground = Utils.TextureLoader(@"Head.png");
    }

    public void Draw(AD2SpriteBatch sb)
    {
        //Draw the GUI.
        sb.DrawTexture(GUIBackground, Breakout.StageWidth + 25, 94, 10, 16);
        Utils.DefaultFont.Draw(sb, "LEVEL", Breakout.StageWidth + 4, 4, Color.Red, 1);
        Utils.DefaultFont.Draw(sb, "1", Breakout.StageWidth + 24, 0, Color.White, 3);
        Utils.DefaultFont.Draw(sb, "SCORE:", Breakout.StageWidth + 4, 30, Color.Red, 1);
        Utils.DefaultFont.Draw(sb, (Ball.Points * 100).ToString(), Breakout.StageWidth + 25, 38, Color.White, 1);
        Utils.DefaultFont.Draw(sb, "HIGH", Breakout.StageWidth + 4, 54, Color.Red, 1);
        Utils.DefaultFont.Draw(sb, "SCORE:", Breakout.StageWidth + 12, 62, Color.Red, 1);
        Utils.DefaultFont.Draw(sb, (Ball.Points * 100).ToString(), Breakout.StageWidth + 25, 70, Color.White, 1);
        Utils.DefaultFont.Draw(sb, "LIVES", Breakout.StageWidth + 4, 86, Color.Red, 1);
        Utils.DefaultFont.Draw(sb, "X" + Ball.BallsLeft.ToString(), Breakout.StageWidth + 36, 100, Color.White, 1);
    }

    public void Update(InGame world, KeyboardState ks)
    {

    }
}
