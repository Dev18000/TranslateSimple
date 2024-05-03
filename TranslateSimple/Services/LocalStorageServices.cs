
using Microsoft.JSInterop;

namespace TranslateSimple.Services
{
    public class LocalStorageServices
    {
        private Lazy<IJSObjectReference> _accessorJsRef = new();
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageServices(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task WaitForReference()
        {
            if (_accessorJsRef.IsValueCreated is false)
            {
                _accessorJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/LocalStorageAccessor.js"));
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_accessorJsRef.IsValueCreated)
            {
                await _accessorJsRef.Value.DisposeAsync();
            }
        }

        public async Task SetSessionString(string title, string valeur)
        {
            try
            {
                await WaitForReference();
                await _accessorJsRef.Value.InvokeAsync<string>("set", title, valeur);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
