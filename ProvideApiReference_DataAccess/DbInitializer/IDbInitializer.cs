using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_DataAccess.DbInitializer
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}
