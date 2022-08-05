namespace RestApi.Infrastructure.EF;

internal record PK(string Id);

internal record CompositePK(string Id, string AdditionalId) : PK(Id);
