# 🎰 AdmiralCapital — Discord Casino Bot

A feature-rich **Discord casino bot** built with **C# / .NET Framework 4.7.2** and the **DSharpPlus** library. Players can gamble virtual currency, level up through an XP system, and spend their winnings in a shop — all through a fully interactive, button-driven UI inside Discord.

---

## 🚀 Features

- 🃏 **5 Casino Games** — Blackjack, Roulette, Crash, Dice & Number Guess
- 💰 **Economy System** — Per-server virtual balance with JSON persistence
- 🏆 **XP & Leveling** — 4 player ranks earned through gameplay (Beginner → Advanced → Addict → Veteran)
- 🛒 **In-Game Shop** — Purchase balance boosters to multiply winnings
- 🎮 **Interactive UI** — Discord buttons and embeds for all game flows (no plain-text commands)
- ⚙️ **Slash + Prefix Commands** — Supports both modern slash commands and classic prefix commands
- 🔄 **Auto-Reconnect** — Bot automatically reconnects on disconnect

---

## 🎮 Games

| Game | Description | Payout |
|------|-------------|--------|
| **Blackjack** | Classic Hit or Stand card game against the dealer | 2× bet |
| **Roulette** | Bet on a number (1–36), colour (Red/Black) or Even/Odd | Up to 36× bet |
| **Crash** | Pick a multiplier — cash out before the crash! | Up to 100× bet |
| **Dice** | Choose a face (1–6) and roll | 6× bet |
| **Guess** | Guess a random number between 1–5 | Variable |

---

## 🛠️ Tech Stack

| Technology | Purpose |
|------------|---------|
| **C# / .NET Framework 4.7.2** | Core language & runtime |
| **DSharpPlus** | Discord API wrapper |
| **DSharpPlus.Interactivity** | Button interactions & message awaiting |
| **DSharpPlus.SlashCommands** | Slash command registration & handling |
| **Newtonsoft.Json** | JSON serialization for persistent user data |

---

## 🏗️ Architecture & Design Patterns

The project is structured with clean separation of concerns:

```
AdmiralCapital/
├── zaidimai/               # Game logic (Blackjack, Roulette, Crash, Dice, Guess)
│   ├── IGame.cs            # Common game interface
│   └── GameEngine.cs       # Central game dispatcher
├── Balansas/               # Economy layer
│   ├── IUserStorage.cs     # Storage abstraction (interface)
│   ├── UserDataStorage.cs  # JSON-backed implementation
│   ├── BalansoEngine.cs    # Balance service (DI-friendly)
│   └── DUser.cs            # User domain model
├── Interfeisas/            # Discord UI & presentation
│   ├── InterfaceEngine.cs  # Menu & navigation orchestrator
│   ├── PatirtiesLygis.cs   # XP / leveling system (abstract class hierarchy)
│   ├── Shop.cs             # In-game shop
│   └── Pranesimai.cs       # Centralised embed message factory
├── Slash/                  # Slash command handlers
├── commands/               # Prefix command handlers
├── config/                 # JSON config reader (token, prefix)
└── Program.cs              # Bot bootstrap & event wiring
```

**Key patterns used:**
- **Interface segregation** — `IGame` contract shared across all 5 games; `IUserStorage` for storage abstraction
- **Dependency injection** — `BalansoEngine` receives `IUserStorage` via constructor injection
- **Abstract class inheritance** — `ExperienceLevel` base class with concrete rank subclasses (`Naujokas`, `Pazenges`, `Ekspertas`, `Veteranas`)
- **Event-driven architecture** — Discord component interactions handled through `ComponentInteractionCreated` events
- **Centralised messaging** — `Pranesimai` acts as a message factory, keeping all embed content in one place

---

## ⚙️ Setup & Configuration

### Prerequisites
- [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472)
- A Discord bot token ([Discord Developer Portal](https://discord.com/developers/applications))

### 1. Clone the repository
```bash
git clone https://github.com/Nedzas22/Discord-casino-bot-C-.git
cd Discord-casino-bot-C-
```

### 2. Create `config.json`
Create a `config.json` file in the output directory (`bin/Debug/`) with:
```json
{
  "token": "YOUR_DISCORD_BOT_TOKEN",
  "prefix": "!"
}
```

### 3. Build & Run
Open the solution in **Visual Studio 2019/2022** and press **F5**, or build via CLI:
```bash
msbuild AdmiralCapital.csproj /p:Configuration=Debug
```

### 4. Invite the bot
Generate an invite link in the Discord Developer Portal with the following permissions:
- `Send Messages`, `Embed Links`, `Read Message History`
- `Use Slash Commands`
- Bot scope with `applications.commands`

---

## 📸 How It Works

All game flows are driven by **Discord interactive buttons** — no typing raw values. Example Blackjack flow:

1. Player runs `/blackjack` or `!blackjack`
2. Bot asks for a bet amount
3. Initial cards are dealt and displayed via an embed
4. Player clicks **⚡ Hit** or **🛑 Stand** buttons
5. Dealer plays out, result embed shown with balance update

---

## 🔮 Potential Future Improvements

- [ ] Migrate from JSON file storage to a proper database (SQLite / PostgreSQL)
- [ ] Upgrade to **.NET 8** and modern DSharpPlus v5
- [ ] Add a leaderboard slash command
- [ ] Daily reward / cooldown system
- [ ] Unit tests for game logic

---

## 👨‍💻 Author

**Nedas** — Junior .NET Developer  
[![GitHub](https://img.shields.io/badge/GitHub-Nedzas22-181717?logo=github)](https://github.com/Nedzas22)

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
