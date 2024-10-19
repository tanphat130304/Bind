using System.Data.Entity;

namespace Bind
{
    public partial class SchoolContext : DbContext
    {
        // Chuỗi kết nối
        public SchoolContext()
            : base("name=SchoolContext")
        {
        }

        // Khai báo bảng Students
        public virtual DbSet<Students> Students { get; set; }

        // Cấu hình chi tiết cho model
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Thiết lập tên bảng cho Students nếu khác với tên class
            modelBuilder.Entity<Students>()
                .ToTable("Students");

            // Các cấu hình chi tiết khác nếu cần thiết
            modelBuilder.Entity<Students>()
                .Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100); // Giới hạn độ dài chuỗi

            modelBuilder.Entity<Students>()
                .Property(s => s.Major)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
