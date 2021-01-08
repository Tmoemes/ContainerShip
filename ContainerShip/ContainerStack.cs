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

        public ContainerStack()
        {
            
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


        public bool CheckWeigth(Container container)
        {
            if (Stack.Count == 0) return true;
            if (Stack.Sum(t => t.Weight) - Stack[0].Weight + container.Weight <= 120)
            {
                if (!Stack.Last().Valuable) return true;
                else if (Stack.Last().Valuable && container.Valuable) return true;
                else return false;
            }

            return false;
        }


        public string GetWeightString()
        {
            var weightstring = "";
            if (Stack.Count == 0) return weightstring;
            foreach (var container in Stack)
            {
                weightstring += container.Weight.ToString();
                weightstring += "-";
            }
            //remove last extra -
            weightstring = weightstring.Remove(weightstring.Length - 1);

            return weightstring;
        }

        public override string ToString()
        {
            var stackstring = "";
            if (Stack.Count == 0) return stackstring; 
            foreach (var container in Stack)
            {
                if (container.Cooled && container.Valuable) stackstring += "4";
                if (container.Cooled && !container.Valuable) stackstring += "3";
                if (!container.Cooled && container.Valuable) stackstring += "2";
                if (!container.Cooled && !container.Valuable) stackstring += "1";
                stackstring += "-";
            }
            //remove last extra -
            stackstring = stackstring.Remove(stackstring.Length - 1);

            return stackstring;
        }
    }
}
