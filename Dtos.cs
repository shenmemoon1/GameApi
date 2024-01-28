using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;
/*
这个Product类是领域模型，包含了产品的详细信息，包括价格、名称等。
现在，你需要在前端展示一个产品列表，但你不想把整个Product对象传递给前端，因为它包含了很多前端不需要的信息。这时，
你可以使用DTO来简化数据传输，只传递前端所需的信息
*/

public record GameDto(
  int Id,
  string Name,
  string Genre,
  decimal Price,
  DateTime ReleaseDate,
  string ImageUri
);

public record CreateGameDto(
  [Required][StringLength(50)] string Name,
  [Required][StringLength(20)] string Genre,
  [Required][Range(1, 100)] decimal Price,
  DateTime ReleaseDate,
  [Url][StringLength(100)] string ImageUri
);

public record UpdateGameDto(
  [Required][StringLength(50)] string Name,
  [Required][StringLength(20)] string Genre,
  [Required][Range(1, 100)] decimal Price,
  DateTime ReleaseDate,
  [Url][StringLength(100)] string ImageUri
);