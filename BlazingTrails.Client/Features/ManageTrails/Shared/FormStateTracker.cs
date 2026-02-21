using System;
using BlazingTrails.Client.Features.Home;
using BlazingTrails.Client.State;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

/// <summary>
/// Tracks the state of the form, so that we can warn the user if they try to navigate away with unsaved changes.
/// </summary>
/// <remarks>This is a component without any markup, so we can define it in code by subclassing ComponentBase.</remarks>
public class FormStateTracker : ComponentBase
{
    /// <summary>
    /// The application state holding the unsaved trail data. We inject it from the DI Container,
    /// and this is done by using property injection with blazor components.
    /// </summary>
    [Inject]
    public AppState AppState { get; set; } = null!;

    /// <summary>
    /// We use the EditContext to track the state of the form. The EditContext is created by the EditForm component,
    /// and we can access it by using the CascadingParameter attribute, which allows us to receive data from a parent component.
    /// </summary>
    [CascadingParameter]
    private EditContext? CascadedEditContext { get; set; }

    /// <summary>
    /// When the component is initialized, we subscribe to the OnFieldChanged event of the EditContext, so that we can update the
    /// AppState with the unsaved trail data whenever a field changes.
    /// </summary>
    /// <exception cref="InvalidOperationException">When there is no cascading EditContext, we throw an exception</exception>
    protected override void OnInitialized()
    {
        if (CascadedEditContext is null)
        {
            throw new InvalidOperationException($"{nameof(FormStateTracker)} requires a cascading parameter of type {nameof(EditContext)}. " +
                $"For example, you can use {nameof(FormStateTracker)} inside an {nameof(EditForm)}.");
        }
        CascadedEditContext.OnFieldChanged += CascadedEditContext_OnFieldChanged;
    }

    /// <summary>
    /// When a field changes, we check if the model of the field is a TrailDto and if its Id is 0 (which means it's a new 
    /// trail that hasn't been saved yet). If so, we update the AppState with the unsaved trail data.
    /// </summary>
    private void CascadedEditContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (e.FieldIdentifier.Model is TrailDto trail && trail.Id == 0)
        {
            AppState.NewTrail.Trail = trail;
        }
    }
}
