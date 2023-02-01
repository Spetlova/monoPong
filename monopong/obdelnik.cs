using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monopong
{
    class obdelnik
    {
        private GraphicsDevice _zobrazovac { get; set; }

        private kolo debil;

        private int _vyska { get; set; }
        private int _sirka { get; set; }
        private int _rychlost { get; set; }

        private Color _barva { get; set; }

        public Vector2 _pozice { get; set; }
        private Texture2D _textura { get; set; }

        private Rectangle _omezeniPohybu { get; set; }

        private int _vyskaokna { get; set; }



        public obdelnik(int vyska, int sirka, int rychlost, Vector2 pozice, Rectangle omezeniPohybu, Color barva, GraphicsDevice zobrazovac, int vyskaokna)
        {
            _vyska = vyska;
            _sirka = sirka;

            _rychlost = rychlost;

            _barva = barva;
            _pozice = pozice;

            _zobrazovac = zobrazovac;

            _omezeniPohybu = omezeniPohybu;

            _vyskaokna = vyskaokna;
            _textura = PripravitTexturu();
        }

        private Texture2D PripravitTexturu()
        {
            Texture2D vyslednaTextura = new Texture2D(_zobrazovac, _vyska, _sirka);

            Color[] pixely = new Color[_vyska * _sirka];
            for (int i = 0; i < pixely.Length; i++)
                pixely[i] = Color.White;
            vyslednaTextura.SetData(pixely);

            return vyslednaTextura;
        }

        public void Aktualizovat()
        {
            _pozice = new Vector2(_pozice.X, _pozice.Y - _rychlost);

            if (_pozice.Y <= 0 || _pozice.Y >= _vyskaokna - 150)
            {
                _rychlost *= -1;
            }

        }

        


        /*public void Aktualizovat(Rectangle noveomezeniPohybu)
        {
            Pohnout();
            _omezeniPohybu = noveomezeniPohybu;
        }*/

        public void Vykreslit(SpriteBatch _vykreslovac)
        {
            _vykreslovac.Draw(_textura, _pozice, _barva);
        }
    }
}