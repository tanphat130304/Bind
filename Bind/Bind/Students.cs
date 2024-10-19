using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bind
{
    public class Students
    {
        [Key] // Khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng
        public int StudentId { get; set; }

        [Required] // Không được để trống
        [StringLength(100)] // Độ dài tối đa
        public string FullName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Major { get; set; }
    }
}
