namespace Server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        // User can create multiple projects
        public ICollection<Project> ? Projects { get; set; }

        // ProjectMember Relationship
        public ICollection<ProjectMember> ? ProjectMembers { get; set; }

        // user can create multiple task
        public ICollection<TaskItem> ? TaskItems { get; set; }

        // TaskAssignment Relation
        public ICollection<TaskAssignment> ? Assignments { get; set; }

        // Comment
        public ICollection<Comment> ? Comments { get; set; }

        // UserRoles
        public ICollection<UserRole> ? UserRoles { get; set; }
    }
}
