using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class InGame
{
    // Player character.
    public Paddle Player;
    //boolean array of bricks
    public Bricks Bricks;
    //the backgound
    public Background Background;

    //list of Balls
    public LinkedList<Ball> Balls;

    public LinkedList<Ball> OutOfBounds;

    public InGame()
    {
        Load();

        SoundManager.Engine.Play2D(@"sounds\macplus.ogg");
    }

    private void Load()
    {
        Bricks = new Bricks();
        Player = new Paddle();
        Balls = new LinkedList<Ball>();
        OutOfBounds = new LinkedList<Ball>();
        Background = new Background();

        Balls.AddLast(new Ball());
    }

    public Breakout.State Update(int ms, KeyboardState keyboardState)
    {
        //update the bricks
        //doesn't do anything ATM
        //bricks.update(this);

        //update the player.
        Player.Update(this, keyboardState);

        //update the balls
        foreach (Ball b in Balls)
            b.Update(this, ms);

        foreach (Ball b in OutOfBounds)
        {
            Balls.Remove(b);
            if (Balls.Count.Equals(0) && !Ball.Dead)
                Balls.AddFirst(new Ball());
        }
        if (Ball.Dead && keyboardState.IsKeyDown(Keys.Enter))
            return Breakout.State.Title;

        return Breakout.State.InGame;
    }

    public void Draw(AD2SpriteBatch sb)
    {

        Background.Draw(sb);

        Player.Draw(sb);

        Bricks.Draw(sb);

        foreach (Ball b in Balls)
            b.Draw(sb);

        if (Ball.Dead)
            Utils.DefaultFont.Draw(sb, "GAME OVER", 25, 30, Color.Red, 4, true);
    }


    public static bool Collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
    {
        //Check for any collision
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }
}
