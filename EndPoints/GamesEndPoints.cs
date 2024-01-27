using GameStore.Api.Entities;
using GameStore.Api.Repositories;
namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{
    const string GetGameEndPointName = "GetGames";
    //!使用 this关键词 来延申 endpoints接口
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        //defin repository instance
        InMemGamesRepository repository = new();
        //使用mapgroup 更好的管理routes
        //WithParameterValidation 来之第三方库
        var gamegroup = routes.MapGroup("games").WithParameterValidation();

        //?get method
        // app.MapGet("/games", () => games);

        //?get 使用group的方式
        gamegroup.MapGet("/", () => repository.GetAll());

        gamegroup.MapGet("/{id}", (int id) =>
        {
            Game? game = repository.Get(id);
            return game is null ? Results.NotFound() : Results.Ok(game);

        })
        .WithName(GetGameEndPointName);


        //在ASP.NET Core中，WithName 的主要作用是为一个特定的路由端点（endpoint）指定一个易记的名字。
        //这个名字可以看作是给这个端点起的一个别名。
        //? Post
        gamegroup.MapPost("/", (Game game) =>
            {
                repository.Create(game);
                //在 Results.CreatedAtRoute 方法中，第一个参数是端点的名称（这里是 /games/{id} 路径的端点），而第二个参数是一个匿名对象，用于提供端点所需的路由参数。
                //在你的具体例子中，game.Id 的值会被用作 id 路由参数，形成了最终的资源URL，即 /games/4
                return Results.CreatedAtRoute(GetGameEndPointName, new
                {
                    id = game.Id
                }, game);
            });

        //? Update put
        gamegroup.MapPut("/{id}", (int id, Game updateGame) =>
        {
            Game? existingGame = repository.Get(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            existingGame.Name = updateGame.Name;
            existingGame.Price = updateGame.Price;
            existingGame.ImageUri = updateGame.ImageUri;
            existingGame.Genre = updateGame.Genre;
            existingGame.ReleaseDate = updateGame.ReleaseDate;

            repository.Update(updateGame);

            return Results.NoContent();
        });

        //? delte endpoint
        gamegroup.MapDelete("/{id}", (int id) =>
        {
            Game? existingGame = repository.Get(id);
            if (existingGame is not null)
            {
                repository.Delete(id);
            }
            return Results.NoContent();
        });
        return gamegroup;
    }

}