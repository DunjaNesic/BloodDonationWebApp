
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.LinkModel
{
    public class LinkWrapper<T> : LinksBase
    {
        public List<T> Value { get; set; } = new List<T>();
        public LinkWrapper()
        {            
        }
        public LinkWrapper(List<T> value) => Value = value;
    }
}
