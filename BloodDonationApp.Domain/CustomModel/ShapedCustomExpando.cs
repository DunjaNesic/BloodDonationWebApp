using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.CustomModel
{
    public class ShapedCustomExpando
    {
        public int Id { get; set; }
        public CustomExpando CustomExpando { get; set; }
        public ShapedCustomExpando()
        {
            CustomExpando = new CustomExpando();
        }
    }
}
