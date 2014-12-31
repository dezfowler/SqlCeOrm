using SqlCeOrm.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlServerCe;
using System.Collections.Generic;

namespace SqlCeOrm.Test
{
    /// <summary>
    ///This is a test class for SqlCePersistentStoreTest and is intended
    ///to contain all SqlCePersistentStoreTest Unit Tests
    ///</summary>
    [TestClass]
    public class SqlCePersistentStoreTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {

        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for TableMeta
        ///</summary>
        [TestMethod()]
        public void TableMetaTest()
        {
            string connectionString = string.Empty; // TODO: Initialize to an appropriate value
            SqlCePersistentStore target = new SqlCePersistentStore(connectionString); // TODO: Initialize to an appropriate value
            Dictionary<string, SqlCePersistentStore.TableMetaData> actual;
            actual = target.TableMeta;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateAndOpenConnection
        ///</summary>
        [TestMethod()]
        public void CreateAndOpenConnectionTest()
        {
            string connectionString = string.Empty; // TODO: Initialize to an appropriate value
            SqlCePersistentStore target = new SqlCePersistentStore(connectionString); // TODO: Initialize to an appropriate value
            SqlCeConnection expected = null; // TODO: Initialize to an appropriate value
            SqlCeConnection actual;
            actual = target.CreateAndOpenConnection();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CacheMetaData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SqlCeOrm.dll")]
        public void CacheMetaDataTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SqlCePersistentStore_Accessor target = new SqlCePersistentStore_Accessor(param0); // TODO: Initialize to an appropriate value
            target.CacheMetaData();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for BeginSession
        ///</summary>
        [TestMethod()]
        public void BeginSessionTest()
        {
            string connectionString = string.Empty; // TODO: Initialize to an appropriate value
            SqlCePersistentStore target = new SqlCePersistentStore(connectionString); // TODO: Initialize to an appropriate value
            ISession expected = null; // TODO: Initialize to an appropriate value
            ISession actual;
            actual = target.BeginSession();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SqlCePersistentStore Constructor
        ///</summary>
        [TestMethod()]
        public void SqlCePersistentStoreConstructorTest()
        {
            string connectionString = string.Empty; // TODO: Initialize to an appropriate value
            SqlCePersistentStore target = new SqlCePersistentStore(connectionString);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
