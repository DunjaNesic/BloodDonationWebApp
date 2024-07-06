using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.LinkModel
{
    public class LinksBase
    {
        public LinksBase()
        {           
        }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
