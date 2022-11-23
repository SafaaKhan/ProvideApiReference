using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_Models;

namespace ProvideApiReference.Controllers
{
    public class ActivitesController : BaseApiController
    {
        private readonly ApplicationDbContext _db;

        public ActivitesController(ApplicationDbContext db)//create  repos
        {
           _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await _db.Activities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await _db.Activities.FindAsync(id);
        }
    }
}
