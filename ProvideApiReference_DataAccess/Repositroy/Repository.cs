using ProvideApiReference_DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_DataAccess.Repositroy
{
    public class Repository : IRepository.IRepository
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _db.SaveChangesAsync()>0;
        }
    }
}
