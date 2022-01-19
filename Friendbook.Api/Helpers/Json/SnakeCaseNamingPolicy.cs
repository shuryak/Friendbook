using System.Text.Json;

namespace Friendbook.Api.Helpers.Json;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.ToSnakeCase();
}
