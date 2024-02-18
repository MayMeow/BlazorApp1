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

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
