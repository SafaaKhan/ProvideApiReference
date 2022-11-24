using ProvideApiReference_Models.DTOs;
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
        Task<IEnumerable<ActivityDto>> GetActivitiesAsync();
        Task<ActivityDto> GetActivityByIdAsync(Guid Id);
        Task<ActivityDto> UpdateActivityAsync(ActivityDto activityDto);
        Task<ActivityDto> AddActivityAsync(PostActivityDto postActivityDto);
        Task<bool> DeleteActivityAsync(Guid Id);
    }
}
