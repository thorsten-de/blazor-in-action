using System.Runtime.CompilerServices;
using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails;

public class TrailDto
{
    public int Id { get; private set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public int TimeInMinutes { get; set; }
    public int Length { get; set; }
    public List<RouteInstruction> Route { get; init; } = [];

    /// <summary>
    /// Filename of the optional image
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Is there any operation performed on the image? 
    /// </summary>
    public ImageAction ImageAction { get; set; } = ImageAction.None;

    public class RouteInstruction
    {
        public int Stage { get; set; }
        public string Description { get; set; } = "";
    }
}

public enum ImageAction
{
    None,
    Add,
    Remove
}

/// <summary>
/// Validates Trails
/// </summary>
public class TrailValidator : AbstractValidator<TrailDto>
{
    public TrailValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
        RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length.");
        RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithMessage("Please enter the time for hiking the trail.");
        RuleFor(x => x.Route).NotEmpty().WithMessage("Please add a route instruction");

        // Use RouteInstructionValidator to validate every route instruction in our trail
        RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator());
    }
}

/// <summary>
/// Validates RouteInstructions
/// </summary>
public class RouteInstructionValidator : AbstractValidator<TrailDto.RouteInstruction>
{
    public RouteInstructionValidator()
    {
        RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
    }
}