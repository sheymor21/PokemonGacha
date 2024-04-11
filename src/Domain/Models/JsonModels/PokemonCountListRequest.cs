namespace Domain.Models.JsonModels;

public class Result
{
    public string name { get; set; }
    public string url { get; set; }
}

public class PokemonCountListRequest
{
    public int count { get; set; }
    public object next { get; set; }
    public object previous { get; set; }
    public List<Result> results { get; set; }
}