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
        public virtual DbSet<TbSlide> TbSlides { get; set; } = null!;
        public virtual DbSet<TbTrademark> TbTrademarks { get; set; } = null!;
        public virtual DbSet<TbTuKhoa> TbTuKhoas { get; set; } = null!;
        public virtual DbSet<TbTuKhoaSanPham> TbTuKhoaSanPhams { get; set; } = null!;
        public virtual DbSet<TbUser> TbUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
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
                entity.HasKey(e => e.ImageSlideId)
                    .HasName("PK_tb_AnhSlide");

                entity.ToTable("tb_ImageSlide");

                entity.Property(e => e.File)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Slide)
                    .WithMany(p => p.TbImageSlides)
                    .HasForeignKey(d => d.SlideId)
                    .HasConstraintName("FK__Anh_Slide__Ma_Sl__38996AB5");
            });

            modelBuilder.Entity<TbOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Don_Hang");

                entity.ToTable("tb_Order");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<TbOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK_Chi_Tiet_Don_Hang");

                entity.ToTable("tb_OrderDetail");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Chi_Tiet___Ma_DH__398D8EEE");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Chi_Tiet___Ma_SP__3A81B327");
            });

            modelBuilder.Entity<TbPost>(entity =>
            {
                entity.HasKey(e => e.BlogId)
                    .HasName("PK_tb_Blog");

                entity.ToTable("tb_Post");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

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
                    .HasConstraintName("FK_tb_Blog_tb_Account");
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

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Thumbnail).HasMaxLength(100);

                entity.HasOne(d => d.CategoryProduct)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.CategoryProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_Product_tb_ProductCategory");

                entity.HasOne(d => d.Trademark)
                    .WithMany(p => p.TbProducts)
                    .HasForeignKey(d => d.TrademarkId)
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
                entity.HasNoKey();

                entity.ToTable("tb_ProductColor");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.HasOne(d => d.Color)
                    .WithMany()
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_ProductColor_tb_Color");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_ProductColor_tb_Product");
            });

            modelBuilder.Entity<TbReview>(entity =>
            {
                entity.HasKey(e => e.ReviewId);

                entity.ToTable("tb_Review");

                entity.Property(e => e.ReviewId).ValueGeneratedOnAdd();

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbReviews)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_tb_Review_tb_Account");

                entity.HasOne(d => d.Review)
                    .WithOne(p => p.TbReview)
                    .HasForeignKey<TbReview>(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_Review_tb_Product");
            });

            modelBuilder.Entity<TbRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_Vai_tro_nguoi_dung");

                entity.ToTable("tb_Role");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<TbSlide>(entity =>
            {
                entity.HasKey(e => e.SlideId)
                    .HasName("PK_Slide");

                entity.ToTable("tb_Slide");

                entity.Property(e => e.Caption).HasMaxLength(100);

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Summary).HasMaxLength(300);
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

            modelBuilder.Entity<TbTuKhoa>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK_tukhoa");

                entity.ToTable("tb_TuKhoa");

                entity.Property(e => e.MaTk).HasColumnName("Ma_TK");

                entity.Property(e => e.Mota)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbTuKhoaSanPham>(entity =>
            {
                entity.HasKey(e => e.MaTksp)
                    .HasName("PK_Tu_Khoa_San_Pham");

                entity.ToTable("tb_TuKhoaSanPham");

                entity.Property(e => e.MaTksp).HasColumnName("Ma_TKSP");

                entity.Property(e => e.MaTk).HasColumnName("Ma_TK");

                entity.HasOne(d => d.MaTkNavigation)
                    .WithMany(p => p.TbTuKhoaSanPhams)
                    .HasForeignKey(d => d.MaTk)
                    .HasConstraintName("FK__Tu_Khoa_S__Ma_TK__403A8C7D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TbTuKhoaSanPhams)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Tu_Khoa_S__Ma_SP__3F466844");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User");

                entity.ToTable("tb_User");

                entity.Property(e => e.Address).HasMaxLength(200);

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
                    .HasConstraintName("FK_tb_Account_tb_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
