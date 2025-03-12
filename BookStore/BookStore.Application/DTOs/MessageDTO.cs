namespace BookStore.Application.DTOs
{
    public class MessageDTO
    {
        public Guid UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
