using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace BlazorApp3.Client
{
    public class IndexedDbAccessor : IAsyncDisposable
    {

        private Lazy<IJSObjectReference> _jSObjectReference = new();
        private readonly IJSRuntime _jsRuntime;

        public IndexedDbAccessor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            await WaitForReference();
            await _jSObjectReference.Value.InvokeVoidAsync("initialize");
        }

        private async Task WaitForReference()
        {
            if (_jSObjectReference.IsValueCreated is false)
            {
                _jSObjectReference = new(await _jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "/indexedDbStorageAccessor.js"));
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_jSObjectReference.IsValueCreated)
            {
                await _jSObjectReference.Value.DisposeAsync();
            }
        }

        public async Task<T> GetValueAsync<T>(string collectionName, int id)
        {
            await WaitForReference();

            var result = await _jSObjectReference.Value.InvokeAsync<T>("pull", collectionName, id);

            return result;
        }

        public async Task SetValueAsync<T>(string collectionName, T valuName)
        {
            await WaitForReference();
            await _jSObjectReference.Value.InvokeVoidAsync("put", collectionName, valuName);
        }

        public async Task<bool> CheckKeyExistsAsync(string collectionName, int id)
        {
            await WaitForReference();

            var result = await _jSObjectReference.Value.InvokeAsync<bool>("hasKey", collectionName, id);

            return result;
        }
    }
}
