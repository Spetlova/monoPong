using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monopong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private kolo _kolo_a;
        private kolo _kolo_b;
        private obdelnik _obdelnik_a;
        private obdelnik _obdelnik_b;
        private Color barva_a = Color.Black;
        private Color barva_b = Color.Blue;
        private Color barva_c = Color.Red;

        private int _sirkaOkna = 1000;
        public int _vyskaOkna = 600;

        public Rectangle omezeniPohybu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _sirkaOkna;
            _graphics.PreferredBackBufferHeight = _vyskaOkna;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            omezeniPohybu = new Rectangle(0, 0, _sirkaOkna, _vyskaOkna);

            _kolo_a = new kolo(50, 1, 5, new Vector2(200, _vyskaOkna - 150), new smer(Keys.W, Keys.S), omezeniPohybu, barva_b, GraphicsDevice, _vyskaOkna, _sirkaOkna);

            _kolo_b = new kolo(50, -1, 5, new Vector2(_sirkaOkna - 200, 150), new smer(Keys.Up, Keys.Down), omezeniPohybu, barva_c, GraphicsDevice, _vyskaOkna, _sirkaOkna);

            _obdelnik_a = new obdelnik(25, 150, 4, new Vector2(50, (_vyskaOkna - 200) / 2), omezeniPohybu, barva_a, GraphicsDevice, _vyskaOkna);

            _obdelnik_b = new obdelnik(25, 150, 5, new Vector2(_sirkaOkna - 75, (_vyskaOkna - 200) / 2), omezeniPohybu, barva_a, GraphicsDevice, _vyskaOkna);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState klavesnice = Keyboard.GetState();

            _kolo_a.Aktualizovat(klavesnice, this);
            _kolo_b.Aktualizovat(klavesnice, this);
            _obdelnik_a.Aktualizovat();
            _obdelnik_b.Aktualizovat();

            _kolo_a.pozice_obd(_obdelnik_a._pozice, _obdelnik_b._pozice);
            _kolo_b.pozice_obd(_obdelnik_a._pozice, _obdelnik_b._pozice);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _kolo_a.Vykreslit(_spriteBatch);
            _kolo_b.Vykreslit(_spriteBatch);
            _obdelnik_a.Vykreslit(_spriteBatch);
            _obdelnik_b.Vykreslit(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}