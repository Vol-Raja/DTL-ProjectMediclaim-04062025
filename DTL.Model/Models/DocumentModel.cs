namespace DTL.Model.Models
{
    public class DocumentModel : BaseModel
    {
        public int DocumentId { get; set; }
        public string DocumentPath { get; set; }
        public string ApplicationArea { get; set; }
        public string ApplicationSubArea { get; set; }
        public int ReferenceId { get; set; }
        public string DocumentFor { get; set; }
    }
}
