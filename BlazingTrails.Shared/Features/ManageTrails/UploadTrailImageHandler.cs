using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails;

public class UploadTrailImageHandler(HttpClient httpClient) : IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
{
    public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
    {
        var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken);

        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(fileContent), "image", request.File.Name);

        var response = await httpClient
            .PostAsync(UploadTrailImageRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()),
                content, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return new UploadTrailImageRequest.Response("");

        var fileName = await response.Content.ReadAsStringAsync(cancellationToken);
        return new UploadTrailImageRequest.Response(fileName);
    }
}
