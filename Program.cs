using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Habilitar arquivos estáticos
builder.Services.AddDirectoryBrowser();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JSON2CSV API", Version = "v1" });
});

var app = builder.Build();

// Servir arquivos estáticos (frontend)
app.UseDefaultFiles(); // procura index.html automaticamente
app.UseStaticFiles();  // wwwroot

// Swagger opcional
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/converter", async (HttpContext http) =>
{
    using var reader = new StreamReader(http.Request.Body);
    var json = await reader.ReadToEndAsync();

    if (string.IsNullOrWhiteSpace(json))
        return Results.BadRequest("JSON vazio.");

    try
    {
        var jsonArray =
            System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

        if (jsonArray is null || jsonArray.Count == 0)
            return Results.BadRequest("JSON inválido.");

        var headers = jsonArray
            .SelectMany(d => d.Keys)
            .Distinct()
            .ToList();

        var sb = new StringBuilder();
        sb.AppendLine(string.Join(",", headers));

        foreach (var item in jsonArray)
        {
            var line = headers
                .Select(h => item.ContainsKey(h) ? item[h]?.ToString() : "")
                .ToArray();

            sb.AppendLine(string.Join(",", line));
        }

        return Results.Ok(sb.ToString());
    }
    catch
    {
        return Results.BadRequest("JSON inválido.");
    }
});

app.Run();
