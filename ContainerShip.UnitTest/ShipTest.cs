using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerShip.UnitTest
{
    [TestClass]
    public class ShipTest
    {
        static List<Container> TestContainers1 = new List<Container> //30000
        {
            new Container(6000, false, false),
            new Container(5000, false, false),
            new Container(7000, false, false),
            new Container(6500, false, false),
            new Container(5500, false, false),
        };

        static List<Container> TestContainers2 = new List<Container> //25000
        {
            new Container(4000, false, false),
            new Container(4000, false, false),
            new Container(6000, false, false),
            new Container(6000, false, false),
            new Container(5000, false, false),
        };

        static ContainerStack testStack30 = new ContainerStack(TestContainers1);
        static ContainerStack testStack25 = new ContainerStack(TestContainers2);

        private List<List<ContainerStack>> testArea = new List<List<ContainerStack>>
        {
            new List<ContainerStack> {testStack30, testStack25, testStack30},
            new List<ContainerStack> {testStack30, testStack25, testStack30},
            new List<ContainerStack> {testStack25, testStack30, testStack25},
            new List<ContainerStack> {testStack25, testStack30, testStack25},
        };


        [TestMethod]
        public void GetWeightByArea_TestArea_330000()
        {
            
            var testShip = new Ship(testArea,4,3);

            var result = testShip.GetWeightByArea(0,4,0,3);

            Assert.AreEqual(330000,result);

        }

        [TestMethod]
        public void GetWeightByArea_TestArea4x1_110000()
        {

            var testShip = new Ship(testArea,4,3);

            var result = testShip.GetWeightByArea(0, 4, 1, 2);

            Assert.AreEqual(110000, result);

        }

        [TestMethod]
        public void IsLowerHalfLighter_TestArea_False()
        {

            var testShip = new Ship(testArea,testArea.Count,testArea[0].Count);


            var result = testShip.IsLowerHalfLighter();


            Assert.IsFalse(result);
        }

    }

}