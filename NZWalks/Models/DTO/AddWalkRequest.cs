using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; }
        [Required]
        [Range(0,50)]
        public double Length { get; set; } 
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
