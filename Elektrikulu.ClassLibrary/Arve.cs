using System;

namespace Elektrikulu.ClassLibrary
{
    public static class Arve
    {
        public const decimal MinTarbimine = 0m;
        public const decimal MaxTarbimine = 100000m;

        public const decimal TaastuvenergiaTasuSendiKwh = 0.84m;
        public const decimal VarustuskindluseTasuSendiKwh = 0.758m;
        public const decimal ElektriaktsiisSendiKwh = 0.21m;
        public const decimal VorguuhenduseKuutasuEur = 0.82m;
        public const decimal UldtariifSendiKwh = 7.83m;
        public const decimal TasakaalustamisvoimsuseTasuSendiKwh = 0.373m;

        public static bool IsValidTarbimine(decimal kogus) =>
            kogus >= MinTarbimine && kogus <= MaxTarbimine;

        public static bool IsValidHind(decimal hind) => hind >= 0;

        public static bool IsValidKaibemaks(decimal protsent) =>
            protsent >= 0 && protsent <= 100;

        public static decimal Arve_lugemine(decimal kogus, decimal hind, decimal kaibemaksu_protsent, bool kaibemaksu_kasutamine)
        {
            if (!IsValidTarbimine(kogus))
                throw new ArgumentOutOfRangeException(nameof(kogus), kogus,
                    $"Tarbimiskogus peab jääma vahemikku {MinTarbimine}–{MaxTarbimine} kWh.");

            if (!IsValidHind(hind))
                throw new ArgumentOutOfRangeException(nameof(hind), hind,
                    "Hind ei tohi olla negatiivne.");

            if (!IsValidKaibemaks(kaibemaksu_protsent))
                throw new ArgumentOutOfRangeException(nameof(kaibemaksu_protsent), kaibemaksu_protsent,
                    "Käibemaksu protsent peab jääma vahemikku 0–100.");

            decimal hindEuris = hind / 100;

            decimal taastuvenergia_tasu = kogus * (TaastuvenergiaTasuSendiKwh / 100);
            decimal varustuskindluse_tasu = kogus * (VarustuskindluseTasuSendiKwh / 100);
            decimal elektriaktsiis = kogus * (ElektriaktsiisSendiKwh / 100);
            decimal uldtariif = kogus * (UldtariifSendiKwh / 100);
            decimal tasakaalustamisvoimsuse = kogus * (TasakaalustamisvoimsuseTasuSendiKwh / 100);

            decimal kogusumma = (hindEuris * kogus)
                + taastuvenergia_tasu
                + varustuskindluse_tasu
                + elektriaktsiis
                + uldtariif
                + tasakaalustamisvoimsuse
                + VorguuhenduseKuutasuEur;

            if (kaibemaksu_kasutamine)
            {
                decimal kaibemaksu_summa = kogusumma * (kaibemaksu_protsent / 100);
                kogusumma += kaibemaksu_summa;
            }

            return Math.Round(kogusumma, 2, MidpointRounding.AwayFromZero);
        }
    }
}