using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace RepositoryADO
{
    public class UserAccountOperations
    {

        private ManasamudramEntities dbContext = new ManasamudramEntities();

        public bool UserExists(App_Users user)
        {
            if (dbContext.App_Users.Any(u => u.UserName == user.UserName))
            {
                return true;
            }
                
            else
            {
                dbContext.App_Users.Add(user);
                dbContext.SaveChanges();
                return false;
            }

        }
    }
}
