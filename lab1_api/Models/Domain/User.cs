using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1_api.Models.Domain
{
    public class User
    {
        [Key] // Đánh dấu là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Định nghĩa tự động tăng
        public int Id { get; set; }

        [Required] // Bắt buộc nhập
        [MaxLength(100)] // Giới hạn độ dài chuỗi
        public string? Name { get; set; }

        [Required]
        [EmailAddress] // Định dạng email
        [MaxLength(200)]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)] // Mật khẩu tối thiểu 6 ký tự
        public string? Password { get; set; }

        [MaxLength(250)] // Giới hạn độ dài đường dẫn avatar
        public string? Avatar { get; set; }
    }
}
