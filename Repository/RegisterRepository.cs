using api.Interfaces;
using api.Models;
namespace api.Repository;

public class RegisterRepository : IRegisterRepository
{
    private const string _collectionName = "peoples";
    private readonly IMongoCollection<Register>? _collection;

    public RegisterRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<Register>(_collectionName);
    }

    public async Task<PeopleDto?> Register(RegisterDto userInput, CancellationToken cancellationToken)
    {

        bool doesExist = await _collection.Find<Register>(user => user.CodeMelli == userInput.CodeMelli).AnyAsync(cancellationToken);

        if (doesExist)
            return null;

        Register register = new Register(
            Id: null,
            CodeMelli: userInput.CodeMelli,
            FirstName: userInput.FirstName,
            LastName: userInput.LastName,
            Age: userInput.Age,
            Password: userInput.Password,
            Email: userInput.Email
        );

        if (_collection is not null)
            await _collection.InsertOneAsync(register, null, cancellationToken);

        if (register.Id is not null)
        {
            PeopleDto peopleDto = new PeopleDto(
                Id: register.Id,
                Email: register.Email,
                CodeMelli: register.CodeMelli
            );
            return peopleDto;
        }
        return null;
    }

    public async Task<PeopleDto?> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        Register register = await _collection.Find<Register>(user =>
            user.Email == userInput.Email.ToLower().Trim()
            && user.Password == userInput.Password).FirstOrDefaultAsync(cancellationToken);

        if (register is null)
            return null;

        if (register is not null)
        {
            PeopleDto peopleDto = new PeopleDto(
                Id: register.Id,
                Email: register.Email,
                CodeMelli: register.CodeMelli
            );
            return peopleDto;
        }

        return null;
    }

    // public Task<RegisterDto?> Create(CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<PeopleDto?> Create(RegisterDto userInput, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<PeopleDto> Login(LoginDto userInput, object cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }

    // public Task<PeopleDto?> Login(object userInput, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }
}



