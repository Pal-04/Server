namespace Server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        // User can create multiple projects
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        // ProjectMember Relationship
        public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

        // user can create multiple task
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();

        // TaskAssignment Relation
        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();

        // Comment
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
