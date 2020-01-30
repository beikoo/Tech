using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomersRepository  : BaseRepository<Customer>
    {
        public bool Authenticate(string username , string passwordHash)
        {
            var user = GetUserByUsername(username);
            return user == null ? false : user.Password == passwordHash;
        }

        public Customer GetUserByUsername(string username)
        {
          return Context.Customers.Where(c => c.Username == username).FirstOrDefault();
        }
    }
}
