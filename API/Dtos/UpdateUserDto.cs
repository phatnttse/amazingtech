namespace API.Dtos
{
    public class UpdateUserDto
    {
        public UpdateUserDto()
        {
            
        }

        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Picture { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
