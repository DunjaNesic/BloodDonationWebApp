using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public interface IMapperCustom<TDestination, TSource>
      where TSource : class
      where TDestination : class
    {
        TDestination ToDto(TSource source);
        TSource FromDto(TDestination source);
        
    }

}
