using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BookStore.Infrastructure.Data;

public partial class DBBookContext : DbContext
{
    public DBBookContext()
    {
    }

    public DBBookContext(DbContextOptions<DBBookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookAuthor> BookAuthors { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chatbox> Chatboxes { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC075FDECEEB");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC0775F3F900");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Category).WithMany(p => p.Books).HasConstraintName("FK_Book_Category");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books).HasConstraintName("FK_Book_Publisher");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Books).HasConstraintName("FK_Book_Supplier");
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book_Aut__3214EC074A6FA799");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Author).WithMany(p => p.BookAuthors).HasConstraintName("FK_Book_Author_Author");

            entity.HasOne(d => d.Book).WithMany(p => p.BookAuthors).HasConstraintName("FK_Book_Author_Book");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Cart__1788CC4CD800DCAC");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithOne(p => p.Cart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__UserId__6C190EBB");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC070DFB3703");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Chatbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chatbox__3214EC07909BE206");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coupon__3214EC07AEC55A01");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07E7AE4985");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Manager).WithMany(p => p.Employees).HasConstraintName("FK_Employee_Manager");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manager__3214EC079FC6F7DA");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Message__3214EC07F6415AAC");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07248D3A1E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders).HasConstraintName("FK_Order_Coupon");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders).HasConstraintName("FK_ORDER_EMPLOYEE");

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Order_De__3214EC07CDF0705F");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderDetails).HasConstraintName("FK_Order_Detail_Book");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK_Order_Detail_Order");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Page__3214EC07FC2F92BA");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishe__3214EC07610A9A2B");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3214EC07FC9D77E6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews).HasConstraintName("FK_Review_Book");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews).HasConstraintName("FK_Review_Order");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("FK_Review_User");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07078CF86D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07E79080E2");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wishlist__3214EC07C472B195");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Book).WithMany(p => p.Wishlists).HasConstraintName("FK_Wishlist_Book");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists).HasConstraintName("FK_Wishlist_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
