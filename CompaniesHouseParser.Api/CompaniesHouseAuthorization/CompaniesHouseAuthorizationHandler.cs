using CompaniesHouseParser.Settings;
using System.Net.Http.Headers;
using System.Text;

namespace CompaniesHouseParser.Api
{
    //Тип для обработчиков HTTP-данных, которые делегируют обработку ответных
    //сообщений HTTP другому обработчику, который называется внутренним обработчиком.
    public class CompaniesHouseAuthorizationHandler : DelegatingHandler                                                               
    {
        //Отправляет HTTP-запрос внутреннему обработчику для отправки на сервер в качестве асинхронной операции.
        protected override async Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, CancellationToken cancellationToken) 
        {
            var accessorAppSettings = new ApplicationSettingsAccessor();
            var apiToken = accessorAppSettings.Get().CompaniesHouseApi.Token;
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Base64Encode(apiToken));

            return await base.SendAsync(request, cancellationToken);
        }

        private string Base64Encode(string apiToken)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(apiToken));
        }
    }
}
