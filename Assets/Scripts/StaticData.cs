namespace Common
{
    public static class StaticData
    {
        /// <summary>
        /// 맵의 x사이즈, 최소 5
        /// </summary>
        public static int maxXSize { get;set;}

        /// <summary>
        /// 맵의 y사이즈, 최소 5
        /// </summary>
        public static int maxYSize { get;set;}

        /// <summary>
        /// 지뢰개수, 최소 1
        /// </summary>
        public static int mineQty { get;set;}

        /// <summary>
        /// 커스텀맵 지뢰 배치의 랜덤성 여부
        /// </summary>
        public static bool randomMine { get; set; }

        
    }
}