﻿namespace Chemistry_Cafe_API.Models
{
    public class MechTagMechList
    {
        public Guid uuid { get; set; }
        public Guid mechanism_uuid { get; set; }
        public Guid tag_mechanism_uuid { get; set; }
        public string? version { get; set; }
        public bool isDel {  get; set; }
    }
}
