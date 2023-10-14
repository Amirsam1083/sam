// using ZstdSharp.Unsafe;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterRepository _registerRepository;
    // private LoginDto userInput;

    public RegisterController(IRegisterRepository registerRepository)
    {
        _registerRepository = registerRepository;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<PeopleDto>> Create(RegisterDto userInput, CancellationToken cancellationToken)
    {
        PeopleDto? peopleDto = await _registerRepository.Register(userInput, cancellationToken);

        if (peopleDto is null)
            return BadRequest("gerefte shode");
            
        return peopleDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<PeopleDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        PeopleDto? peopleDto = await _registerRepository.Login(userInput, cancellationToken);

        if (peopleDto is null)
            return Unauthorized("Wrong username or password");

        return peopleDto;
    }
}

    // [HttpGet("get-by-codemelli/{userInput}")]
    // public ActionResult<Register> Get(int userInput)
    // {
    //     Register register = _collection.Find(register => register.CodeMelli == userInput)

    //     if (register == null)
    //     {
    //         return NotFound("kasi ba in codemelli peyda nashod");
    //     }
    //     return register;
    // }
    
    // [HttpGet("get-all")]
    // public ActionResult<IEnumerable<Register>> GetAll()
    // {
    //     List<Register> registers = _collection.Find<Register>(new BsonDocument()).ToList();

    //     if(!registers.Any())
    //         return NoContent();

    //     return registers;    
    // } 

    // [HttpPut("update/userInput")]
    // public ActionResult<UpdateResult> UpdateRegisterById(string userId, Register userIn)
    // {
    //     var UpdateRegister = Builders<Register>.Update
    //     .Set((Register doc) => doc.Age, userIn.Age)
    //     .Set(doc => doc.FirstName, userIn.FirstName);

    //     return _collection.UpdateOne<Register>(doc => doc.Id == userId, UpdateRegister);
    // }

//     [HttpDelete("delete/{userId}")]
//     public ActionResult<DeleteResult> Delete(string userId)
//     {
//         return _collection.DeleteOne<Register>(doc => doc.Id == userId);
//     }
// }
  