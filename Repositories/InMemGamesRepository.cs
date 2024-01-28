using GameStore.Api.Entities;
namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
  /*
  Repository Pattern（仓储模式）是一种软件设计模式，通常用于数据存取层（Data Access Layer）。
  该模式旨在将数据访问逻辑从业务逻辑中分离出来，提供了一种方式来组织和管理应用程序的数据访问代码。

  在 Repository Pattern 中，每个实体类（或聚合根）都有一个相应的仓储（Repository）。
  仓储是负责处理该实体类在数据存储中的增、删、改、查等操作的类。仓储层充当业务逻辑和数据库之间的中间层，
  隐藏了底层数据访问细节，使得业务逻辑层能够专注于业务规则而不用关心具体的数据存储实现。

  Repository Pattern 的主要优点包括：

  抽象数据访问逻辑： 通过使用 Repository Pattern，你可以将数据访问逻辑抽象出来，
  使得业务逻辑与具体的数据存储实现解耦。这样，你可以更容易地更换或切换底层的数据存储，而不会影响到业务逻辑。

  单一职责原则： 仓储层的存在使得每个类都有一个明确的职责。业务逻辑负责业务规则，而仓储负责数据访问。

  测试性： 通过使用 Repository Pattern，你可以更容易地进行单元测试，
  因为你可以使用模拟对象或者内存中的存储来测试业务逻辑，而不需要依赖于真实的数据库
  */
  private readonly List<Game> games = new(){
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

  //get all games
  public IEnumerable<Game> GetAll()
  {
    return games;
  }


  //get a game by id
  public Game? Get(int id)
  {
    return games.Find(game => game.Id == id);
  }

  //create a new game
  public void Create(Game game)
  {
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);
  }

  //update the game
  public void Update(Game updateGame)
  {
    int index = games.FindIndex(game => game.Id == updateGame.Id);
    games[index] = updateGame;
  }

  //delete the game
  public void Delete(int id)
  {
    int index = games.FindIndex(game => game.Id == id);
    games.RemoveAt(index);
  }
}