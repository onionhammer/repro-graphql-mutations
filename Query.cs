using HotChocolate.Authorization;
using HotChocolate.Resolvers;

namespace Repro.GraphQLMutations;

[QueryType]
public class Query
{
    public string Hello => "World";

    [Authorize("Partner")]
    public Partner GetPartner(IResolverContext context, string partnerId)
    {

        return new Partner();
    }
}

public class Partner
{
    public string Name => "Partner";

    [Authorize("Partner")]
    public Lead GetLead()
    {
        return new Lead();
    }
}

public class Lead
{
    public string GetPartnerName([ScopedState("PartnerId")] string id)
    {
        return $"Partner {id}";
    }
}