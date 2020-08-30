using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts;
using Virtualcompanion.Core.Contexts.Extensibility;
using VirtualCompanion.Core.Http.Serialization.Json;

namespace VirtualCompanion.Core.Http.Contexts.Extensibility
{
    public class HttpVirtualCompanionExecutionContextProcessorHandlerBase : VirtualCompanionExecutionContextProcessorHandlerBase
    {
        private readonly HttpClient _httpClient;

        private readonly IVirtualCompanionExecutionContextSerializer _serializer;

        public HttpVirtualCompanionExecutionContextProcessorHandlerBase(HttpClient httpClient, IVirtualCompanionExecutionContextSerializer serializer)
        {
            _httpClient = httpClient;
            _serializer = serializer;
        }

        public override async Task HandleVirtualCompanionExecutionContextAsync(IVirtualCompanionExecutionContext context, Func<Task> next)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "/event"))
                {
                    var requesBody = await _serializer.SerializeAsync(context);
                    request.Content = new StringContent(requesBody, Encoding.UTF8);

                    using (var response = await _httpClient.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseBody = await response.Content.ReadAsStringAsync();
                        var newContext = await _serializer.DeserializeAsync(responseBody);

                        foreach (var kvp in newContext)
                        {
                            context[kvp.Key] = kvp.Value;
                        }
                    }
                }
            }

            await next();
        }
    }
}
