# Elektrikulu

Lihtne C# ja WPF rakendus elektrikulu arvutamiseks.

Rakendus võimaldab arvutada elektrienergia kogukulu vastavalt tarbitud kogusele ja ühikuhinnale ning vajadusel lisada käibemaksu.

## Kasutatud tehnoloogiad

* C#
* .NET
* WPF
* XAML

## Funktsionaalsus

* Elektrikulu arvutamine
* Tarbitud koguse ja ühikuhinna arvestamine
* Käibemaksu lisamise võimalus
* Arvutusloogika ja sisendite valideerimine on eraldatud kasutajaliidesest (asuvad `Elektrikulu.ClassLibrary` teegis)

## Projekti eesmärk

Projekt on loodud õppeprojekti raames, et harjutada C# programmeerimist, objektorienteeritud programmeerimist ja WPF-i abil töölauarakenduste loomist.

## Käivitamine

Projekti käivitamiseks on vaja .NET SDK-d ja Visual Studiot koos **.NET desktop development** töökoormusega.

Ava projekt Visual Studios, ehita lahendus ja käivita WPF-rakendus.

## Fikseeritud tariifid (kehtivad hinnakomponendid)
- Taastuvenergia tasu: 0,84 senti/kWh
- Varustuskindluse tasu: 0,758 senti/kWh
- Elektriaktsiis: 0,21 senti/kWh
- Võrguühenduse kuutasu: 0,82 € (fikseeritud, ei sõltu tarbimisest)
- Üldtariif (võrguteenus): 7,83 senti/kWh
- Tasakaalustamisvõimsuse tasu: 0,373 senti/kWh

Kõik tariifid on defineeritud `Elektrikulu.ClassLibrary.Arve` klassis nimetatud konstantidena.

## Sisendite valideerimine

Teek kontrollib sisendeid enne arvutamist ja viskab vea, kui väärtused on lubatud vahemikust väljas:

| Sisend | Lubatud vahemik | Viga sobimatu väärtuse korral |
|---|---|---|
| Tarbitud kogus (kWh) | 0–100 000 | `ArgumentOutOfRangeException`: "Tarbimiskogus peab jääma vahemikku 0–100000 kWh." |
| Hind (senti/kWh) | ≥ 0 | `ArgumentOutOfRangeException`: "Hind ei tohi olla negatiivne." |
| Käibemaksu protsent | 0–100 | `ArgumentOutOfRangeException`: "Käibemaksu protsent peab jääma vahemikku 0–100." |

## Kontrollnäited

1. Tarbimine 300 kWh, börsihind 8,5 senti/kWh, käibemaks ei kasutata
   → oodatud tulemus: **56,35 €**
2. Tarbimine -50 kWh (vigane sisend)
   → oodatud tulemus: viskab `ArgumentOutOfRangeException` sõnumiga "Tarbimiskogus peab jääma vahemikku 0–100000 kWh."