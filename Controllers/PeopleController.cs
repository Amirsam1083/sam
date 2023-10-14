using api.Interfaces;
using api.Repository;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase 
{
    private const string _collectionName = "peoples";
    private readonly IMongoCollection<Register> _collection;

    public PeopleController(IMongoClient client, IMongoDbSettings dbSettings, IPeopleRepository peopleRepository)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Register>(_collectionName);

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Register>>> GetAll()
    {
        List<Register> registers = await _collection.Find<Register>(new BsonDocument()).ToListAsync();

        if(!registers.Any())
            return NoContent();

        return registers;    
    } 

    [HttpGet("get-by-Email/{userEmail}")]
    public async Task<ActionResult<Register>> GetByEmail(string userEmail)
    {
        Register register = await _collection.Find<Register>(user => user.Email == userEmail).FirstOrDefaultAsync();

        if (register is null)
            return NotFound("kasi ba in id peyda nashod");
        
        return register;
    }

    [HttpPut("update/{userId}")]
    public async Task<ActionResult<UpdateResult>> UpdateUserById(string userId, Register userIn)
    {
        var UpdateUser = Builders<Register>.Update
        .Set((Register doc) => doc.Age, userIn.Age)
        .Set(doc => doc.FirstName, userIn.FirstName);

        return await _collection.UpdateOneAsync<Register>(doc => doc.Id == userId, UpdateUser);
    }

    [HttpDelete("delete/{userId}")]
    public async Task<ActionResult<DeleteResult>> Delete(string userId)
    {
        return await _collection.DeleteOneAsync<Register>(doc => doc.Id == userId);
    }
}