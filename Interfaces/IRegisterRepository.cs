namespace api.Interfaces;

public interface IRegisterRepository
{
    // public Task<PeopleDto?> Create(RegisterDto userInput, CancellationToken cancellationToken);
    public Task<PeopleDto?> Register(RegisterDto userInput,CancellationToken cancellationToken);

    public Task<PeopleDto?> Login(LoginDto userInput, CancellationToken cancellationToken);
    // Task<PeopleDto> Login(LoginDto userInput, object cancellationToken);
    // Task<PeopleDto?> Login(object userInput, CancellationToken cancellationToken);
}
