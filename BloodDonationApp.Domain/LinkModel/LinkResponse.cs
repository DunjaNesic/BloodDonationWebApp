using BloodDonationApp.Domain.CustomModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.LinkModel
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        public List<CustomExpando> ShapedEntities { get; set; }
        public LinkWrapper<CustomExpando> LinkedEntities { get; set; }
        public LinkResponse()
        {
            ShapedEntities = new List<CustomExpando>();
            LinkedEntities = new LinkWrapper<CustomExpando>();
        }
    }
}
