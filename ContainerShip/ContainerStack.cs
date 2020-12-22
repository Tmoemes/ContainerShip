using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace ContainerShip
{
    public class ContainerStack
    {

        public List<Container> Stack { get; private set; } = new List<Container>();

        public ContainerStack(List<Container> containers)
        {
            foreach (var cont in containers)
            {
                Stack.Add(cont);
            }
        }


        public int GetWeight()
        {
            return Stack.Sum(t => t.Weight);
        }

        public int GetHeight() 
        {
            return Stack.Count;
        }

        public void AddToStack(Container container)
        {
            Stack.Add(container);
        }

        public Container GetTopContainer()
        {
            return Stack.Last();
        }

        public Container GetContainerAt(int height)
        {
            return Stack[height];
        }


        public bool CheckPossible(Container container)
        {
            if (Stack.Sum(t => t.Weight) - Stack[0].Weight <= 120000)
            {
                if (!Stack.Last().Valuable) return true;
                else if (Stack.Last().Valuable && container.Valuable) return true;
                else return false;
            }

            return false;
        }

    }
}
