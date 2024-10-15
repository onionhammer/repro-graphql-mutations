namespace Repro.GraphQLMutations;

[QueryType]
public class Query
{
    public BaseThing? GetThing(string? name) => name switch
    {
        "Car" => new Car { Name = name, Make = "Toyota" },
        "Boat" => new Boat { Name = name, Make = "Yamaha", Length = 10 },
        _ => null
    };
}


[InterfaceType]
public interface BaseThing
{
    public string? Name { get; }
}

[ObjectType]
public class Car : BaseThing
{
    public required string Name { get; set; }
    public required string Make { get; set; }
}

[ObjectType]
public class Boat : BaseThing
{
    public required string Name { get; set; }
    public required string Make { get; set; }
    public int Length { get; set; }
}