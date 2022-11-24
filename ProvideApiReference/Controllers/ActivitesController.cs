using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_DataAccess.Repositroy.IRepository;
using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Models;

namespace ProvideApiReference.Controllers
{
    public class ActivitesController : BaseApiController
    {
        private readonly IActivityRepository _activityRepo;
        protected ResponseDto _responseDto { get; set; }

        public ActivitesController(IActivityRepository activityRepo)
        {
            _activityRepo = activityRepo;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> GetActivities()
        {
            try
            {
                IEnumerable<ActivityDto> activityDtos= await _activityRepo.GetActivitiesAsync();
                _responseDto.Result = activityDtos;
            }
            catch(Exception ex)
            {
                _responseDto.IsSeccuss = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpGet("{id}")]
        public async Task<object> GetActivityById(Guid id)
        {
            var activityDto = await _activityRepo.GetActivityByIdAsync(id);
            if (activityDto == null)
            {
                _responseDto.IsSeccuss = false;
                _responseDto.ErrorMessages = new List<string>(){ "activiy object was not found"};
            }
            _responseDto.Result = activityDto;
            return _responseDto;
        }

        [HttpPost]
        public async Task<object> AddActivity(PostActivityDto activity)
        {
            try
            {
               ActivityDto activityDto = await _activityRepo.AddActivityAsync(activity);
                _responseDto.Result = activityDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSeccuss = false;
                _responseDto.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }
            


        [HttpPut]
        public async Task<ActionResult<ActivityDto>> UpdateActivity(ActivityDto activity)//global handling errors
            //for any exception happened + if id was wrong for example, handlign other errors
        {
            return await _activityRepo.UpdateActivityAsync(activity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteActivty(Guid id)
        {
            return await _activityRepo.DeleteActivityAsync(id);
        }
    }
}
