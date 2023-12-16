using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using Imager.Dapr.S3.Interfaces;
using Imager.Dapr.S3.Models;
using Imager.Dapr.S3.Responses;

using Throw;

namespace Imager.Dapr.S3.Implementations;

public class DaprObjectStore : IObjectStore
{
    protected readonly Uri _daprUri;
    protected readonly HttpClient _client;

    public DaprObjectStore(int daprPort, string bindingName, HttpClient client)
    {
        bindingName.ThrowIfNull();
        client.ThrowIfNull();
        _daprUri = new Uri($"http://localhost:{daprPort}/v1.0/bindings/{bindingName}");
        _client = client;

    }

    public virtual async Task<CreateObjectResponse> CreateObjectAsync<TObject>(
        ObjectStoreKey key,
        TObject data,
        string? presignTTL = null,
        CancellationToken cancellationToken = default)
        where TObject : notnull
    {
        data.ThrowIfNull();
        key.ThrowIfNull();

        var operation = "create";
        var dataBytes = data switch
        {
            byte[] bytes => bytes,
            string str => Encoding.Default.GetBytes(str),
            _ => Encoding.Default.GetBytes(JsonSerializer.Serialize(data))
        };
        var metadata = new { key = key.ToString(), presignTTL };

        var body = new { operation, data = Convert.ToBase64String(dataBytes), metadata };

        var response = await _client.PostAsJsonAsync(_daprUri, body, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CreateObjectResponse>(cancellationToken)
            .ConfigureAwait(false);

        return result.ThrowIfNull();
    }

    public virtual async Task DeleteObjectAsync(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default)
    {
        key.ThrowIfNull();

        var operation = "delete";
        var metadata = new { key = key.ToString() };

        var body = new { operation, metadata };

        var response = await _client.PostAsJsonAsync(_daprUri, body, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }

    public virtual async Task<ObjectData<TObject>?> GetObjectAsync<TObject>(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default)
        where TObject : notnull
    {
        key.ThrowIfNull();

        var operation = "get";
        var metadata = new { key = key.ToString() };

        var body = new { operation, metadata };

        var response = await _client.PostAsJsonAsync(_daprUri, body, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TObject>(cancellationToken).ConfigureAwait(false);
        if (result is null) return null;
        return new ObjectData<TObject>(key, result);
    }

    public async Task<bool> IsObjectExistsAsync(
        ObjectStoreKey key,
        CancellationToken cancellationToken = default)
    {
        key.ThrowIfNull();

        var operation = "get";
        var metadata = new { key = key.ToString() };

        var body = new { operation, metadata };

        var response = await _client.PostAsJsonAsync(_daprUri, body, cancellationToken).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }


    public virtual async Task<ListObjectsResponse> ListObjectsAsync(
        int maxResults = 1000,
        string? prefix = null,
        string? delimiter = null,
        string? marker = null,
        CancellationToken cancellationToken = default)
    {
        var operation = "list";
        var data = new { maxResults, prefix, marker, delimiter };

        var body = new { operation, data };

        var response = await _client.PostAsJsonAsync(_daprUri, body, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ListObjectsResponse>(cancellationToken).ConfigureAwait(false);
        return result.ThrowIfNull();
    }

    public virtual async Task<PresignObjectResponse> PresignObjectAsync(
        ObjectStoreKey key,
        string presignTTL,
        CancellationToken cancellationToken = default)
    {
        key.ThrowIfNull();

        var operation = "presign";
        var metadata = new
        {
            presignTTL,
            key = key.ToString()
        };

        var body = new { operation, metadata };

        var response = await _client.PostAsJsonAsync(_daprUri, body, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PresignObjectResponse>(cancellationToken).ConfigureAwait(false);
        return result.ThrowIfNull();
    }
}