namespace BackendFarmersApp.WebApi.Models;

public class FarmersAppDatabaseSettings
{
    public string ConnectionString { get; init; } = null!;
    public string DatabaseName { get; init; } = null!;
    public string UsersCollectionName { get; init; } = null!;
    public string ProductsCollectionName { get; init; } = null!;
    public string OrdersCollectionName { get; init; } = null!;
    public string OrderDetailsCollectionName { get; init; } = null!;
}