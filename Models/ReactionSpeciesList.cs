namespace Chemistry_Cafe_API.Models
{
    public class ReactionSpeciesList
    {
        public Guid uuid { get; set; }
        public Guid reaction_uuid { get; set; }
        public Guid species_uuid { get; set; }
        public string? version { get; set; }
        public bool isDel {  get; set; }
    }
}
