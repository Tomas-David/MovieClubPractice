# Cvičení: REST API – Filmový klub

## Popis

Vytvoř REST API v ASP.NET Core (.NET 10) pro správu filmového klubu. Skupina přátel si hlasuje o tom, jaký film půjdou příště sléhat. Každý člen klubu může přidávat filmy a hlasovat pro ty, které chce vidět.

---

## Datový model

```csharp
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public string AddedBy { get; set; }
    public int Votes { get; set; }
    public bool IsWatched { get; set; }
    public DateTime AddedAt { get; set; }
}
```

---

## Endpointy

### GET /api/movies
Vrátí seznam všech filmů seřazených podle počtu hlasů sestupně. Podporuje volitelné filtrování přes query string `?genre=Horror` a `?addedBy=Honza`.

### GET /api/movies/{id}
Vrátí detail filmu podle ID. Pokud film neexistuje, vrátí `404 Not Found`.

### GET /api/movies/unwatched
Vrátí pouze filmy, které ještě nebyly shlédnuty (`IsWatched == false`).

### GET /api/movies/winner
Vrátí film s nejvyšším počtem hlasů mezi neshlédnutými filmy. Pokud jsou dva filmy se stejným počtem hlasů, vrátí ten, který byl přidán dříve. Pokud není žádný film, vrátí `404 Not Found`.

### POST /api/movies
Přidá nový film. `Votes` se nastaví automaticky na `0`, `IsWatched` na `false`. Vrátí `201 Created`.

```json
{
  "title": "Inception",
  "year": 2010,
  "genre": "Sci-Fi",
  "addedBy": "Honza"
}
```

### POST /api/movies/{id}/vote
Přidá jeden hlas k filmu. Jméno hlasujícího se předá jako query parametr: `/api/movies/3/vote?voter=Petra`. Každý člen může hlasovat pro jeden film pouze jednou – pokud již hlasoval, vrátí `400 Bad Request`. Nelze hlasovat pro film, který již byl shlédnut.

### PUT /api/movies/{id}
Aktualizuje titul, rok nebo žánr filmu. Pokud film neexistuje, vrátí `404 Not Found`.

### DELETE /api/movies/{id}
Odstraní film. Pokud film neexistuje, vrátí `404 Not Found`. Vrátí `204 No Content`.

---

## Poznámky

- Data ukládej v paměti pomocí statického `List<Movie>` – databáze není potřeba.
- Pro sledování hlasů použij druhý seznam: `List<(int MovieId, string Voter)>`.
- API testuj přes Scalar UI, které se spustí automaticky na `/scalar`.
- Pozor na konflikt routování mezi `/unwatched` a `/{id}`.
- Zadaní je připraveno pro SqLite a je potřeba udělat migraci




