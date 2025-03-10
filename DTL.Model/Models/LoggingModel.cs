using System;

namespace DTL.Model.Models
{
    public class LoggingModel
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string ErrorText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
