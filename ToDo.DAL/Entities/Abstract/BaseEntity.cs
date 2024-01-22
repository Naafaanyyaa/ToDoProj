namespace ToDo.DAL.Entities.Abstract;

public class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }

}