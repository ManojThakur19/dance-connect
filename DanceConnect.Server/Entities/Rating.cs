using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanceConnect.Server.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(0, 5)]  
        public decimal RatingValue { get; set; }

        [ForeignKey(nameof(RatedUser))]
        public int RatedTo { get; set; }
        public Instructor? RatedUser { get; set; }

        //[ForeignKey(nameof(RaterUser))]
        //public int RatedBy { get; set; }
        //public User? RaterUser { get; set; }
    }
}
