using FluentAssertions;
using ToolLogicLibrary;

namespace ToolLibraryTests
{
    [TestClass]
    public class SqlLogicTests
    {
        private static string _serverFromFile = "";

        [ClassInitialize]
#pragma warning disable IDE0060 // Nicht verwendete Parameter entfernen
        public static void ClassInitialize(TestContext context)
#pragma warning restore IDE0060 // Nicht verwendete Parameter entfernen
        {
            string[] connectionFileData = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/InventoryManagementTool/TestConfig.txt");
            _serverFromFile = connectionFileData[0];
        }

        [TestMethod]
        public void ConnectToServerAndGetListOfDatabases()
        {
            List<string> databases = SqlLogic.GetDatabasesFromServer(_serverFromFile);
            Assert.IsNotNull(databases);
            Assert.IsTrue(databases.Count > 0);
        }

        [TestMethod]
        public void CreateDatabaseAndExistsAndCorrectStructure()
        {
            DateTime created = DateTime.Now;
            string newDatabase = $"Test_IMT_{created:yyyy_MM_dd_HH_mm_ss_fff}";
            List<string> tables = ["Adjustment", "Area", "Product"];

            SqlLogic.CreateDatabase(_serverFromFile, newDatabase);

            List<string> databases = SqlLogic.GetDatabasesFromServer(_serverFromFile);
            Assert.IsTrue(databases.Contains(newDatabase));

            SqlLogic.GetTablesFromDatabase(_serverFromFile, newDatabase).Should().BeEquivalentTo(tables);
        }

        [TestMethod]
        public void CreateExistingDatabaseAndException()
        {
            DateTime created = DateTime.Now;
            string newDatabase = $"Test_IMT_{created:yyyy_MM_dd_HH_mm_ss_fff}";
            SqlLogic.CreateDatabase(_serverFromFile, newDatabase);

            Action act = () => SqlLogic.CreateDatabase(_serverFromFile, newDatabase);

            act.Should().Throw<ArgumentException>().WithMessage($"Database \"{newDatabase}\" already exist");
        }
    }
}