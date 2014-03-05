using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.OrmLite;

namespace Sanjay.Io.UnitTestingRepositories.Tests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        public Container Container { get; set; }
        public IDbConnectionFactory DbFactory { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Container = new Container();
            Container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", false, SqliteDialect.Provider));
            DbFactory = Container.Resolve<IDbConnectionFactory>();

            using (var db = DbFactory.Open())
            {
                db.CreateTable<Customer>(overwrite: true);
                db.Insert(new Customer
                {
                    FirstName = "Sanjay",
                    LastName = "Uttam"
                });
            }
        }

        [TestMethod]
        public void Test_Get_Customer_By_Id()
        {
            var result = new CustomerRepository(DbFactory).Get(1);
            Assert.AreEqual(1, result.Id);
        }
    }
}
