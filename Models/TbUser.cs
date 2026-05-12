using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAUNGJAJAN.Models;

[Table("tb_user")]
public class TbUser
{
    [Key]
    [Column("id_user")]
    public int IdUser { get; set; }

    [Column("nama")]
    public string Nama { get; set; } = "";

    [Column("email")]
    public string Email { get; set; } = "";

    [Column("password")]
    public string Password { get; set; } = "";

    [Column("saldo", TypeName = "decimal(10,2)")]
    public decimal Saldo { get; set; } = 0;


    
}
