using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_DataAccess.Repositroy.IRepository;
using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Models;
using ProvideApiReference_Models.ValidateModelAttributes;
using ProvideApiReference_Utilities.Helpers;

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
        public async Task<IActionResult> GetActivities()
        {
            return Ok(await _activityRepo.GetActivitiesAsync());
        }

        //here
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityById(Guid id)
        {  
           return Ok(await _activityRepo.GetActivityByIdAsync(id));
        }

       

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddActivity(PostActivityDto activity)
        {
            return Ok(await _activityRepo.AddActivityAsync(activity));
        }
            


        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> UpdateActivity(ActivityDto activity)//global handling errors
            //for any exception happened + if id was wrong for example, handlign other errors
        {
            return Ok(await _activityRepo.UpdateActivityAsync(activity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivty(Guid id)
        {
            return Ok(await _activityRepo.DeleteActivityAsync(id));
        }
    }
}
