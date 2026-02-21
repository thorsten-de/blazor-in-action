using System;
using BlazingTrails.Shared.Features.ManageTrails;

namespace BlazingTrails.Client.State;

/// <summary>
/// Remembers unsaved data for a trail while the user is adding it.
/// </summary>
public class AppState
{
    private TrailDto _unsavedTrail = new();

    /// <summary>
    /// Unsaved trail data.
    /// </summary>
    public TrailDto Trail
    {
        get => _unsavedTrail;
        set
        {
            _unsavedTrail = value;
        }
    }

    /// <summary>
    /// Clears the unsaved trail data.
    /// </summary>
    public void ClearTrail()
        => _unsavedTrail = new TrailDto();
}
