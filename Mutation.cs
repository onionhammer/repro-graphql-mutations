namespace Repro.GraphQLMutations;

[MutationType]
public class Mutation
{
    /// <summary>
    /// Add a thingy
    /// </summary>
    public bool AddThingy(CouldBeInputThingInput thing)
    {
        return true;
    }
}

[OneOf]
public class CouldBeInputThingInput
{
    public Boat? Boat { get; set; }

    public Car? Car { get; set; }
}

public class BoatInput : InputObjectType<Boat>;

public class CarInput : InputObjectType<Car>;