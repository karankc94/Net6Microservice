using LoginMicroservice.Data.Model;

namespace LoginMicroservice.Data.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
