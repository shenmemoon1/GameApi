using GameStore.Api.Entities;

namespace GameStore.Api.EndPoints;

public static class GamesEndPoints
{
    const string GetGameEndPointName = "GetGames";
    static List<Game> games = new(){
    new Game(){
        Id = 1,
        Name = "League of legend",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(2000, 1,2),
        ImageUri = "https://placehold.co/100"
    },
    new Game(){
        Id = 2,
        Name = "MindCraft",
        Genre = "Casal",
        Price = 39.99M,
        ReleaseDate = new DateTime(2011, 1,2),
        ImageUri = "https://placehold.co/100"
    },
    new Game(){
        Id = 3,
        Name = "Espect Legend",
        Genre = "Fighting",
        Price = 89.99M,
        ReleaseDate = new DateTime(2006, 1,2),
        ImageUri = "https://placehold.co/100"
    },

    };

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        //使用mapgroup 更好的管理routes
        //WithParameterValidation 来之第三方库
        var gamegroup = routes.MapGroup("games").WithParameterValidation();

        //?get method
        // app.MapGet("/games", () => games);

        //?get 使用group的方式
        gamegroup.MapGet("/", () => games);

        gamegroup.MapGet("/{id}", (int id) =>
        {
            Game? game = games.Find(game => game.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game);
        })
        .WithName(GetGameEndPointName);


        //在ASP.NET Core中，WithName 的主要作用是为一个特定的路由端点（endpoint）指定一个易记的名字。
        //这个名字可以看作是给这个端点起的一个别名。
        //? Post
        gamegroup.MapPost("/", (Game game) =>
        {
            game.Id = games.Max(game => game.Id) + 1;
            games.Add(game);
            //在 Results.CreatedAtRoute 方法中，第一个参数是端点的名称（这里是 /games/{id} 路径的端点），而第二个参数是一个匿名对象，用于提供端点所需的路由参数。
            //在你的具体例子中，game.Id 的值会被用作 id 路由参数，形成了最终的资源URL，即 /games/4
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        //? Update put
        gamegroup.MapPut("/{id}", (int id, Game updateGame) =>
        {
            Game? existingGame = games.Find(game => game.Id == id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            existingGame.Name = updateGame.Name;
            existingGame.Price = updateGame.Price;
            existingGame.ImageUri = updateGame.ImageUri;
            existingGame.Genre = updateGame.Genre;
            existingGame.ReleaseDate = updateGame.ReleaseDate;

            return Results.NoContent();
        });

        //? delte endpoint
        gamegroup.MapDelete("/{id}", (int id) =>
        {
            Game? existingGame = games.Find(game => game.Id == id);
            if (existingGame is not null)
            {
                games.Remove(existingGame);
            }
            return Results.NoContent();
        });
        return gamegroup;
    }

}