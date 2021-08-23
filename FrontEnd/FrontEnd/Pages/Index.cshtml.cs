using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DaprClient _daprClient;

    public IndexModel(ILogger<IndexModel> logger, DaprClient daprClient)
    {
        _logger = logger;
        this._daprClient = daprClient;
    }

    public async Task OnGet()
    {
        var envoygateway = DaprClient.CreateInvokeHttpClient("envoygateway");
        var requestUri = $"/c/Weatherforecast";
        var forecasts = await envoygateway.GetFromJsonAsync<IEnumerable<WeatherForecast>>(requestUri);
        //var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
        //    HttpMethod.Get,
        //    "envoygateway",
        //    "weatherforecast");
        ViewData["WeatherForecastData"] = forecasts; // new List<WeatherForecast>();

    }
}