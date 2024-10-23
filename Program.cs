var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

var graphql = builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddMutationConventions()
    .AddAuthorization()
    .ModifyOptions(static o => 
    {
        // o.DefaultBindingBehavior = BindingBehavior.Explicit;
        // TODO: Enable OneOf
        o.EnableOneOf = true;
    })
    ;

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// GraphQL index
app.MapGraphQL();

app.Run();
