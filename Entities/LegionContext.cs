﻿using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	public partial class LegionContext : IdentityDbContext
	{

		const string DATABASE_NAME = "legionofcommerce8"; // Change this AND connection string

		public LegionContext(DbContextOptions<LegionContext> options)
			: base(options)
		{
			// Database.EnsureDeleted(); // Use this for database resets!
			Database.EnsureCreated();
		}

		public virtual DbSet<Address> Address { get; set; }
		public virtual DbSet<CustomerReview> CustomerReview { get; set; }
		public virtual DbSet<Order> Order { get; set; }
		public virtual DbSet<OrderAddress> OrderAddress { get; set; }
		public virtual DbSet<Product> Product { get; set; }
		public virtual DbSet<ProductAddress> ProductAddress { get; set; }
		public virtual DbSet<ProductReview> ProductReviews { get; set; }
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<QuestionAnswer> QuestionAnswer { get; set; }
		public virtual DbSet<Review> Review { get; set; }
		public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<UserAddress> UserAddress { get; set; }
		public virtual DbSet<RefreshToken> RefreshToken { get; set; }
		public virtual DbSet<Category> Categories { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

			modelBuilder.Entity<Address>(entity =>
			{
				entity.ToTable("address", DATABASE_NAME);

				entity.Property(e => e.AddressId).HasColumnType("int(11)");

				entity.Property(e => e.AdministrativeArea)
							.IsRequired()
							.HasMaxLength(85)
							.IsUnicode(false);

				entity.Property(e => e.CountryCode)
							.IsRequired()
							.HasMaxLength(2)
							.IsUnicode(false);

				entity.Property(e => e.Locality)
							.IsRequired()
							.HasMaxLength(30)
							.IsUnicode(false);

				entity.Property(e => e.PostalCode)
							.IsRequired()
							.HasMaxLength(6)
							.IsUnicode(false);

				entity.Property(e => e.Premise)
							.HasMaxLength(100)
							.IsUnicode(false);

				entity.Property(e => e.StreetAddress)
							.IsRequired()
							.HasMaxLength(255)
							.IsUnicode(false);
			});


			modelBuilder.Entity<CustomerReview>(entity =>
			{
				entity.ToTable("customer_review", DATABASE_NAME);

				entity.HasIndex(e => e.ReviewId)
							.HasName("fk_CustomerReview_Review1_idx");

				entity.HasIndex(e => e.TargetUserId)
							.HasName("fk_CustomerReview_User1_idx");

				entity.Property(e => e.CustomerReviewId)
							.HasColumnType("int(11)")
							.ValueGeneratedNever();

				entity.Property(e => e.ReviewId).HasColumnType("int(11)");

				entity.Property(e => e.TargetUserId).HasColumnType("varchar(256)");

				entity.HasOne(d => d.Review)
							.WithOne(p => p.CustomerReview)
							.HasForeignKey<CustomerReview>(d => d.ReviewId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_CustomerReview_Review1");

				entity.HasOne(d => d.TargetUser)
							.WithMany(p => p.CustomerReviews)
							.HasForeignKey(d => d.TargetUserId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_CustomerReview_User1");
			});

			modelBuilder.Entity<Order>(entity =>
			{
				entity.ToTable("order", DATABASE_NAME);

				entity.HasIndex(e => e.Date)
							.HasName("date_idx");

				entity.HasIndex(e => e.ProductCondition)
							.HasName("product_condition_idx");

				entity.HasIndex(e => e.ProductId)
							.HasName("fk_Order_Product1_idx");

				entity.HasIndex(e => e.ProductPrice)
							.HasName("product_price_idx");

				entity.HasIndex(e => e.ProductTitle)
							.HasName("product_title_idx");

				entity.HasIndex(e => e.State)
							.HasName("state_idx");

				entity.HasIndex(e => e.UserType)
							.HasName("user_type_idx");

				entity.Property(e => e.OrderId)
							.HasColumnType("int(11)")
							.ValueGeneratedNever();

				entity.Property(e => e.BuyerId).HasColumnType("varchar(256)");

				entity.Property(e => e.Date).HasColumnType("date");

				entity.Property(e => e.PaymentMethod)
							.IsRequired()
							.HasColumnType("enum('CREDIT','DEBIT','PAYPAL')");

				entity.Property(e => e.ProductCondition)
							.IsRequired()
							.HasColumnType("enum('USED','NEW')");

				entity.Property(e => e.ProductId).HasColumnType("int(11)");

				entity.Property(e => e.ProductTitle)
							.IsRequired()
							.HasMaxLength(100)
							.IsUnicode(false);

				entity.Property(e => e.SellerId).HasColumnType("varchar(256)");

				entity.Property(e => e.State)
							.IsRequired()
							.HasColumnType("enum('GETTING_READY','DELIVERING','DELIVERED','REFUNDED','REFUND_REQUESTED','REFUND_DELIVERING','CANCELED')");

				entity.Property(e => e.UserType)
							.IsRequired()
							.HasColumnType("enum('SELLER','BUYER')");

				entity.HasOne(d => d.Product)
							.WithMany(p => p.Orders)
							.HasForeignKey(d => d.ProductId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_Order_Product1");
			});

			modelBuilder.Entity<OrderAddress>(entity =>
			{
				entity.ToTable("order_address", DATABASE_NAME);

				entity.HasIndex(e => e.AddressId)
							.HasName("fk_OrderAddress_Address1_idx");

				entity.HasIndex(e => e.OrderId)
							.HasName("fk_OrderAddress_Order1_idx");

				entity.Property(e => e.OrderAddressId)
							.HasColumnType("int(11)")
							.ValueGeneratedNever();

				entity.Property(e => e.AddressId).HasColumnType("int(11)");

				entity.Property(e => e.OrderId).HasColumnType("int(11)");

				entity.HasOne(d => d.Address)
							.WithOne(p => p.OrderAddress)
							.HasForeignKey<OrderAddress>(d => d.AddressId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_OrderAddress_Address1");

				entity.HasOne(d => d.Order)
							.WithOne(p => p.OrderAddress)
							.HasForeignKey<OrderAddress>(d => d.OrderId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_OrderAddress_Order1");
			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.ToTable("categories", DATABASE_NAME);

				entity.Property(e => e.CategoryId).HasColumnType("int(11)");

				entity.Property(e => e.Name)
							.IsRequired()
							.HasMaxLength(255)
							.IsUnicode(false);
				entity.Property(e => e.ImageUrl)
							.IsRequired()
							.HasMaxLength(2048)
							.IsUnicode(false);
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.ToTable("product", DATABASE_NAME);

				entity.HasIndex(e => e.Condition)
							.HasName("condition_idx");

				entity.HasIndex(e => e.CreationDate)
							.HasName("creationdate_idx");

				entity.HasIndex(e => e.Price)
							.HasName("price_idx");

				entity.HasIndex(e => e.Quantity)
							.HasName("quantity_idx");

				entity.HasIndex(e => e.Rating)
							.HasName("rating_idx");

				entity.HasIndex(e => e.SellerId)
							.HasName("fk_Product_User1_idx");

				entity.HasIndex(e => e.State)
							.HasName("state_idx");

				entity.HasIndex(e => e.Title)
							.HasName("title_idx");

				entity.HasIndex(e => e.CategoryId)
							.HasName("fk_category_idx");

				entity.Property(e => e.ProductId).HasColumnType("int(11)");

				entity.Property(e => e.Condition)
							.IsRequired()
							.HasColumnType("enum('USED','NEW')");

				entity.Property(e => e.CreationDate).HasColumnType("date");

				entity.Property(e => e.Description)
							.IsRequired()
							.IsUnicode(false);

				entity.Property(e => e.MainImgId)
							.IsRequired().HasColumnType("int(11)");
				entity.Property(e => e.CategoryId)
							.IsRequired().HasColumnType("int(11)");

				entity.Property(e => e.Quantity).HasColumnType("int(11)");

				entity.Property(e => e.RatingsCount).HasColumnType("int(11)");

				entity.Property(e => e.SellerId).HasColumnType("varchar(256)");

				entity.Property(e => e.State)
							.IsRequired()
							.HasColumnType("enum('LIVE','DRAFT')");

				entity.Property(e => e.Title)
							.IsRequired()
							.HasMaxLength(100)
							.IsUnicode(false);

				entity.HasOne(d => d.Seller)
							.WithMany(p => p.Products)
							.HasForeignKey(d => d.SellerId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_Product_User1");
			});

			modelBuilder.Entity<ProductAddress>(entity =>
			{
				entity.ToTable("product_address", DATABASE_NAME);

				entity.HasIndex(e => e.AddressId)
							.HasName("fk_ProductAddress_Address1_idx");

				entity.HasIndex(e => e.ProductId)
							.HasName("fk_ProductAddress_Product1_idx");

				entity.Property(e => e.ProductAddressId).HasColumnType("int(11)");

				entity.Property(e => e.AddressId).HasColumnType("int(11)");

				entity.Property(e => e.ProductId).HasColumnType("int(11)");

				entity.HasOne(d => d.Address)
							.WithOne(p => p.ProductAddress)
							.HasForeignKey<ProductAddress>(d => d.AddressId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_ProductAddress_Address1");

				entity.HasOne(d => d.Product)
							.WithOne(p => p.ProductAddress)
							.HasForeignKey<ProductAddress>(d => d.ProductId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_ProductAddress_Product1");
			});

			modelBuilder.Entity<ProductReview>(entity =>
			{
				entity.HasKey(e => e.ProductReviewId);

				entity.ToTable("product_reviews", DATABASE_NAME);

				entity.HasIndex(e => e.ReviewId)
							.HasName("fk_ProductReviews_Review1_idx");

				entity.HasIndex(e => e.TargetProductId)
							.HasName("fk_ProductReviews_Product1_idx");

				entity.Property(e => e.ProductReviewId)
							.HasColumnType("int(11)")
							.ValueGeneratedNever();

				entity.Property(e => e.ReviewId).HasColumnType("int(11)");

				entity.Property(e => e.TargetProductId).HasColumnType("int(11)");

				entity.HasOne(d => d.Review)
							.WithOne(p => p.ProductReview)
							.HasForeignKey<ProductReview>(d => d.ReviewId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_ProductReviews_Review1");

				entity.HasOne(d => d.TargetProduct)
							.WithMany(p => p.ProductReviews)
							.HasForeignKey(d => d.TargetProductId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_ProductReviews_Product1");
			});

			modelBuilder.Entity<ProductImage>(entity =>
			{
				entity.HasKey(e => e.ProductImageId);

				entity.ToTable("product_images", DATABASE_NAME);

				entity.HasIndex(e => e.ProductId)
							.HasName("fk_ProductImages_Product1_idx");

				entity.Property(e => e.ProductImageId)
							.HasColumnType("int(11)");

				entity.Property(e => e.ProductId).HasColumnType("int(11)");

				entity.Property(e => e.ProductImageUrl)
							.IsRequired()
							.HasMaxLength(2048) // max length url
							.IsUnicode(false);

				entity.HasOne(d => d.Product)
							.WithMany(p => p.ProductImages)
							.HasForeignKey(d => d.ProductId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_ProductImages_Product1");
			});

			modelBuilder.Entity<QuestionAnswer>(entity =>
			{
				entity.ToTable("question_answer", DATABASE_NAME);

				entity.HasIndex(e => e.AnswererId)
							.HasName("answerer_idx");

				entity.HasIndex(e => e.CreationDate)
							.HasName("creationdate_idx");

				entity.HasIndex(e => e.ProductProductId)
							.HasName("fk_QuestionAnswer_Product1_idx");

				entity.HasIndex(e => e.Question)
							.HasName("question_idx");

				entity.HasIndex(e => e.QuestionerId)
							.HasName("questioner_idx");

				entity.Property(e => e.QuestionAnswerId)
							.HasColumnType("int(11)")
							.ValueGeneratedNever();

				entity.Property(e => e.Answer)
							.HasMaxLength(500)
							.IsUnicode(false);

				entity.Property(e => e.AnswererId).HasColumnType("varchar(256)");

				entity.Property(e => e.CreationDate).HasColumnType("date");

				entity.Property(e => e.ProductProductId)
							.HasColumnName("Product_ProductId")
							.HasColumnType("int(11)");

				entity.Property(e => e.Question)
							.IsRequired()
							.HasMaxLength(200)
							.IsUnicode(false);

				entity.Property(e => e.QuestionerId).HasColumnType("varchar(256)");

				entity.HasOne(d => d.ProductProduct)
							.WithMany(p => p.QuestionAnswers)
							.HasForeignKey(d => d.ProductProductId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_QuestionAnswer_Product1");
			});

			modelBuilder.Entity<Review>(entity =>
			{
				entity.ToTable("review", DATABASE_NAME);

				entity.HasIndex(e => e.AmountUseful)
							.HasName("amountuseful_idx");

				entity.HasIndex(e => e.AuthorId)
							.HasName("fk_Review_User1_idx");

				entity.HasIndex(e => e.CreationDate)
							.HasName("creationdate_idx");

				entity.HasIndex(e => e.Rating)
							.HasName("rating_idx");

				entity.Property(e => e.ReviewId)
							.HasColumnType("int(11)")
							.ValueGeneratedNever();

				entity.Property(e => e.AmountUseful).HasColumnType("int(11)");

				entity.Property(e => e.AuthorId).HasColumnType("varchar(256)");

				entity.Property(e => e.Body)
							.HasMaxLength(500)
							.IsUnicode(false);

				entity.Property(e => e.CreationDate).HasColumnType("date");

				entity.Property(e => e.Rating).HasColumnType("int(11)");

				entity.Property(e => e.Title)
							.IsRequired()
							.HasMaxLength(200)
							.IsUnicode(false);

				entity.HasOne(d => d.Author)
							.WithMany(p => p.Reviews)
							.HasForeignKey(d => d.AuthorId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_Review_User1");
			});

			modelBuilder.Entity<ShoppingCartItem>(entity =>
			{
				entity.ToTable("shopping_cart_item", DATABASE_NAME);

				entity.HasIndex(e => e.ProductId)
							.HasName("fk_ShoppingCart_Product1_idx");

				entity.HasIndex(e => e.UserId)
							.HasName("fk_ShoppingCart_User1_idx");

				entity.Property(e => e.ShoppingCartItemId)
							.HasColumnType("int(11)")
							.ValueGeneratedNever();

				entity.Property(e => e.ProductId).HasColumnType("int(11)");

				entity.Property(e => e.Quantity).HasColumnType("int(11)");

				entity.Property(e => e.UserId).HasColumnType("varchar(256)");

				entity.HasOne(d => d.Product)
							.WithMany(p => p.ShoppingCartItems)
							.HasForeignKey(d => d.ProductId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_ShoppingCart_Product1");

				entity.HasOne(d => d.User)
							.WithMany(p => p.ShoppingCartItems)
							.HasForeignKey(d => d.UserId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_ShoppingCart_User1");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("user", DATABASE_NAME);

				/* entity.HasIndex(e => e.Email)
							.HasName("Email_UNIQUE")
							.IsUnique(); */

				/* entity.HasIndex(e => e.Username)
							.HasName("Username_UNIQUE")
							.IsUnique(); */

				entity.Property(e => e.BillingAddressId).HasColumnType("int(11)");

				entity.Property(e => e.CreationDate).HasColumnType("date");

				entity.Property(e => e.Email)
							.IsRequired()
							.HasMaxLength(320)
							.IsUnicode(false);

				entity.Property(e => e.Fname)
							.HasColumnName("FName")
							.HasMaxLength(20)
							.IsUnicode(false);

				entity.Property(e => e.Lname)
							.HasColumnName("LName")
							.HasMaxLength(20)
							.IsUnicode(false);

				/* entity.Property(e => e.Password)
							.IsRequired()
							.HasMaxLength(128)
							.IsUnicode(false); */

				entity.Property(e => e.ProfilePicture)
							.HasMaxLength(2083)
							.IsUnicode(false);

				entity.Property(e => e.ResidenceAddressId).HasColumnType("int(10) unsigned");

				/* entity.Property(e => e.Username)
							.IsRequired()
							.HasMaxLength(15)
							.IsUnicode(false); */
			});

			modelBuilder.Entity<UserAddress>(entity =>
			{
				entity.ToTable("user_address", DATABASE_NAME);

				entity.HasIndex(e => e.AddressId)
							.HasName("fk_UserAddress_Address1_idx");

				entity.HasIndex(e => e.UserId)
							.HasName("fk_UserAddress_User1_idx");

				entity.Property(e => e.UserAddressId).HasColumnType("int(11)");

				entity.Property(e => e.AddressId).HasColumnType("int(11)");

				entity.Property(e => e.Type)
							.IsRequired()
							.HasColumnType("enum('BILLING','RESIDENCY')");

				entity.Property(e => e.UserId).HasColumnType("varchar(256)");

				entity.HasOne(d => d.Address)
							.WithOne(p => p.UserAddress)
							.HasForeignKey<UserAddress>(d => d.AddressId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_UserAddress_Address1");

				entity.HasOne(d => d.User)
							.WithOne(p => p.UserAddress)
							.HasForeignKey<UserAddress>(d => d.UserId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_UserAddress_User1");
			});
		}
	}
}
