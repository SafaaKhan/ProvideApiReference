using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Helpers;
using ProvideApiReference_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_DataAccess.Repositroy.IRepository
{
    public interface IActivityRepository
    {
        Task<ResponseModel> GetActivitiesAsync();
        Task<ResponseModel> GetActivityByIdAsync(Guid Id);
        Task<ResponseModel> UpdateActivityAsync(ActivityDto activityDto);
        Task<ResponseModel> AddActivityAsync(PostActivityDto postActivityDto);
        Task<ResponseModel> DeleteActivityAsync(Guid Id);
    }
}
