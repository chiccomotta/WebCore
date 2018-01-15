using System.ComponentModel.DataAnnotations;

namespace WebCore.Models
{
    public class Customer
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public int Codice { get; set; }
    }
}