using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

public class Breakout : AD2Game
{
    // Player character.
    Paddle player;
    //boolean array of bricks
    Bricks bricks;
    //the ball
    Ball ball;
    //the backgound
    Background bg;

    //speed the game runs at (1/gameSpeed = FPS)
    public static readonly int gameSpeed = 16;
    // Game Dims.
    public static readonly int baseWidth = 320;
    public static readonly int baseHeight = 224;
    public static readonly int stageWidth = 250;


    public Breakout() : base(baseWidth, baseHeight, gameSpeed)
    {
        //lol stub constructor
        Renderer.resolution = Renderer.Resolution.WINDOWED;
    }

    public static bool collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        //Check for any collision
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }

    
    public static bool collideX(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
    {
        //Ckeck for collision on the sides
        //TODO: Fix this mess!
        return !(((x1 - (x2 + (w2 / 2)))/ (w2 / 2)) > (((y2 + (h2 / 2)) - (y1 + h1)) / (h2 / 2)) ||
            ((x1 - (x2 + (w2 / 2))) / (w2 / 2)) > ((y1 - (y2 - (h2 / 2))) / (h2 / 2)) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) > (y1 - ((y2 + (h2 / 2))) / (h2 / 2))) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) > (((y2 + (h2 / 2)) - (y1 + w1)) / (h2 / 2))));
    }

    public static bool collideY(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
    {
        //Ckeck for collision on the top or bottom
        //TODO: Fix this mess!
        return !(((x1 - (x2 + (w2 / 2))) / (w2 / 2)) < (((y2 + (h2 / 2)) - (y1 + h1)) / (h2 / 2)) ||
            ((x1 - (x2 + (w2 / 2))) / (w2 / 2)) > ((y1 - (y2 - (h2 / 2))) / (h2 / 2)) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) < (y1 - ((y2 + (h2 / 2))) / (h2 / 2))) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) > (((y2 + (h2 / 2)) - (y1 + w1)) / (h2 / 2))));
    }

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {
        
        //update the player.
        player.update(this,keyboardState);

        //update the bricks
        bricks.update(this);

        //update the bricks
        ball.update(this, ms);
        
    }

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        bg.draw(primarySpriteBatch);

        player.draw(primarySpriteBatch);

        bricks.draw(primarySpriteBatch);

        ball.draw(primarySpriteBatch);
        
    }

    protected override void AD2LoadContent()
    {
        bricks = new Bricks();
        //make bricks
        player = new Paddle();
        ball = new Ball();
        bg = new Background();

        //starts music
        SoundManager.engine.Play2D(@"sounds\macplus.ogg", true);

    }
}

