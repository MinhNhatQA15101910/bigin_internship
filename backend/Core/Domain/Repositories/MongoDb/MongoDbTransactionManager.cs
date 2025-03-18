using MongoDB.Driver;

namespace Domain.Repositories.MongoDb;

public class MongoDbTransactionManager(IMongoClient mongoClient)
{
    public async Task ExecuteTransactionAsync(Func<IClientSessionHandle, Task> transactionBody)
    {
        using var session = await mongoClient.StartSessionAsync();
        session.StartTransaction();

        try
        {
            await transactionBody(session);
            await session.CommitTransactionAsync();
            Console.WriteLine("Transaction committed successfully.");
        }
        catch (Exception ex)
        {
            await session.AbortTransactionAsync();
            Console.WriteLine("Transaction aborted due to error: " + ex.Message);
            throw;
        }
    }
}
