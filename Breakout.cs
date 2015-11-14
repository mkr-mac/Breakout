using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Breakout : AD2Game
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

    //speed the game runs at (1/gameSpeed = FPS)
    public static readonly int GameSpeed = 16;
    // Game Dimensions
    public static readonly int BaseWidth = 320;
    public static readonly int BaseHeight = 224;
    public static readonly int StageWidth = 250;


    public Breakout() : base(BaseWidth, BaseHeight, GameSpeed)
    {
        Renderer.Resolution = Renderer.ResolutionType.WindowedLarge;
    }

    public static bool Collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        //Check for any collision
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {

        //Kill the program if escape is pressed
        if (keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        //update the bricks
        //doesn't do anything ATM
        //bricks.update(this);

        //update the player.
        Player.Update(this, keyboardState);

        //update the balls
        foreach(Ball b in Balls)
            b.Update(this, ms);

        foreach (Ball b in OutOfBounds)
        {
            Balls.Remove(b);
            if (Balls.Count.Equals(0) && !Ball.Dead)
                Balls.AddFirst(new Ball());
        }
    }

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        Background.Draw(primarySpriteBatch);

        Player.Draw(primarySpriteBatch);

        Bricks.Draw(primarySpriteBatch);

        foreach (Ball b in Balls)
            b.Draw(primarySpriteBatch);

        if (Ball.Dead)
            Utils.DefaultFont.draw(primarySpriteBatch, "Game Over", 25, 30, Color.Red, 4, true);
    }

    protected override void AD2LoadContent()
    {
        Bricks = new Bricks();
        Player = new Paddle();
        Balls = new LinkedList<Ball>();
        OutOfBounds = new LinkedList<Ball>();
        Background = new Background();

        //get the first ball
        Balls.AddLast(new Ball());
        //starts music
        SoundManager.Engine.Play2D(@"sounds\macplus.ogg", true);

    }
}

