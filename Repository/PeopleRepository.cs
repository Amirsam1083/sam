namespace api.Repository;

public class PeopleRepository : IPeopleRepository
{
    private const string _collectionName = "peoples";
    private readonly IMongoCollection<Register>? _collection;

    public PeopleRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Register>(_collectionName);
    }
    public async Task<RegisterDto?> GetAll()
    {
        List<Register> registers = await _collection.Find<Register>(new BsonDocument()).ToListAsync();

        return null;
    }
    public async Task<RegisterDto?> GetByEmail(string userEmail)
    {
        Register register = await _collection.Find<Register>(user => user.Email == userEmail).FirstOrDefaultAsync();

        return null;
    }
    public async Task<UpdateResult> UpdateUserById(string userId, Register userIn)
    {
        var UpdateUser = Builders<Register>.Update
        .Set((Register doc) => doc.Age, userIn.Age)
        .Set(doc => doc.FirstName, userIn.FirstName);

        return await _collection.UpdateOneAsync<Register>(doc => doc.Id == userId, UpdateUser);
    }
    public async Task<DeleteResult> Delete(string userId)
    {
        return await _collection.DeleteOneAsync<Register>(doc => doc.Id == userId);
    }

}
