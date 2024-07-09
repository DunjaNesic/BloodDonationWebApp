using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Questionnaires
{
    [Serializable]
    public class UpdateQuestionnaireDTO
    {
        public string? Remark { get; set; }
        public List<bool> Answers { get; set; } = new List<bool>();
        public bool Approved { get; set; }
        public string? QRCode { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
