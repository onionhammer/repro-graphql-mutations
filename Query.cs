using HotChocolate.Authorization;
using HotChocolate.Resolvers;

namespace Repro.GraphQLMutations;

[QueryType]
public class Query
{
    public string Hello => "World";

    public Partner GetPartner(IResolverContext context)
    {
        var id = new Guid("8515658c-e431-48c0-afab-eeee66ad5d59");
        context.SetScopedState("PartnerId", id);

        return new Partner();
    }
}

public class Partner
{
    public string Name => "Partner";

    [Authorize("Partner")]
    public string GetSomething([ScopedState("PartnerId")] Guid id)
    {
        return id.ToString();
    }
}