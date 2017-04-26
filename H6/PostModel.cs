using System;

namespace H6
{
    public class PostModel
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public DateTime SubmitDateTime { get; set; }
        public string Body { get; set; }
        public byte[] Image { get; set; }
    }
}