﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BloodDonationApp.Domain.DomainModel;


namespace BloodDonationApp.DataTransferObject.Donors
{
    [Serializable]
    public class GetDonorDTO
    {
        public string JMBG { get; set; } = string.Empty;
        public string DonorFullName { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public string PlaceName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public List<CallsToDonorDTO>? CallsToDonate { get; set; }


    }
}
