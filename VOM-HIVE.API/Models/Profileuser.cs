using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sp2.Models
{
    [Table("Profile_user")]
    public class ProfileuserModel
    {
        [Key]
        [Column("id_user")]
        public int id_user { get; set; }

        [Required]
        [Column("nm_user")]
        public string? nm_user { get; set; }

        [Required]
        [Column("pass_user")]
        public string? pass_user { get; set; }

        [Required]
        [Column("dt_user")]
        public DateTime dt_register { get; set; }

        [Required]
        [Column("permission_user")]
        public string? permission_user { get; set; }

        [Required]
        [Column("status_user")]
        public string? status { get; set; }

        [ForeignKey("Company")]
        public int id_company { get; set; }
        public CompanyModel Company { get; set; }
    }
}
