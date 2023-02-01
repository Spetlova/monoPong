using Microsoft.Xna.Framework.Input;

namespace monopong
{
    class smer
    {
        public Keys _nahoru { get; private set; }
        public Keys _dolu { get; private set; }

        public smer(Keys nahoru, Keys dolu)
        {
            _nahoru = nahoru;
            _dolu = dolu;
        }
    }
}