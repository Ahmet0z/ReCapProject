﻿using Core.Entities;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public string Plate { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public int ModelYear { get; set; }
        public int Findeks { get; set; }
        public bool IsRentable { get; set; }
        public string ImagePath { get; set; }
    }
}
