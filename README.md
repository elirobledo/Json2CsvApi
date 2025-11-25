# JSON2CSV Project (Minimal .NET 8 API + Frontend)

## Estrutura
- `Json2CsvApi/Program.cs` - backend minimal API (.NET 8)
- `Json2CsvApi/Json2CsvApi.csproj` - projeto
- `Json2CsvApi/wwwroot/index.html` - frontend (serve estático)
- `Json2CsvApi/wwwroot/style.css`  - css
- `Json2CsvApi/wwwroot/script.js`  - javascript
- `README.md` - este arquivo

## Como rodar (requer .NET 8 SDK)
1. Abra um terminal na pasta `Json2CsvApi`.
2. Execute: `dotnet run`
3. Abra no navegador: `http://localhost:5000` http://localhost:58314/ (ou a porta exibida no terminal)

## Observações
- Conversão aceita apenas JSON como array de objetos simples.
- Nenhuma biblioteca externa foi usada para CSV; somente `System.Text.Json` para parse do JSON.
- A API também retorna `text/csv` para fácil consumo.
