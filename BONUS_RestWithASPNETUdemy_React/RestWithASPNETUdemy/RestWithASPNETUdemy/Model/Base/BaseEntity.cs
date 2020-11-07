using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
