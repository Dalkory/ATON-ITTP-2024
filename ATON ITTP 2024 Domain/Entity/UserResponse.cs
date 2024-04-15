using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ATON_ITTP_2024_Domain.Entity
{
    public class UserResponse : Entity
    {
        [Required]
        [DisplayName("Имя (запрещены все символы кроме латинских и русских букв)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DisplayName("Пол (0 - женщина, 1 - мужчина, 2 - неизвестно)")]
        public int Gender { get; set; }

        [MaybeNull]
        [DisplayName("Поле даты рождения (может быть Null)")]
        public DateTime? Birthday { get; set; }

        [MaybeNull]
        [DisplayName("Активный")]
        public bool IsActive { get; set; }

        public UserResponse() { }

        public UserResponse(User user)
        {
            this.Name = user.Name;
            this.Gender = user.Gender;
            this.Birthday = user.Birthday;
            this.IsActive = user.RevokedOn == null;
        }
    }
}
