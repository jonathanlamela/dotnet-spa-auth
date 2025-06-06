namespace DotNetSpaAuth.Dtos
{

    public record SigninRequest
    {
        public string Email { get; init; } = string.Empty;
        public string Firstname { get; init; } = string.Empty;
        public string Lastname { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }

}
