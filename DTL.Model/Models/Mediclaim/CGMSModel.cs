namespace DTL.Model.Models.Mediclaim
{
    public class CGMSModel
    {
        public int Id { get; set; }
        public string Treatment { get; set; }
        public decimal NonNABHNABLRate { get; set; }
        public decimal NABHNABLRate { get; set; }
    }
}
