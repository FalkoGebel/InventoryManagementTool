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
    }
}