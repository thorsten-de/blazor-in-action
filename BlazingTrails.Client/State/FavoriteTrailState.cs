using System;
using BlazingTrails.Client.Features.Home;
using Blazored.LocalStorage;

namespace BlazingTrails.Client.State;

/// <summary>
/// We want to remember the Users favorite trails, so the user can find them easily. This state should persist
/// even when the user closes the browser, so we will store it in the local storage of the browser.
/// </summary>
/// <param name="localStorageService">We inject the local storage service to access the local storage.</param>
public class FavoriteTrailState(ILocalStorageService localStorageService)
{
    private const string FavoriteTrailsKey = "favoriteTrails";
    private bool _isInitialized = false;
    private HashSet<Trail> _favoriteTrails = new(Trail.EqualityComparer);

    /// <summary>
    /// The OnChange event is triggered whenever the favorite trails change, so that the UI can update accordingly.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Readonly set of the user's favorite trails. We expose it as a read-only set 
    /// to prevent external code from modifying it directly.
    /// </summary>
    public IReadOnlySet<Trail> FavoriteTrails => _favoriteTrails.AsReadOnly();

    /// <summary>
    /// This method is called when the application starts, so the state can load the favorite trails from the local storage.
    /// </summary>
    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        _favoriteTrails = await localStorageService.GetItemAsync<HashSet<Trail>>(FavoriteTrailsKey) ?? new(Trail.EqualityComparer);
        _isInitialized = true;
        NotifyStateHasChanged();
    }

    /// <summary>
    /// Adds a trail to the user's favorite trails. If the trail is already in the favorites, it returns. After adding the trail,
    /// it saves the updated favorites to the local storage and triggers the OnChange event to update the UI.
    /// </summary>
    /// <param name="trail">The trail to be included as favorite</param>
    public async Task AddFavorite(Trail trail)
    {
        if (!_favoriteTrails.Add(trail)) return;

        await localStorageService.SetItemAsync(FavoriteTrailsKey, _favoriteTrails);
        NotifyStateHasChanged();
    }

    /// <summary>
    /// Removes a trail from the user's favorite trails. If the trail is not in the favorites, it returns. After removing the trail,
    /// it saves the updated favorites to the local storage and triggers the OnChange event to update the UI.
    /// </summary>
    /// <param name="trail">The trail to be removed from the favorites.</param>
    public async Task RemoveFavorite(Trail trail)
    {
        if (!_favoriteTrails.Remove(trail)) return;

        await localStorageService.SetItemAsync(FavoriteTrailsKey, _favoriteTrails);
        NotifyStateHasChanged();
    }

    /// <summary>
    /// Checks if a trail is in the user's favorite trails. This is used to determine if the "Add to Favorites" 
    /// or "Remove from Favorites" button should be shown for a trail.
    /// </summary>
    /// <param name="trail">The trail to check.</param>
    public bool IsFavorite(Trail trail) => _favoriteTrails.Contains(trail);

    private void NotifyStateHasChanged() => OnChange?.Invoke();
}
