var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Serve index.html automatically
app.UseDefaultFiles();
app.UseStaticFiles();

// Validation endpoint
app.MapPost("/validate", async (HttpContext ctx) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var values = form["cells"].ToString().Split(',');
    int[,] board = new int[9, 9];
    for (int i = 0; i < 81; i++) board[i / 9, i % 9] = int.Parse(values[i]);
    bool valid = SudokuValidator.IsValid(board);
    return Results.Json(new { valid });
});

// Solver endpoint
app.MapPost("/solve", async (HttpContext ctx) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var values = form["cells"].ToString().Split(',');
    int[,] board = new int[9, 9];
    for (int i = 0; i < 81; i++) board[i / 9, i % 9] = int.Parse(values[i]);
    bool solved = SudokuValidator.Solve(board);

    var result = new List<int>();
    for (int i = 0; i < 9; i++)
        for (int j = 0; j < 9; j++)
            result.Add(board[i, j]);

    return Results.Json(new { solved, board = result });
});

app.Run();
