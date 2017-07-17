using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Map
{
    public abstract class Map
    {
        public Size MapSize { get; set; }
        public Size TiledSize { get; set; }
        public Size MapTiledNum { get; set; }

        Tiled[] TiledArray;

        public Tiled GetTiledByIndex(int x, int y)
        {
            if (x < 0 || y < 0 || x >= MapTiledNum.width || y >= MapTiledNum.height)
            {
                return null;
            }

            if (x * MapTiledNum.height + y >= TiledArray.Length)
            {
                return null;
            }

            return TiledArray[x * MapTiledNum.height + y];
        }

        public void SetTiledByIndex(int x, int y, Tiled tiled)
        {
            if (x < 0 || y < 0 || x >= MapTiledNum.width || y >= MapTiledNum.height)
            {
                return ;
            }

            TiledArray[x * MapTiledNum.height + y] = tiled;
        }
    }
}
