using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AspNet7.Core.Entities;
using AspNet7.Core.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNet7.Infrastructure.Data
{
    public class AspNet7DataContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;
        //private IConfiguration _configuration;
        //private readonly ICurrentUser _currentUser;

        public AspNet7DataContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        //public AspNet5DataContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        //public AspNet5DataContext(DbContextOptions<AspNet5DataContext> options, ICurrentUser currentUser)
        //    : base(options)
        //{
        //    _currentUser = currentUser;
        //}
        public AspNet7DataContext(DbContextOptions<AspNet7DataContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            //options.UseSqlServer(_configuration.GetConnectionString(Constants.DbConnectionStringKey));
            //options.UseSqlServer("Server=localhost;User Id=sa;password=Dev@2019;Database=AspNet5Dev");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(AspNet7ModelBuilder.ConfigureAddress);
            modelBuilder.Entity<Category>(AspNet7ModelBuilder.ConfigureCategory);
            modelBuilder.Entity<Product>(AspNet7ModelBuilder.ConfigureProduct);
            modelBuilder.Entity<Customer>(AspNet7ModelBuilder.ConfigureCustomer);
            modelBuilder.Entity<Order>(AspNet7ModelBuilder.ConfigureOrder);
            modelBuilder.Entity<OrderItem>(AspNet7ModelBuilder.ConfigureOrderItem);
            modelBuilder.Entity<Payment>(AspNet7ModelBuilder.ConfigurePayment);
            modelBuilder.Entity<User>(AspNet7ModelBuilder.ConfigureUser);
            //ConfigureUser(modelBuilder.Entity<User>());
        }

        //private void ConfigureUser<T>(EntityTypeBuilder<T> entityTypeBuilder) where T : AuditEntity
        //{
        //    //entityTypeBuilder.ToTable("User").Property(u => u.UserId).ValueGeneratedOnAdd().UseIdentityColumn();
        //    //entityTypeBuilder.Property(u => u.UserName).IsRequired();
        //    //entityTypeBuilder.HasIndex(u => u.UserName).IsUnique();
        //    //entityTypeBuilder.Property<byte[]>("PasswordHash").HasColumnType("varbinary(max)");
        //    //entityTypeBuilder.Property<byte[]>("PasswordSalt").HasColumnType("varbinary(max)");
        //    entityTypeBuilder.Property(d => d.IsDeleted).IsRequired(true);
        //    entityTypeBuilder.Property(d => d.CreatedBy).IsRequired(true);
        //    entityTypeBuilder.Property(d => d.CreatedDate).IsRequired(true);
        //    entityTypeBuilder.Property(d => d.LastModifiedBy).IsRequired(false);
        //    entityTypeBuilder.Property(d => d.LastModifiedDate).IsRequired(false);
        //}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsDeleted = false;
                        //entry.Entity.CreatedBy = _currentUser.UserName;
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUser.UserName;
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = _currentTransaction ?? await Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                //await SaveChangesAsync();
                await _currentTransaction?.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}
