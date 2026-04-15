using InglenookPuzzler.Models;
using InglenookPuzzler.Models.Puzzle;
using System.Text.Json;

namespace InglenookPuzzler.Services;

public class PuzzleGenerator(WagonService wagonService)
{
    private readonly WagonService _wagonService = wagonService;
    private readonly Random _random = new();

    public async Task<PuzzleSession> GenerateAsync(PuzzleConfig config)
    {
        var allWagons = await _wagonService.GetAllAsync();

        if (allWagons.Count < config.TotalWagons)
            throw new InvalidOperationException(
                $"Not enough wagons in collection. Need {config.TotalWagons}, have {allWagons.Count}.");

        // Pick exactly TotalWagons random wagons
        var selected = allWagons
            .OrderBy(_ => _random.Next())
            .Take(config.TotalWagons)
            .ToList();

        // Start state — distribute all 8 wagons across tracks
        var startState = GenerateStartState(selected, config);

        // Goal state — pick 5 wagons in a random order for track A
        var goalState = GenerateGoalState(selected, config);

        return new PuzzleSession
        {
            CreatedAt = DateTime.UtcNow,
            StartState = JsonSerializer.Serialize(startState),
            GoalState = JsonSerializer.Serialize(goalState),
            Config = config
        };
    }

    private List<TrackState> GenerateStartState(List<Wagon> wagons, PuzzleConfig config)
    {
        var shuffled = wagons.OrderBy(_ => _random.Next()).ToList();
        var tracks = CreateEmptyTracks(config);

        // Only fill A, B, C — H starts empty
        var sidings = tracks.Where(t => t.TrackId != "H").ToList();

        foreach (var wagon in shuffled)
        {
            var availableTracks = sidings
                .Where(t => t.WagonIds.Count < t.Capacity)
                .ToList();

            if (availableTracks.Count != 0)
            {
                var track = availableTracks[_random.Next(availableTracks.Count)];
                track.WagonIds.Add(wagon.Id);
            }
        }

        return tracks;
    }

    private List<TrackState> GenerateGoalState(List<Wagon> wagons, PuzzleConfig config)
    {
        var tracks = CreateEmptyTracks(config);

        // Pick GoalWagons (5) in random order — THIS ORDER MATTERS
        var goalWagons = wagons
            .OrderBy(_ => _random.Next())
            .Take(config.GoalWagons)
            .ToList();

        // If a Brake Van is among the goal wagons, it must be last
        var brakeVan = goalWagons.FirstOrDefault(w =>
            w.WagonType?.Name.Equals("Brake Van", StringComparison.OrdinalIgnoreCase) == true);
        
        if (brakeVan is not null)
        {
            goalWagons.Remove(brakeVan);
            goalWagons.Add(brakeVan);
        }

        // These must end up on track A in exactly this order
        foreach (var wagon in goalWagons)
            tracks[0].WagonIds.Add(wagon.Id);

        // Remaining 3 wagons can be anywhere — we don't care where
        // PuzzleEngine validates only track A order

        return tracks;
    }

    private List<TrackState> CreateEmptyTracks(PuzzleConfig config) =>
        new()
        {
            new TrackState { TrackId = "A", Capacity = config.TrackACapacity },
            new TrackState { TrackId = "B", Capacity = config.TrackBCapacity },
            new TrackState { TrackId = "C", Capacity = config.TrackCCapacity },
            new TrackState { TrackId = "H", Capacity = config.HeadshuntCapacity }
        };
}
