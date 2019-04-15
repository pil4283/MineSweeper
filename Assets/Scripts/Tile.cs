using System;

namespace Model
{
    [Serializable]
    public class Tile
    {
        /// <summary>
        /// 타일의 x위치
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// 타일의 y위치
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// 타일타입 1 : 일반타일(지뢰x), 2 : 지뢰타일, 3 : 공백타일
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 아직 안밝혀진 구역인지
        /// </summary>
        public bool isBlind { get;set;}

        /// <summary>
        /// 주변에 있는 지뢰의 수
        /// </summary>
        public int mineNumber { get; set; }

        public Tile()
        {
            x = 0;
            y = 0;
            type = 1;
            isBlind = true;
            mineNumber = 0;
        }
        public Tile(int x, int y, int type, bool isBlind, int mineNumber)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            this.isBlind = isBlind;
            this.mineNumber = mineNumber;
        }
    }
}