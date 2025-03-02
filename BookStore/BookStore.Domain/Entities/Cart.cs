namespace BookStore.Domain.Entities
{
    public partial class Cart
    {
        public Guid UserId { get; set; }

        public string? IdsItem { get; set; }

        public DateTime? Created { get; set; }

        public virtual User? User { get; set; }

    }
}
