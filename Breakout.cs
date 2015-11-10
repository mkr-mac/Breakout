using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

public class Breakout : AD2Game
{
    // Player character.
    public Paddle player;
    //boolean array of bricks
    public Bricks bricks;
    //the backgound
    public Background bg;
    //list of Balls
    public LinkedList<Ball> balls;

    public LinkedList<Ball> outOfBounds = new LinkedList<Ball>();

    //speed the game runs at (1/gameSpeed = FPS)
    public static readonly int gameSpeed = 16;
    // Game Dimensions
    public static readonly int baseWidth = 320;
    public static readonly int baseHeight = 224;
    public static readonly int stageWidth = 250;


    public Breakout() : base(baseWidth, baseHeight, gameSpeed)
    {
        Renderer.resolution = Renderer.Resolution.WINDOWED_LARGE;
    }

    public static bool collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        //Check for any collision
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {

        //lol hold to pause
        if (keyboardState.IsKeyDown(Keys.Escape))
            return;

        //update the bricks
        //doesn't do anything ATM
        //bricks.update(this);

        //update the player.
        player.update(this, keyboardState);

        //update the balls
        foreach(Ball b in balls)
            b.update(this, ms);

        foreach (Ball b in outOfBounds)
        {
            balls.Remove(b);
            if(balls.Count.Equals(0))
                balls.AddLast(new Ball());
        }
    }

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        bg.draw(primarySpriteBatch);

        player.draw(primarySpriteBatch);

        bricks.draw(primarySpriteBatch);

        foreach (Ball b in balls)
            b.draw(primarySpriteBatch);
        
    }

    protected override void AD2LoadContent()
    {
        bricks = new Bricks();
        player = new Paddle();
        balls = new LinkedList<Ball>();
        bg = new Background();

        //get the first ball
        balls.AddLast(new Ball());

        //starts music
        SoundManager.engine.Play2D(@"sounds\macplus.ogg", true);

    }
}

