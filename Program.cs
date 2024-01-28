using GameStore.Api.EndPoints;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
///当有地方需要使用 IGamesRepository 接口时，实际上要用的是 InMemGamesRepository 类的一个实例。
builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();
var app = builder.Build();
//refactoring
app.MapGamesEndpoints();
app.Run();


/*
当我们谈论 ASP.NET Core 中的依赖注入（DI）时，我们其实是在讨论如何管理不同部分（组件）之间的相互依赖关系。DI 帮助我们将组件的创建和使用解耦，使得代码更容易维护和测试。

这里有三种不同类型的服务（组件）生命周期，我们可以根据应用程序的需求来选择：

单例（Singleton）：

比喻： 就像是整个应用程序中的“老朋友”一样，只有一个实例，大家都认识。
适用场景： 用于那些在整个应用程序中都需要共享的服务，比如全局配置、缓存。
作用域（Scoped）：

比喻： 就像是每次请求时的“短期朋友”，这个朋友在请求期间和你共享信息，但请求结束后就消失了。
适用场景： 用于在一个 HTTP 请求处理期间共享信息的服务，比如数据库上下文，这些信息在请求期间保持一致。
瞬时（Transient）：

比喻： 就像是“一次性使用”的工具，每次都是新的实例，不会被共享。
适用场景： 用于需要短时间内创建多个实例的服务，比如某些计算、临时数据处理。
为什么要有这些不同的生命周期？

性能和资源： 单例可以提高性能，但可能会占用更多资源。瞬时则更轻量，但每次都要创建新实例。
共享与隔离： 单例适合全局共享，而作用域适合请求内共享。瞬时则在每次使用时都是独立的。
怎么选择？

如果你的服务在整个应用程序中都是共享的，并且创建它的代价较大，选择 Singleton。
如果你的服务在每个请求期间都需要共享，并且请求结束后就可以释放，选择 Scoped。
如果你的服务是短暂的、轻量级的，每次都需要一个新实例，选择 Transient。
通过选择适当的生命周期，我们可以更好地管理应用程序中的组件，使其具有更好的性能、资源利用率和适应性。
*/