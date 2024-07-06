using BloodDonationApp.Domain.CustomModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedCustomExpando> ShapeData(IEnumerable<T> entities, string fieldsString);
        ShapedCustomExpando ShapeData(T entity, string fieldsString);
    }
}
