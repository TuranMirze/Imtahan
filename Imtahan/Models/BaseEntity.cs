using System.Runtime.CompilerServices;

namespace Imtahan.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
