using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoAn_PTUDWEB.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbColor> TbColors { get; set; } = null!;
        public virtual DbSet<TbContact> TbContacts { get; set; } = null!;
        public virtual DbSet<TbImageProduct> TbImageProducts { get; set; } = null!;
        public virtual DbSet<TbImageSlide> TbImageSlides { get; set; } = null!;
        public virtual DbSet<TbOrder> TbOrders { get; set; } = null!;
        public virtual DbSet<TbOrderDetail> TbOrderDetails { get; set; } = null!;
        public virtual DbSet<TbPost> TbPosts { get; set; } = null!;
        public virtual DbSet<TbPostComment> TbPostComments { get; set; } = null!;
        public virtual DbSet<TbProduct> TbProducts { get; set; } = null!;
        public virtual DbSet<TbProductCategory> TbProductCategories { get; set; } = null!;
        public virtual DbSet<TbProductColor> TbProductColors { get; set; } = null!;
        public virtual DbSet<TbReview> TbReviews { get; set; } = null!;
        public virtual DbSet<TbRole> TbRoles { get; set; } = null!;
        public virtual DbSet<TbTrademark> TbTrademarks { get; set; } = null!;
        public virtual DbSet<TbType> TbTypes { get; set; } = null!;
        public virtual DbSet<TbTypeProduct> TbTypeProducts { get; set; } = null!;
        public virtual DbSet<TbUser> TbUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-OAUGEEUH\\SQLEXPRESS;Initial Catalog=DoAnPT;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbColor>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.ToTable("tb_Color");

                entity.Property(e => e.ColorId).ValueGeneratedNever();

                entity.Property(e => e.ColorName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(100);
            });

            modelBuilder.Entity<TbContact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("tb_Contact");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.SubjectName).HasMaxLength(50);
            });

            modelBuilder.Entity<TbImageProduct>(entity =>
            {
                entity.HasKey(e => e.ImageProductId)
                    .HasName("PK_Anh_San_Pham");

                entity.ToTable("tb_ImageProduct");

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(500)
                    .HasColumnName("thumbnail");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbImageProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Anh_San_Pham_San_Pham");
            });

            modelBuilder.Entity<TbImageSlide>(entity =>
            {
                entity.HasKey(e => e.SlideId)
                    .HasName("PK_tb_AnhSlide");

                entity.ToTable("tb_ImageSlide");

                entity.Property(e => e.Path)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("path");
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Don_Hang");

                entity.ToTable("tb_Order");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tb_Order_tb_User");
            });

            modelBuilder.Entity<TbOrderDetail>(entity =>
            {
                entity.ToTable("tb_OrderDetail");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.TotalMoney).HasColumnType("money");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tb_OrderDetail_tb_Color");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_tb_OrderDetail_tb_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_tb_OrderDetail_tb_Product");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tb_OrderDetail_tb_Types");
            });

            modelBuilder.Entity<TbPost>(entity =>
            {
                entity.HasKey(e => e.BlogId)
                    .HasName("PK_tb_Blog");

                entity.ToTable("tb_Post");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoKeywords).HasMaxLength(250);

                entity.Property(e => e.SeoTitle).HasMaxLength(250);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbPosts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tb_Post_tb_User");
            });

            modelBuilder.Entity<TbPostComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_tb_BlogComment");

                entity.ToTable("tb_PostComment");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Detail).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.TbPostComments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_tb_BlogComment_tb_Blog");
            });

            modelBuilder.Entity<TbProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_San_Pham");

                entity.ToTable("tb_Product");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.PriceDiscount).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.Thumbnail).HasMaxLength(100);

                entity.HasOne(d => d.CategoryProduct)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.CategoryProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_Product_tb_ProductCategory");

                entity.HasOne(d => d.Trademark)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.TrademarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__San_Pham__Ma_TH__3D5E1FD2");
            });

            modelBuilder.Entity<TbProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryProductId)
                    .HasName("PK_Phan_Loai");

                entity.ToTable("tb_ProductCategory");

                entity.Property(e => e.Name).HasMaxLength(60);
            });

            modelBuilder.Entity<TbProductColor>(entity =>
            {
                entity.HasKey(e => e.ProductColorId);

                entity.ToTable("tb_ProductColor");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.TbProductColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_ProductColor_tb_Color");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbProductColors)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_tb_ProductColor_tb_Product");
            });

            modelBuilder.Entity<TbReview>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tb_Review");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_Review_tb_Product");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tb_Review_tb_User");
            });

            modelBuilder.Entity<TbRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_Vai_tro_nguoi_dung");

                entity.ToTable("tb_Role");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<TbTrademark>(entity =>
            {
                entity.HasKey(e => e.TrademarkId)
                    .HasName("PK_Thuong_Hieu");

                entity.ToTable("tb_Trademark");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Logo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('no-image.jpg')");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TbType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("tb_Types");

                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<TbTypeProduct>(entity =>
            {
                entity.HasKey(e => e.TypeProductId);

                entity.ToTable("tb_TypeProduct");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbTypeProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tb_TypeProduct_tb_Product");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TbTypeProducts)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_tb_TypeProduct_tb_Types");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User");

                entity.ToTable("tb_User");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Password_hash");

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.UserName)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_Account_tb_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
