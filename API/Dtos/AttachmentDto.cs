using System;

namespace API.Dtos
{
    public class AttachmentDto
    {
        public required string Id { get; set; }

        public required string Url { get; set; }

        public DateTime UploadedAt { get; set; }

    }
}
