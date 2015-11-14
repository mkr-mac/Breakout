using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public class InGame
{
    //The player's paddle.
    public Paddle Player;
    //The bricks.
    public Bricks Bricks;
    //The backgound.
    public Background Background;

    //List of Balls in play.
    public LinkedList<Ball> Balls;
    //List of balls that need to be removed.
    public LinkedList<Ball> OutOfBounds;

    public InGame()
    {
        //Load the game's content.
        Load();
        //Start the music!!
        SoundManager.Engine.Play2D(@"sounds\macplus.ogg");
    }

    private void Load()
    {
        //Load all of the classes.
        Bricks = new Bricks();
        Player = new Paddle();
        Background = new Background();
        //The lists too!
        Balls = new LinkedList<Ball>();
        OutOfBounds = new LinkedList<Ball>();

        //Give ourselves a ball to get going.
        Balls.AddLast(new Ball());
    }

    public Breakout.State Update(int ms, KeyboardState keyboardState)
    {
        //Update the bricks.
        //They don't do anything at the moment...
        //bricks.update(this);

        //Update the player.
        Player.Update(this, keyboardState);

        //Update the balls.
        foreach (Ball b in Balls)
            b.Update(this, ms);

        //Check the OutOfBounds list to see if we need to remove a Ball from Balls.
        foreach (Ball b in OutOfBounds)
        {
            Balls.Remove(b);
            //Check to see if there are no balls left and there is still life left.
            if (Balls.Count.Equals(0) && !Ball.Dead)
                //Adds a new ball to the stage.
                Balls.AddFirst(new Ball());
        }
        //If you suck at the game and get Game Over,
        if (Ball.Dead && keyboardState.IsKeyDown(Keys.Enter))
            //You can push the Enter button to return to the title screen.
            return Breakout.State.Title;

        //Here to keep us coming back!
        return Breakout.State.InGame;
    }

    public void Draw(AD2SpriteBatch sb)
    {
        //Draw the background.
        Background.Draw(sb);
        //Draw the player's paddle.
        Player.Draw(sb);
        //Draw the bricks.
        Bricks.Draw(sb);

        //Draw all the balls.
        foreach (Ball b in Balls)
            b.Draw(sb);
        //gg
        if (Ball.Dead)
            Utils.DefaultFont.Draw(sb, "GAME OVER", 25, 30, Color.Red, 4, true);
    }


    public static bool Collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
    {
        //Check for any collision.
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }
}
