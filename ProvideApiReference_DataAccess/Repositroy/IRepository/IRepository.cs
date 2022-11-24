using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_DataAccess.Repositroy.IRepository
{
    public interface IRepository
    {
        Task<bool> SaveAllAsync();
    }
}
