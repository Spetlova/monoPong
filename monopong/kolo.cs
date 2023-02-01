using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monopong
{
    class kolo
    {
        private GraphicsDevice _zobrazovac { get; set; }

        private int _velikost { get; set; }
        private int _rychlostX { get; set; }
        private int _rychlostY { get; set; }
        private int _vyska_okna { get; set; }
        private int _sirka_okna { get; set; }

        private Color _barva { get; set; }

        private Vector2 _pozice { get; set; }
        private Vector2 _pozice_obd_a { get; set; }
        private Vector2 _pozice_obd_b { get; set; }
        private Texture2D _textura { get; set; }

        private Rectangle _omezeniPohybu { get; set; }

        private smer _smer { get; set; }




        public kolo(int velikost, int rychlostX, int rychlostY, Vector2 pozice, smer ovladaniPohybu, Rectangle omezeniPohybu, Color barva, GraphicsDevice zobrazovac, int vyska_okna, int sirka_okna)
        {
            _velikost = velikost;
            _rychlostX = rychlostX;
            _rychlostY = rychlostY;

            _smer = ovladaniPohybu;
            _omezeniPohybu = omezeniPohybu;

            _barva = barva;
            _pozice = pozice;

            _zobrazovac = zobrazovac;

            _vyska_okna = vyska_okna;
            _sirka_okna = sirka_okna;
            
            _textura = createCircleText();
        }

        public void pozice_obd(Vector2 pozice_obd_a, Vector2 pozice_obd_b)
        {
            _pozice_obd_a = pozice_obd_a;
            _pozice_obd_b = pozice_obd_b;
        }
        Texture2D createCircleText()
        {
            Texture2D texture = new Texture2D(_zobrazovac, _velikost, _velikost);
            Color[] colorData = new Color[_velikost * _velikost];

            float diam = _velikost / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < _velikost; x++)
            {
                for (int y = 0; y < _velikost; y++)
                {
                    int index = x * _velikost + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }

        public void Aktualizovat(KeyboardState klavesnice, Game game)
        {

            _pozice = new Vector2(_pozice.X + _rychlostX, _pozice.Y);

            if (_pozice_obd_a.X + 25 == _pozice.X &&  _pozice.Y < _pozice_obd_a.Y + 125 && _pozice.Y > _pozice_obd_a.Y)
            {
                _rychlostX *= -1;
            }

            if (_pozice_obd_b.X == _pozice.X + 50 && _pozice.Y < _pozice_obd_b.Y + 125 && _pozice.Y > _pozice_obd_b.Y)
            {
                _rychlostX *= -1;
            }

            if(_pozice.X < 0 || _pozice.X > _sirka_okna - 50)
            {
                game.Exit();
            }

            if (_pozice.Y <= 0)
            {
                _pozice = new Vector2(_pozice.X, 0);
            }

            if (_pozice.Y >= _vyska_okna)
            {
                _pozice = new Vector2(_pozice.X, _vyska_okna - 50);
            }


            if (klavesnice.IsKeyDown(_smer._nahoru))
                _pozice = new Vector2(_pozice.X, _pozice.Y - _rychlostY);
            if (klavesnice.IsKeyDown(_smer._dolu))
                _pozice = new Vector2(_pozice.X, _pozice.Y + _rychlostY);
        }

        public void Vykreslit(SpriteBatch _vykreslovac)
        {
            _vykreslovac.Draw(_textura, _pozice, _barva);
        }
    }
}
