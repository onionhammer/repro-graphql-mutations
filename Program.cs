var builder = WebApplication.CreateBuilder(args);

var graphql = builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddMutationConventions()
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
