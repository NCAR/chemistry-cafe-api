namespace Chemistry_Cafe_API.Models
{
    public class FamilyMechList
    {
        public Guid uuid { get; set; }
        public Guid family_uuid { get; set; }
        public Guid mechanism_uuid { get; set; }
        public string? version { get; set; }
        public bool isDel {  get; set; }
    }
}
