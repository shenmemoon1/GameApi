using GameStore.Api.EndPoints;
using GameStore.Api.Entities;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//refactoring
app.MapGamesEndpoints();
app.Run();
