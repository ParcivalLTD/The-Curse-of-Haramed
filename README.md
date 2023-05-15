# The Curse of Haramed
## Spielbeschreibung / Lore:
The Curse of Haramed ist ein Action-Strategiespiel, das in einer Welt spielt, in der Tiere mit dem Verstand von Menschen leben. Der Spieler muss das Land von einem Fluch befreien, der von einem mächtigen und bösartigen Menschen namens Magomed ausgeht. Der Fluch hat ihn in einen Haramed, ein Wesen mit unstillbarem Hunger nach dem Fleisch von Schweinen, Katzen und Creepern verwandelt. Haramed hat es geschafft, sich in mehrere Klone mit unterschiedlicher Stärke zu teilen, um seine Feinde zu besiegen. Der Spieler muss alle Klone besiegen und gegen Haramed im Endkampf antreten, um den Fluch zu brechen.

## Spielmechanik:
Der Spieler muss seine Basis schützen, indem er Verteidigungsanlagen (Tiere) platziert und Feinde, die sich ihm nähern, bekämpft. Die Tiere können verbessert werden, Feinde effektiver zu besiegen. Der Spieler muss auch eine Strategie entwickeln, um die verschiedenen Klone von Haramed zu besiegen, die alle unterschiedliche Stärken und Schwächen haben. 

## Steuerung:
Der Spieler kann die Tiere platzieren in dem er über dem Platzier-Spot hovered, und dann die zugehörige Taste pro Tier drückt (1, 2, 3, …)
Es gibt aktuell 10 Skripts, die jeweils eine Klasse enthalten:

•	GameOver: Verhalten des Spielendes. Lädt das aktuelle Level neu, wenn der Spieler das Spiel verloren hat.
•	BulletBehavior: Verwaltet das Verhalten der Geschosse, die von Monstern abgefeuert werden. Sie steuert die Bewegung, Kollision und den Schaden, den ein Geschoss verursacht.
•	EnemyDestructionDings: Verhalten der zerstörten Gegner. Informiert andere Klassen über die Zerstörung eines Gegners.
•	MonsterData: Speichert die Daten für die verschiedenen Level der Monster. Ermöglicht es, das aktuelle Level eines Monsters zu ändern, auf das nächste Level zuzugreifen und die Gesamtkosten für ein Monster zu berechnen.
•	MoveEnemy: steuert die Bewegung der Gegner entlang eines vorgegebenen Pfades. berechnet auch die verbleibende Distanz zum Ziel für jeden Gegner.
•	HealthBar: Verwaltet die Lebensanzeige für Gegner und Spieler. Aktualisiert die Anzeige basierend auf dem aktuellen Lebenswert.
•	SpawnEnemy: Steuert das Spawnen von Gegnern in verschiedenen Wellen. Bestimmt auch, wann das Spiel gewonnen ist.
•	PlaceMonster: Platziert jeweiliges Tier bei Keypress.
•	ShootEnemies: Ermöglicht es Monstern, auf Gegner zu schießen, die sich in ihrer Reichweite befinden. Wählt das am nächsten liegende Ziel aus und rotiert das Monster, um auf den Gegner zu zielen.
•	GameManagerBehavior: Verwaltet den Spielzustand, einschließlich Gold, Wellen und Gesundheit. Aktualisiert die Benutzeroberfläche und steuert das Spielende.
Alle Spielassets sind selbst gezeichnet.
