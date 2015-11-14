using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Breakout : AD2Game
{

    public Title T;
    public InGame Game;

    public enum State { Title, InGame }
    State GS;

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

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {

        //Kill the program if escape is pressed
        if (keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        State newState;
        switch (GS)
        {
            case State.Title:
                newState = T.Update(keyboardState);
                if (newState == State.InGame)
                {
                    SoundManager.Engine.StopAllSounds();
                    GS = State.InGame;
                    Game = new InGame();
                }
                break;

            case State.InGame:

                newState = Game.Update(ms, keyboardState);
                if (newState == State.Title)
                {
                    SoundManager.Engine.StopAllSounds();
                    GS = State.Title;
                    T = new Title();
                }
                break;
        }
    }

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        switch (GS)
        {
            case State.Title:
                T.Draw(primarySpriteBatch);
                break;
            case State.InGame:
                Game.Draw(primarySpriteBatch);
                break;
        }
    }

    protected override void AD2LoadContent()
    {
        GS = State.Title;
        T = new Title();
    }
}

