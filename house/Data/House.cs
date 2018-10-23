using System.ComponentModel.DataAnnotations;

namespace house.Data
{
    public class House
    {
        public int Id { get; set; }

        //[MaxLength(5)]
        //[Required]
        public string Zip { get; set; }

        //[MaxLength(200)]
        //[Required]
        public string Address { get; set; }
    } 
}
