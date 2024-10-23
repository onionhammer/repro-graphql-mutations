using HotChocolate.Resolvers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(c =>
{
    c.AddPolicy("Partner", builder =>
    {
        builder.RequireAssertion(context =>
        {
            if (context.Resource is IMiddlewareContext middlewareContext)
            {
                if (middlewareContext.GetScopedStateOrDefault<string>("PartnerId") is {} partnerIdStr)
                {
                    return true;
                }

                if (middlewareContext.ArgumentValue<string>("partnerId") is {} partnerId)
                {
                    // Add to scope
                    middlewareContext.SetScopedState("PartnerId", partnerId);

                    return true;
                }

                return true;
            }

            return false;
        });
    });
});

var graphql = builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddMutationConventions()
    .AddAuthorization()
    .AddHttpRequestInterceptor<ClaimsRequestInterceptor>()
    .ModifyOptions(static o => 
    {
        // o.DefaultBindingBehavior = BindingBehavior.Explicit;
        // TODO: Enable OneOf
        o.EnableOneOf = true;
    })
    .ModifyRequestOptions(o => o.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    ;

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// GraphQL index
app.MapGraphQL();

app.Run();
