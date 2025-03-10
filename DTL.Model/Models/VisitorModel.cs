using System;

namespace DTL.Model.Models
{
    public class VisitorModel
    {
        public Guid Id { get; set; }
        public DateTime VisitDate { get; set; }

        public string Note { get; set; }

        public int CurrentMonthVisitorCount { get; set; }
        public int TodayVisitorCount { get; set; }
        public int TotalVisitorCount { get; set; }

    }
}
