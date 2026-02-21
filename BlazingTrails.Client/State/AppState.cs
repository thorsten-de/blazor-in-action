using System;
using Blazored.LocalStorage;

namespace BlazingTrails.Client.State;

/// <summary>
/// Handles all application state that is shared between multiple components and
/// should be preserved when navigating between pages
/// </summary>
/// <param name="localStorageService">We inject the local storage service to access the local storage.</param>
public class AppState(ILocalStorageService localStorageService)
{
    private bool _isInitialized = false;

    /// <summary>
    /// Holds unsaved trail data while the user is adding a new trail.
    /// </summary>
    public NewTrailState NewTrail { get; } = new();

    /// <summary>
    /// Holds the user's favorite trails. This state is persisted in the local storage of the browser, so it is preserved even when the user closes the browser.
    /// </summary>
    public FavoriteTrailState FavoriteTrails { get; } = new(localStorageService);

    /// <summary>
    /// Initializes the application state. This method propagates the initialization to the individual states, so they can load any necessary data (e.g. from local storage).
    /// This method should be called when the application starts. If the state is already initialized, it returns immediately.
    /// </summary>
    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        await FavoriteTrails.InitializeAsync();
        _isInitialized = true;
    }
}
