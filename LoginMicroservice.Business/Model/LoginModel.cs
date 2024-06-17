namespace LoginMicroservice.Business.Model
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // LoginResponse model
    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
