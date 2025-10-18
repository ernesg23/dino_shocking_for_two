using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace dino_jockey_for_two;

public class Game1 : Core
{
    private Player _player1;
    private Player _player2;
    private TextureAtlas _atlas;

    public Game1() : base("Dino Jockey", 1280, 720, true) { }

    protected override void LoadContent()
    {
        _atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

        var walkAnim = _atlas.GetAnimation("dino_walk");
        var jumpAnim = _atlas.GetAnimation("dino_jump");

        _player1 = new Player(walkAnim, jumpAnim, new Vector2(100, 200), Keys.Space, 200);
        _player2 = new Player(walkAnim, jumpAnim, new Vector2(100, 400), Keys.Up, 400);

        _player1.Sprite.Scale = new Vector2(2.0f, 2.0f);
        _player2.Sprite.Scale = new Vector2(2.0f, 2.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var keyboard = Keyboard.GetState();

        _player1.Update(gameTime, keyboard);
        _player2.Update(gameTime, keyboard);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _player1.Draw(SpriteBatch);
        _player2.Draw(SpriteBatch);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
