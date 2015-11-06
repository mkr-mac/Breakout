﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Breakout : AD2Game
{
    // Player character.
    Paddle player;
    //boolean array of bricks
    Bricks bricks;
    //the ball
    Balls balls;

    // This game's level.
    //public FlatMap level;

    // Game Dims.
    public static readonly int baseWidth = 320;
    public static readonly int baseHeight = 224;
    public static readonly int stageWidth = 250;


    public Breakout() : base(baseWidth, baseHeight, 6)
    {
        //lol stub constructor
    }

    public static bool collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }

    public static bool collideX(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
    {
        return !(((x1 - (x2 + (w2 / 2)))/ (w2 / 2)) > (((y2 + (h2 / 2)) - (y1 + h1)) / (h2 / 2)) ||
            ((x1 - (x2 + (w2 / 2))) / (w2 / 2)) > ((y1 - (y2 - (h2 / 2))) / (h2 / 2)) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) > (y1 - ((y2 + (h2 / 2))) / (h2 / 2))) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) > (((y2 + (h2 / 2)) - (y1 + w1)) / (h2 / 2))));
    }

    public static bool collideY(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2)
    {
        return !(((x1 - (x2 + (w2 / 2))) / (w2 / 2)) < (((y2 + (h2 / 2)) - (y1 + h1)) / (h2 / 2)) ||
            ((x1 - (x2 + (w2 / 2))) / (w2 / 2)) < ((y1 - (y2 - (h2 / 2))) / (h2 / 2)) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) < (y1 - ((y2 + (h2 / 2))) / (h2 / 2))) ||
            ((((x2 + (w2 / 2)) - (x1 + w1)) / (w2 / 2)) < (((y2 + (h2 / 2)) - (y1 + w1)) / (h2 / 2))));
    }

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {
        
        //update the player.
        player.update(this,keyboardState);

        //update the baddies.
        bricks.update(this);

        balls.update(this);
        
    }

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        //primarySpriteBatch.drawTexture(background, 0, 0);

        //level.drawBase(primarySpriteBatch,0, 0);

        player.draw(primarySpriteBatch);

        bricks.draw(primarySpriteBatch);

        balls.draw(primarySpriteBatch);
        
    }

    protected override void AD2LoadContent()
    {
        bricks = new Bricks();
        //make bricks
        player = new Paddle();
        balls = new Balls();

        //TODO : should not need to pass screen width or height
        //level = new FlatMap("map/map.xml", Breakout.baseWidth, Breakout.baseHeight);

    }
}
