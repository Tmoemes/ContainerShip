using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ContainerShip.UnitTest
{
    [TestClass]
    public class ContainerStackTest
    {
        List<Container> TestContainers = new List<Container>
            {
            new Container(5000, false, false),
            new Container(4000, false, false),
            new Container(6000, false, false),
            new Container(6500, false, false),
            new Container(4500, false, false),
            };


        [TestMethod]
        public void GetWeight_TestStack_26000()
        {
            //arrange  
            ContainerStack TestStack = new ContainerStack(TestContainers);
            
            //act
            int result = TestStack.GetTotalWeight();

            //assert
            Assert.AreEqual(26000, result);

        }
    
        [TestMethod]
        public void GetHeight_TestStack_5()
        {

            ContainerStack TestStack = new ContainerStack(TestContainers);

            int result = TestStack.GetHeight();

            Assert.AreEqual(5, result);
        }
    
    
    }
}
