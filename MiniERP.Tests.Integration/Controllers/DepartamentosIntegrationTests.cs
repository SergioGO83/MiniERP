using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MiniERP.Tests.Integration.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniERP.Tests.Integration.Controllers;

public class DepartamentosIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DepartamentosIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "POST /Departamentos/Create (form) -> 302 Redirect a Index")]
    public async Task Post_Create_Form_ReturnsRedirect()
    {
        // 1) GET del formulario para obtener token + cookie
        var getResp = await _client.GetAsync("/Departamentos/Create");
        getResp.StatusCode.Should().Be(HttpStatusCode.OK);
        var (cookie, token) = await ExtractAntiforgeryAsync(getResp);

        // 2) Preparar el form (incluye el token con el nombre que genera TagHelper)
        var form = new Dictionary<string, string?>
        {
            ["__RequestVerificationToken"] = token,
            ["Nombre"] = "Innovación"
        };

        var content = new FormUrlEncodedContent(form);

        // 3) Enviar cookie de antiforgery
        _client.DefaultRequestHeaders.Remove("Cookie");
        _client.DefaultRequestHeaders.Add("Cookie", cookie);

        // 4) POST
        var resp = await _client.PostAsync("/Departamentos/Create", content);

        resp.StatusCode.Should().Be(HttpStatusCode.Found);
        resp.Headers.Location!.ToString().Should().Contain("/Departamentos/Index");
    }

    private static async Task<(string cookie, string token)> ExtractAntiforgeryAsync(HttpResponseMessage getResponse)
    {
        // Cookie de antiforgery viene en Set-Cookie; la reenviamos como "Cookie" entera
        var setCookie = getResponse.Headers.GetValues("Set-Cookie")
            .First(h => h.Contains(".AspNetCore.Antiforgery", StringComparison.OrdinalIgnoreCase));
        var cookiePair = setCookie.Split(';')[0]; // nombre=valor
        var cookieHeader = cookiePair;            // lo reenviamos tal cual

        // Token oculto en el formulario
        var html = await getResponse.Content.ReadAsStringAsync();
        var match = Regex.Match(
            html,
            @"<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)""",
            RegexOptions.IgnoreCase);

        if (!match.Success)
            throw new InvalidOperationException("No se encontró __RequestVerificationToken en la página Create.");

        var token = match.Groups[1].Value;
        return (cookieHeader, token);
    }
}
