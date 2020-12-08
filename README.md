# SollicitatieOpdracht

Solliciate opdracht voor Nerds & Company

## Het opstarten van de applicaties
Om de werking te kunnen controleren dienen de volgende applicaties te worden gestart:
- Api --> Draaiend op https://localhost/44332
- OAuth2Api --> Draaiend op https://localhost:44303
- OAuth2Client --> Draaiend op https://localhost:44342/

Vanuit de OAuth2Client dient te worden gewerkt.

## Werking van het systeem
De Api en OAuth2Api hebben beiden een pagina met content die alleen te benaderen is wanneer er een accesstoken is gemaakt.

De OAuth2Client vraagt aan de OAuth2Api een accesstoken op waarbij een username opgegeven dient te worden.
De OAuth2Api maakt vervolgens de accesstoken en geeft deze terug.
De OAuth2Client slaat deze op in een cookie en benaderd vervolgens de Api met zojuist gemaakte token om te kijken of deze ook hier bij de secret content mag.