namespace API.Responses
{
    public class UserResponse
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public required string? Email { get; set; }
        public string? Picture { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
