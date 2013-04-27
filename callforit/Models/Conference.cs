using System;

namespace callforit.Models
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string CreatorName { get; set; }
        public DateTime StartDate { get; set; }
    }
}