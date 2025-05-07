using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.API.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public ContentResult Index()
    {
        var html = @"<html><head><meta charset='UTF-8'><title>API - CleanArchitecture</title></head><body style='font-family:sans-serif;text-align:center;margin-top:10%'>
        <h1>&#128640; API REST - CleanArchitecture</h1>
        <p>Este URL es solo para llamadas a <b>API</b>.<br>No es una p&aacute;gina web p&uacute;blica.</p>
        <p>Consulta la documentaci&oacute;n de la API o usa herramientas como Postman o Swagger para interactuar.</p>
        </body></html>";
        return new ContentResult { Content = html, ContentType = "text/html; charset=utf-8" };
    }
} 