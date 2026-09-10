namespace Elektrikulu.ClassLibrary
{
    public class Arve
    {
       static void Main(string[] args)
       {
       }

        public static decimal Arve_lugemine(decimal kogus, decimal hind, decimal kaibemaksu_protsent, bool kaibemaksu_kasutamine)
        {
            decimal kaibemaksu_summa = (hind * kogus) * (kaibemaksu_protsent / 100);
            decimal kogusumma = (hind * kogus);
            if (kaibemaksu_kasutamine)
            {
                kogusumma += kaibemaksu_summa;
            }
            return kogusumma;
        }
    }
}
