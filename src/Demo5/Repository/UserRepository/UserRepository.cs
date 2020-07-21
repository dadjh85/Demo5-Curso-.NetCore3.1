
using Database;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        #region Properties

        private readonly DemoContext _demoContext;

        #endregion

        public UserRepository(DemoContext demoContext)
        {
            _demoContext = demoContext ?? throw new ArgumentNullException(nameof(demoContext));
        }

        #region Implementation IUserRepository

       

        #endregion

    }
}
