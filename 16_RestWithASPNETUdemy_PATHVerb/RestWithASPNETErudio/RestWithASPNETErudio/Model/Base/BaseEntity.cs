using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETErudio.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
