using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_DataAccess.Repositroy.IRepository;
using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_DataAccess.Repositroy
{
    public class ActivityRepository : Repository, IActivityRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        //unit of work 
        //error handling
        public ActivityRepository(ApplicationDbContext db, IMapper mapper):base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ActivityDto> AddActivityAsync(PostActivityDto postActivityDto)
        {
            var activity = _mapper.Map<Activity>(postActivityDto);

             _db.Activities.Add(activity);

            var result = await SaveAllAsync();
            if(!result) return null;

            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<bool> DeleteActivityAsync(Guid Id)
        {
            var activity = await _db.Activities.FindAsync(Id);
            if(activity == null) return false;
            _db.Activities.Remove(activity);

            var result = await SaveAllAsync();
            if (!result) return false;
            return true;
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesAsync()
        {
            var activities = await _db.Activities.ToListAsync();
            return _mapper.Map<IList<ActivityDto>>(activities);
        }

        public async Task<ActivityDto> GetActivityByIdAsync(Guid Id)
        {
            var activity = await _db.Activities.FindAsync(Id);
            if (activity == null) return null;
            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<ActivityDto> UpdateActivityAsync(ActivityDto activityDto)
        {
            //try catch must or handling errors
            var activity = _mapper.Map<Activity>(activityDto);

            _db.Activities.Update(activity); 
            var result = await SaveAllAsync();
            if (!result) return null;

            return _mapper.Map<ActivityDto>(activity);
        }
    }
}
