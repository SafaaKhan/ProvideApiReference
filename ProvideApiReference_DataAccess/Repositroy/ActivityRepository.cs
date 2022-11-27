using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_DataAccess.Repositroy.IRepository;
using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Helpers;
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
        public ActivityRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddActivityAsync(PostActivityDto postActivityDto)
        {
            var activity = _mapper.Map<Activity>(postActivityDto);

            _db.Activities.Add(activity);

            var result = await SaveAllAsync();
            if (!result)
            {
                return ResponseModel.Failure("Failed to create the activity",500);
            }

            return ResponseModel.Seccuss(_mapper.Map<ActivityDto>(activity),"");
        }

        public async Task<ResponseModel> DeleteActivityAsync(Guid Id)
        {
            var activity = await _db.Activities.FindAsync(Id);
            //if (activity == null)
            //{
            //    return ResponseModel.Failure("The activity object was not found",404);
            //}
            _db.Activities.Remove(activity);

            var result = await SaveAllAsync();
            if (!result)
            {
                return ResponseModel.Failure("Failed to delete the activity",500);
            };
            return  ResponseModel.Seccuss(null,"The object is deleted seccussfully"); ;
        }

        public async Task<ResponseModel> GetActivitiesAsync()
        {
            var activities = await _db.Activities.ToListAsync();
            return ResponseModel.Seccuss(_mapper.Map<IList<ActivityDto>>(activities),"");
        }

        //here
        public async Task<ResponseModel> GetActivityByIdAsync(Guid Id)
        {
            var activity = await _db.Activities.FindAsync(Id);
            if (activity == null)
            {
                return ResponseModel.Failure("The activity object was not found", 404);
            }
            var activityDto = _mapper.Map<ActivityDto>(activity);
            return ResponseModel.Seccuss(activityDto,"");
        }

        public async Task<ResponseModel> UpdateActivityAsync(ActivityDto activityDto)
        {
            var activity = await _db.Activities.FindAsync(activityDto.Id);
            if (activity == null)
            {
                return ResponseModel.Failure("The activity object was not found", 404);
            }

             _mapper.Map(activityDto,activity);

            var result = await SaveAllAsync();
            if (!result) 
            {
                return ResponseModel.Failure("Failed to update the activity", 500);
            }

            var activityDto2 = _mapper.Map<ActivityDto>(activity);
            return ResponseModel.Seccuss(activityDto2, "");
        }
    }
}
