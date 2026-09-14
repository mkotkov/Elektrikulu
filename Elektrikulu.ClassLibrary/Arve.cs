namespace Elektrikulu.ClassLibrary
{
    public static class Arve
    {
        public const decimal MinTarbimine = 0m;
        public const decimal MaxTarbimine = 100000m; // kWh 30 päeva jooksul

        public static bool IsValidTarbimine(decimal kogus) =>
            kogus >= MinTarbimine && kogus <= MaxTarbimine;

        public static bool IsValidHind(decimal hind) => hind >= 0;

        public static bool IsValidKaibemaks(decimal protsent) =>
            protsent >= 0 && protsent <= 100;

        public static decimal Arve_lugemine(decimal kogus, decimal hind, decimal kaibemaksu_protsent, bool kaibemaksu_kasutamine)
        {
            hind /= 100;
            decimal taastuvenergia_tasu = kogus * (0.84m / 100);
            decimal varustuskindluse_tasu = kogus * (0.758m / 100);
            decimal elektriaktsiis = kogus * (0.21m / 100);
            decimal vorguuhenduse_kuutasu = 0.82m;

            decimal uldtariif = kogus * (7.83m / 100);
            decimal tasakaalustamisvoimsuse = kogus * (0.373m / 100);

            decimal kogusumma = (hind * kogus) + taastuvenergia_tasu + varustuskindluse_tasu + elektriaktsiis + uldtariif + tasakaalustamisvoimsuse + vorguuhenduse_kuutasu;
            decimal kaibemaksu_summa = (kogusumma * (kaibemaksu_protsent / 100));
            
            if (kaibemaksu_kasutamine)
            {
                kogusumma += kaibemaksu_summa;
            }
            return Math.Round(kogusumma, 2, MidpointRounding.AwayFromZero);
        }
    }
}
