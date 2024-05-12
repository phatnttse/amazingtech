namespace API.Dtos
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Active { get; set; }
        public string? Token { get; set; }
    }
}
