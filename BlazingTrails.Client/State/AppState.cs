using System;

namespace BlazingTrails.Client.State;

/// <summary>
/// Handles all application state that is shared between multiple components and
/// should be preserved when navigating between pages
/// </summary>
public class AppState
{

    /// <summary>
    /// Holds unsaved trail data while the user is adding a new trail.
    /// </summary>
    public NewTrailState NewTrail { get; }

    public AppState()
    {
        NewTrail = new NewTrailState();
    }
}
