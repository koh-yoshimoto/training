using System;

namespace Training1
{
    class Program
    {
        static void Main()
        {
            var list = new LinkedList();
            list.Add(new StringNode("A"));
            list.Remove("A");
            // list.Add(new StringNode("B"));
            // list.Add(new StringNode("C"));
            // list.Add(new StringNode("D"));
            // list.Add(new StringNode("E"));
            // list.Insert(0, new StringNode("!!"));
            // Console.WriteLine(list.ToString());
            // list.RemoveAt(3);
            // Console.WriteLine(list.ToString());
            // list.Add(new StringNode("??"));
            // Console.WriteLine(list.ToString());
            // list.Remove("D");
            // Console.WriteLine(list.ToString());
        }

    }

    class LinkedList {

        public StringNode Head { get; private set; }
        public StringNode Tail { get; private set; }
        public LinkedList(){}

        private void ConnectNode(StringNode prevNode, StringNode nextNode)
        {
            //Connect new node to next node
            if (nextNode != null) {
                nextNode.Prev = prevNode;
            }
            prevNode.Next = nextNode;
        }

        private StringNode GetAt(int index) 
        {
            StringNode currentNode = this.Head;
            int i = 0;
            while(currentNode != null) {
                if (i == index) return currentNode;
                currentNode = currentNode.Next;
                i++;
            }
            throw new ArgumentOutOfRangeException();
        }

        private int IndexOf(StringNode node)
        {
            int index = 0;
            StringNode currentNode = this.Head;
            while (currentNode != null) {
                if (currentNode.Equals(node)) return index;
                index++;
                currentNode = currentNode.Next;
            }
            return -1;
        }

        public void Add(StringNode node)
        {
            if (this.Head == null) { 
                this.Head = node;
                this.Count = 1;
                return;
            }
            // Get tail node
            StringNode tailNode = GetAt(Count-1);
            //Connect node to tailNode
            ConnectNode(tailNode, node);
            this.Count++;
        }

        public void Insert(int index, StringNode node)
        {
            // Insert node into Head
            if (index == 0) { 
                node.Next = this.Head;
                if(this.Head != null) {
                    this.Head.Prev = node;
                }
                // Replace 
                this.Head = node;
                this.Count++;
                return;
            }
            // Get previous node of index
            StringNode prevNode = GetAt(index-1);
            // Out of index
            if (prevNode == null) return;
            StringNode nextNode = prevNode.Next;
            ConnectNode(node, nextNode);
            ConnectNode(prevNode, node);
            this.Count++;
        }

        public void Remove(string data)
        {
            int index = IndexOf(new StringNode(data));
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            // Rmove node from Head
            if (index == 0) { 
                if (this.Head.Next != null) {
                    this.Head.Prev = null;
                }
                this.Head = this.Head.Next;
                this.Count--;
                return;
            }
            StringNode currentNode = GetAt(index);
            if(currentNode == null) return;
            ConnectNode(currentNode.Prev, currentNode.Next);
            this.Count--;
        }

        public void Clear()
        {
            var currentNode = this.Head;
            this.Head = null;
            while (currentNode != null) {
                currentNode = currentNode.Next;
                currentNode.Next = null;
                currentNode.Prev = null;
            }
        }

        // public int Count {get { return 0; }}
        public int Count { get; private set; }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            var currentNode = this.Head;
            while(currentNode != null) {
                sb.Append($"[{currentNode.Data}]->");
                currentNode = currentNode.Next;
                }
            sb.Append("[end]");            
            return sb.ToString();
        }
    }
    
    class StringNode {
        public string Data { get; private set; }
        public StringNode Next { get; set; }
        public StringNode Prev { get; set; }

        public StringNode(string data) 
        {
            this.Data = data;
        }

        public override int GetHashCode()
        {
            return this.Data.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(this == obj) {
                return true;
            }

            if(!(obj is StringNode)) {
                return false;
            }

            var other = obj as StringNode;

            if(this.GetHashCode()!=other.GetHashCode()) {
                return false;
            }

            if(!(this.Data.Equals(other.Data))) {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return this.Data;
        }
    }
}
