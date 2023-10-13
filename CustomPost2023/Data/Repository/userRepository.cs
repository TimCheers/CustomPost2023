using CustomPost2023.Data;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CustomPost2023.Data.Repository
{
    public class UserRepository
    {
        private readonly ApplicationContext applicationContext;
        public UserRepository(ApplicationContext appDBContext)
        {
            this.applicationContext = appDBContext;
        }
        public IEnumerable<user> AllUsers => applicationContext.users;
    }
}
