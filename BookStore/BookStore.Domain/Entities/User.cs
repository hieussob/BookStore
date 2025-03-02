namespace BookStore.Domain.Entities
{
    public partial class User
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? Created { get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    }
}
