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
    public class PlaceRepository : IPlaceRepository
    {
        private readonly BloodDonationContext _context;
        public PlaceRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public Task CreateAsync(Place t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Place t)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Place> GetAll(bool trackChanges, params Expression<Func<Place, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Place> GetByCondition(Expression<Func<Place, bool>> condition, bool trackChanges, params Expression<Func<Place, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public void Update(Place t)
        {
            throw new NotImplementedException();
        }
    }
}
