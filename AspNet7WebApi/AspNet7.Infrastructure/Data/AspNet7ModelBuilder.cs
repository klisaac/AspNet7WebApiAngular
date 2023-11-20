using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AspNet7.Core.Entities;
using AspNet7.Core.Entities.Base;


namespace AspNet7.Infrastructure.Data
{
    public static class AspNet7ModelBuilder
    {
        public static void ConfigureAddress(EntityTypeBuilder<Address> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Address").HasKey(a => a.AddressId);
            entityTypeBuilder.Property(a => a.AddressId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(a => a.AddressType).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(a => a.AddressLine).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.Country).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.City).IsRequired(false).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.State).IsRequired(false).HasMaxLength(100);
            entityTypeBuilder.Property(a => a.ZipCode).IsRequired(true).HasMaxLength(100);
            AddAuditFields(entityTypeBuilder);
        }
        public static void ConfigureCategory(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Category").HasKey(c => c.CategoryId);
            entityTypeBuilder.Property(c => c.CategoryId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(c => c.Description).HasMaxLength(100);
            entityTypeBuilder.Property(c => c.ImageName).IsRequired(false).HasMaxLength(200);
            AddAuditFields(entityTypeBuilder);
        }

        public static void ConfigureCustomer(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Customer").Property(c => c.CustomerId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.HasKey(c=>c.CustomerId);
            entityTypeBuilder.Property(c=>c.Name).HasMaxLength(200);
            entityTypeBuilder.Property(c=>c.Surname).HasMaxLength(100);
            entityTypeBuilder.Property(c=>c.Phone).HasMaxLength(20);
            entityTypeBuilder.Property(c=>c.DefaultAddressId);
            entityTypeBuilder.Property(c=>c.Email).HasMaxLength(100);
            entityTypeBuilder.Property(c=>c.CitizenId).HasMaxLength(100);
            AddAuditFields(entityTypeBuilder);
        }
        public static void ConfigureProduct(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Product").Property(p => p.ProductId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.HasKey(p => p.ProductId);
            entityTypeBuilder.Property(p => p.Code).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.HasIndex(p => p.Code).IsUnique();
            entityTypeBuilder.Property(p => p.Name).IsRequired(true).HasMaxLength(100);
            entityTypeBuilder.Property(p => p.Summary);
            entityTypeBuilder.Property(p => p.Description);
            entityTypeBuilder.Property(p => p.ImageFile).HasMaxLength(200);
            entityTypeBuilder.Property(p => p.UnitPrice).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(p => p.UnitsInStock);
            entityTypeBuilder.Property(p => p.Star);
            entityTypeBuilder.Property(p => p.CategoryId);
            AddAuditFields(entityTypeBuilder);
        }

        public static void ConfigureOrder(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Order").Property(o => o.OrderId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.HasKey(o => o.OrderId);
            entityTypeBuilder.Property(o => o.CustomerId).IsRequired();
            entityTypeBuilder.Property(o => o.BillingAddressId);
            entityTypeBuilder.HasOne(o => o.BillingAddress).WithMany().OnDelete(DeleteBehavior.Restrict);
            entityTypeBuilder.Property(o => o.ShippingAddressId);
            entityTypeBuilder.HasOne(o => o.ShippingAddress).WithMany().OnDelete(DeleteBehavior.Restrict);
            entityTypeBuilder.Property(o => o.Status);
            AddAuditFields(entityTypeBuilder);
        }
        public static void ConfigureOrderItem(EntityTypeBuilder<OrderItem> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("OrderItem").Property(oi => oi.OrderItemId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.HasKey(oi => oi.OrderItemId);
            entityTypeBuilder.Property(oi => oi.Quantity).IsRequired();
            entityTypeBuilder.Property(oi => oi.UnitPrice).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(oi => oi.ProductId).IsRequired();
            entityTypeBuilder.Property(oi => oi.OrderId).IsRequired();
            AddAuditFields(entityTypeBuilder);
        }
        public static void ConfigurePayment(EntityTypeBuilder<Payment> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Payment").Property(p => p.PaymentId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.HasKey(o => o.PaymentId);
            entityTypeBuilder.Property(p => p.OrderId).IsRequired(true);
            entityTypeBuilder.Property(p => p.Amount).HasColumnType("decimal(16, 3)");
            entityTypeBuilder.Property(p => p.Method).IsRequired(true);
            AddAuditFields(entityTypeBuilder);
        }

        public static void ConfigureUser(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("User").Property(u => u.UserId).ValueGeneratedOnAdd().UseIdentityColumn();
            entityTypeBuilder.HasKey(u => u.UserId);
            entityTypeBuilder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            entityTypeBuilder.HasIndex(u => u.UserName).IsUnique();
            entityTypeBuilder.Property(u => u.PasswordHash).HasColumnType("varbinary(max)").IsRequired();
            entityTypeBuilder.Property(u => u.PasswordSalt).HasColumnType("varbinary(max)").IsRequired();
            AddAuditFields(entityTypeBuilder);
        }

        private static void AddAuditFields<T>(EntityTypeBuilder<T> entityTypeBuilder) where T : AuditEntity
        {
            entityTypeBuilder.Property(ae => ae.IsDeleted).IsRequired(true);
            entityTypeBuilder.Property(ae => ae.CreatedBy).IsRequired(true).HasMaxLength(50);
            entityTypeBuilder.Property(ae => ae.CreatedDate).IsRequired(true);
            entityTypeBuilder.Property(ae => ae.LastModifiedBy).IsRequired(false).HasMaxLength(50);
            entityTypeBuilder.Property(ae => ae.LastModifiedDate).IsRequired(false);
        }

        //public static void Build(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<ProductSpecificationAssociation>()
        //    //    .HasKey(psa => new { psa.ProductId, psa.SpecificationId });

        //    //modelBuilder.Entity<ProductSpecificationAssociation>()
        //    //    .HasOne(psa => psa.Product)
        //    //    .WithMany(p => p.Specifications)
        //    //    .HasForeignKey(psa => psa.ProductId);

        //    //modelBuilder.Entity<OrderPaymentAssociation>()
        //    //    .HasKey(psa => new { psa.OrderId, psa.PaymentId });

        //    //modelBuilder.Entity<OrderPaymentAssociation>()
        //    //    .HasOne(opa => opa.Order)
        //    //    .WithMany(o => o.Payments)
        //    //    .HasForeignKey(opa => opa.OrderId);

        //    //modelBuilder.Entity<ContractPaymentAssociation>()
        //    //    .HasKey(psa => new { psa.ContractId, psa.PaymentId });

        //    //modelBuilder.Entity<ContractPaymentAssociation>()
        //    //    .HasOne(cpa => cpa.Contract)
        //    //    .WithMany(c => c.Payments)
        //    //    .HasForeignKey(cpa => cpa.ContractId);
        //}
    }
}
