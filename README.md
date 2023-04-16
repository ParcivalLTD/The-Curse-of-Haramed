# The Curse of Haramed
## Spielbeschreibung / Lore:
The Curse of Haramed ist ein Action-Strategiespiel, das in einer Welt spielt, in der Tiere mit dem Verstand von Menschen leben. Der Spieler muss das Land von einem Fluch befreien, der von einem mächtigen und bösartigen Menschen namens Magomed ausgeht. Der Fluch hat ihn in einen Haramed, ein Wesen mit unstillbarem Hunger nach dem Fleisch von Schweinen, Katzen und Creepern verwandelt. Haramed hat es geschafft, sich in mehrere Klone mit unterschiedlicher Stärke zu teilen, um seine Feinde zu besiegen. Der Spieler muss alle Klone besiegen und gegen Haramed im Endkampf antreten, um den Fluch zu brechen.

## Spielmechanik:
Der Spieler muss seine Basis schützen, indem er Verteidigungsanlagen (Tiere) platziert und Feinde, die sich ihm nähern, bekämpft. Die Tiere können verbessert werden, Feinde effektiver zu besiegen. Der Spieler muss auch eine Strategie entwickeln, um die verschiedenen Klone von Haramed zu besiegen, die alle unterschiedliche Stärken und Schwächen haben. 
  
![image](https://user-images.githubusercontent.com/79400664/232307601-b9d686e4-6149-4c41-8717-7cd7992d7c0e.png)
![image](https://user-images.githubusercontent.com/79400664/232307608-b7067a65-5424-4a42-beb1-be924cecff43.png)

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

## Einige Gegner / Monster Assets: 

![image](https://user-images.githubusercontent.com/79400664/232307641-f24a8d50-d450-41bb-aa10-f0cae1b293a4.png)
![image](https://user-images.githubusercontent.com/79400664/232307647-60728c97-9d95-4665-b5f1-24f8eb2516f4.png)
![image](https://user-images.githubusercontent.com/79400664/232307650-802e6b58-8b1f-45e6-84f6-3205cc37c04f.png)
![image](https://user-images.githubusercontent.com/79400664/232307654-7edef2ae-9cf5-4628-bd3e-8a499f298006.png)
![image](https://user-images.githubusercontent.com/79400664/232307659-93d80a06-686f-41bb-a6b1-cfad7bf07537.png)
![image](https://user-images.githubusercontent.com/79400664/232307660-c6a2dbe1-dbc2-437d-a0e4-098f9124e912.png)
![image](https://user-images.githubusercontent.com/79400664/232307661-e2172450-6e0f-4b7d-9552-83caddf9a092.png)
![image](https://user-images.githubusercontent.com/79400664/232307664-457bf546-4a3c-488f-b696-51008c32563b.png)
![image](https://user-images.githubusercontent.com/79400664/232307671-a2ce01e2-e12a-44ea-a95c-a9f3e47b8f1b.png)
![image](https://user-images.githubusercontent.com/79400664/232307673-32b32ac8-8f14-423f-9067-56d892850cbe.png)
![image](https://user-images.githubusercontent.com/79400664/232307678-a746d47d-21cd-4857-8b23-d658ae8c0d50.png)
![image](https://user-images.githubusercontent.com/79400664/232307679-efb9cb4d-5412-4067-bc27-b3889732c480.png)
![image](https://user-images.githubusercontent.com/79400664/232307680-fe473af9-d4b1-4507-8001-200ba5b2ef88.png)
![image](https://user-images.githubusercontent.com/79400664/232307683-341932a8-bd99-46fd-8b1f-df2daf01d641.png)
![image](https://user-images.githubusercontent.com/79400664/232307686-adf6ef04-38d4-4fb8-a81f-e7eeec223e43.png)
![image](https://user-images.githubusercontent.com/79400664/232307687-a7d4a952-d048-4c1a-afa6-53dfd41366da.png)
![image](https://user-images.githubusercontent.com/79400664/232307691-de95a7cc-bbdc-487b-b890-63b76b7676dc.png)
![image](https://user-images.githubusercontent.com/79400664/232307695-93fa2d35-2655-49f0-ac29-fa987ffdc98c.png)
![image](https://user-images.githubusercontent.com/79400664/232307698-0aad190b-260c-4ad5-896b-98acad17376e.png)
![image](https://user-images.githubusercontent.com/79400664/232307702-71c75383-2c4c-49a3-a170-cd5a497f3555.png)
![image](https://user-images.githubusercontent.com/79400664/232307704-3fb7d518-eb58-4ef8-ae45-7a652fc150c6.png)

![image](https://user-images.githubusercontent.com/79400664/232307706-0fba78e4-66bc-4479-9175-0b287418678c.png)

       
 
