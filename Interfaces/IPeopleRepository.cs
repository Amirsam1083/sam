namespace api.Interfaces;

public interface IPeopleRepository
{
    public Task<RegisterDto?> GetAll();
    public Task<RegisterDto?> GetByEmail(string userEmail);
    public Task<DeleteResult> Delete(string userId);
    public Task<UpdateResult> UpdateUserById(string userId, Register userIn);
}
