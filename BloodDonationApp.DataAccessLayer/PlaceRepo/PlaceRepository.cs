using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.PlaceRepo
{
    public class PlaceRepository : RepositoryBase<Place>, IPlaceRepository
    {
        private readonly BloodDonationContext _context;
        public PlaceRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }

    }
}
