using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace Sanjay.Io.UnitTestingRepositories
{
    public class CustomerRepository
    {
        private readonly IDbConnectionFactory _dbFactory = null;
        private const string GetCustomerQuery = "SELECT Id, FirstName, LastName FROM Customer WHERE Id = {0}";

        public CustomerRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Customer Get(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Customer>(GetCustomerQuery, id).SingleOrDefault();
            }
        }
    }

    public class Customer
    {
        [Index]
        [AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
