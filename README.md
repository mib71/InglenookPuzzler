# 🚂 Inglenook Puzzler

A digital companion for the classic **Inglenook Sidings shunting puzzle** — built for model railway enthusiasts who want to practice with their own rolling stock.

> Built with Blazor Server · EF Core · SQL Server · Self-contained

---

## What is the Inglenook Puzzle?

The Inglenook Sidings puzzle was created by Alan Wright and is based on the real Kilham Sidings on the Alnwick-Cornhill branch of the North Eastern Railway. The rules are simple:

- You have **8 wagons** distributed across three sidings (5-3-3)
- A headshunt allows you to move 1, 2 or 3 wagons at a time
- Your goal: form a **specific train of 5 wagons** on Track A — in the correct order
- A Brake Van, if included, must always be last

Simple rules. Surprisingly deep strategy. Over 40,000 possible combinations.

---

## Features

**Wagon Collection**
- Add your own wagons with photos, rolling stock numbers, type and era
- Upload photos from your phone or camera — automatically cropped and resized
- Default images per wagon type if no photo is available

**Puzzle**
- Generates puzzles from your own collection — you see your actual wagons on screen
- Headshunt with correct movement rules (1, 2 or 3 wagons per move)
- Move counter — one move per loco operation regardless of wagon count
- Brake Van always placed last in goal automatically
- Win detection and session saving

**Settings**
- Define your own wagon types and eras
- Seeded with British Era I–III types and wagon types out of the box

**Dashboard**
- Collection overview with breakdown by wagon type
- Puzzle stats — total solved, best score, average moves

---

## Screenshots

![Dashboard](docs/screenshots/dashboard.png)
![Collection](docs/screenshots/collection.png)
![Puzzle](docs/screenshots/puzzle.png)

---

## Stack

![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=flat-square&logo=blazor&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=flat-square&logo=visualstudio&logoColor=white)
![Git](https://img.shields.io/badge/Git-F05032?style=flat-square&logo=git&logoColor=white)

---

## Getting Started

**Prerequisites**
- .NET 10 SDK
- SQL Server (LocalDB works fine)

**Clone and run**
```bash
git clone https://github.com/mib71/InglenookPuzzler.git
cd InglenookPuzzler/InglenookPuzzler
dotnet run
```

The database is created and seeded automatically on first run — no manual migration needed.

**Connection string**

Update `appsettings.json` with your SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=InglenookPuzzler;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## How to Play

1. **Settings** — configure your own wagon types and eras (or use the defaults)
2. **Collection** — add your wagons with photos and rolling stock numbers
3. **New Puzzle** — generate a puzzle from your collection
4. **Play** — use the headshunt to move wagons between tracks, form the goal train on Track A in the correct order

---

## Roadmap

| Version | Features |
|---|---|
| **V1** ✅ | Wagon collection, image upload, digital puzzle, move counter, win detection, session saving |
| **V2** | Print card for physical play, highscores |

---

## About

Built by [mib71](https://github.com/mib71) — a .NET backend dev from Sweden who also happens to collect model railways.

🌐 [bifrostpixel.com](https://www.bifrostpixel.com)
