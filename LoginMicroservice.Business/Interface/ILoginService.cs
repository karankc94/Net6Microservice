using LoginMicroservice.Business.Model;

namespace LoginMicroservice.Business.Interface
{
    // Interface for business logic
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
