﻿namespace nzworks.api.Models.DTO
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //Navigation Properties
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
