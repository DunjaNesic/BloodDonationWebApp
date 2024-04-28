﻿using BloodDonationApp.DataAccessLayer.BaseRepository;
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

        public Task DeleteAsync(Place t)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Place>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Place>> GetByConditionAsync(Expression<Func<Place, bool>> condition, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Place t)
        {
            throw new NotImplementedException();
        }
    }
}
