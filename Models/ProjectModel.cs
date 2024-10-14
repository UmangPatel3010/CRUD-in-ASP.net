namespace StaticCRUD.Models
{
    public class ProjectModel
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public double budget { get; set; }
    }
}
