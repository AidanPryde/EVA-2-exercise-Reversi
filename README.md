# EVA-2-first-exercise-WFA-Reversi 

(1.) beadandó feladat: Windows Forms grafikus felületű alkalmazás
Közös követelmények:

  • A beadandók dokumentációból, valamint programból állnak, utóbbi csak a
megfelelő dokumentáció bemutatásával értékelhető. Csak funkcionálisan teljes, a
feladatnak megfelelő, önállóan megvalósított, személyesen bemutatott program
fogadható el.

  • A megvalósításnak felhasználóbarátnak, és könnyen kezelhetőnek kell lennie. A
szerkezetében törekednie kell az objektumorientált szemlélet megtartására.

  • A programot háromrétegű (modell/nézet/perzisztencia) architektúrában kell
felépíteni, amelyben elkülönül a megjelenítés, az üzleti logika, valamint az
adatelérés (amennyiben része a feladatnak). Az egyes rétegeknek megfelelő
funkcionalitást kell biztosítania, és megfelelő hierarchiában kell kommunikálnia
(pl. a modell csak eseményekkel kommunikálhat a nézettel, a nézet nem
végezheti az adatkezelést).

  • A modell működését egységtesztek segítségével kell ellenőrizni. Nem kell teljes
körű tesztet végezni, azonban a lényeges funkciókat, és azok hatásait ellenőrizni
kell.

  • A program játékfelületét dinamikusan kell létrehozni futási időben. A
megjelenítéshez lehet vezérlőket használni, vagy elemi grafikát. Egyes
feladatoknál különböző méretű játéktábla létrehozását kell megvalósítani, ekkor
ügyelni kell arra, hogy az ablakméret mindig alkalmazkodjon a játéktábla
méretéhez.

 • A dokumentációnak jól áttekinthetőnek, megfelelően formázottnak kell lennie,
tartalmaznia kell a fejlesztő adatait, a feladatleírást, a feladat elemzését,
felhasználói eseteit (UML felhasználói esetek diagrammal), a program
szerkezetének leírását (UML osztálydiagrammal), valamint a tesztesetek leírását.
A dokumentáció ne tartalmazzon kódrészleteket, illetve képenyőképeket. A
megjelenő diagramokat megfelelő szerkesztőeszköz segítségével kell előállítani.
A dokumentációt elektronikusan, PDF formátumban kell leadni.

(17.) Reversi
Készítsünk programot, amellyel az alábbi Reversi játékot játszhatjuk.
A játékot két játékos játssza n × n-es négyzetrácsos táblán fekete és fehér
korongokkal. Kezdéskor a tábla közepén X alakban két-két korong van elhelyezve
mindkét színből. A játékosok felváltva tesznek le újabb korongokat. A játék
lényege, hogy a lépés befejezéseként az ellenfél ollóba fogott, azaz két oldalról
(vízszintesen, függőlegesen vagy átlósan) közrezárt bábuit (egy lépésben akár
több irányban is) a saját színünkre cseréljük.
Mindkét játékosnak, minden lépésben ütnie kell. Ha egy állásban nincs olyan
lépés, amivel a játékos ollóba tudna fogni legalább egy ellenséges korongot,
passzolnia kell és újra ellenfele lép. A játékosok célja, hogy a játék végére minél
több saját színű korongjuk legyen a táblán.
A játék akkor ér véget, ha a tábla megtelik, vagy ha mindkét játékos passzol. A
játék győztese az a játékos, akinek a játék végén több korongja van a táblán. A
játék döntetlen, ha mindkét játékosnak ugyanannyi korongja van a játék végén.
A program biztosítson lehetőséget új játék kezdésére a táblaméret megadásával
(10 × 10, 20 × 20, 30 × 30), játék szüneteltetésére, valamint játék mentésére és
betöltésére. Ismerje fel, ha vége a játéknak, és jelenítse meg, melyik játékos
győzött. A program folyamatosan jelezze külön-külön a két játékos gondolkodási
idejét (azon idők összessége, ami az előző játékos lépésétől a saját lépéséig tart,
ezt is mentsük el és töltsük be).
