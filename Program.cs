using System;

namespace Training1
{
    class Program
    {
        static void Main()
        {
            var list = new LinkedList();
            list.AddLast(new StringNode("A"));
            // list.Remove("A");
            list.AddLast(new StringNode("B"));
            list.AddLast(new StringNode("C"));
            list.AddLast(new StringNode("D"));
            list.AddLast(new StringNode("E"));
            list.Insert(5, new StringNode("!!"));

            list.Insert(1, new StringNode("!!"));
            list.Insert(0, new StringNode("!!"));

            Console.WriteLine(list.ToString());
            list.RemoveAt(0);
            Console.WriteLine(list.ToString());
            list.AddLast(new StringNode("??"));
            Console.WriteLine(list.ToString());
            bool result = list.Remove("??S");
            if (result == false)
            {
                Console.WriteLine("Could not remove!");
            }
            Console.WriteLine(list.ToString());
            list.Clear();
            Console.WriteLine(list.ToString());
        }

    }

    class LinkedList
    {

        public StringNode Head { get; private set; }
        public LinkedList()
        {
        }

        private StringNode GetAt(int index)
        {
            if (index < 0 || Count < index) throw new ArgumentOutOfRangeException("index");

            StringNode current = this.Head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }

        private int IndexOf(StringNode node)
        {
            StringNode current = this.Head;
            int index = 0;
            while (index < Count)
            {
                if (current.Equals(node)) return index;
                current = current.Next;
                index++;
            }
            return -1;
        }

        public void AddFirst(StringNode node)
        {
            ValidateNewNode(node);

            if (Head == null)
            {
                InternalInsertNodeToEmptyList(node);
            }
            else
            {
                InternalInsertNodeBefore(Head, node);
                Head = node;
            }
        }
        public void AddLast(StringNode node)
        {
            ValidateNewNode(node);

            if (Head == null)
            {
                InternalInsertNodeToEmptyList(node);
            }
            else
            {
                InternalInsertNodeBefore(Head, node);
            }
        }
        public void Insert(int index, StringNode newNode)
        {
            if (index < 0 || Count < index) throw new IndexOutOfRangeException();

            ValidateNewNode(newNode);
            // Insert node into head
            if (index == 0)
            {
                AddFirst(newNode);
            }
            else
            {
                StringNode node = GetAt(index);
                InternalInsertNodeBefore(node, newNode);
            }
        }

        public bool Remove(string data)
        {
            int index = IndexOf(new StringNode(data));

            if (index == -1)
            {
                return false;
            }
            else
            {
                bool result = RemoveAt(index);
                return result;
            }
        }

        public bool RemoveAt(int index)
        {
            StringNode node;
            try
            {
                node = GetAt(index);
            }
            catch
            {
                return false;
            }
            InternalRemoveNode(node);
            return true;
        }

        public void Clear()
        {
            var current = this.Head;
            while (current != null)
            {
                StringNode temp = current;
                current = current.Next;
                temp.Invalidate();
            }
            Head = null;
            Count = 0;
        }

        // public int Count {get { return 0; }}
        public int Count { get; private set; }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            var currentNode = this.Head;
            for (int i = 0; i < Count; i++)
            {
                sb.Append($"[{currentNode.Data}]->");
                currentNode = currentNode.Next;
            }
            sb.Append("[end]");
            return sb.ToString();
        }

        internal void InternalInsertNodeToEmptyList(StringNode newNode)
        {
            newNode.Next = newNode;
            newNode.Prev = newNode;
            Head = newNode;
            Count = 1;
        }

        internal void InternalInsertNodeBefore(StringNode node, StringNode newNode)
        {
            newNode.Next = node;
            newNode.Prev = node.Prev;
            node.Prev.Next = newNode;
            node.Prev = newNode;
            Count++;
        }

        internal void InternalRemoveNode(StringNode node)
        {
            if (node.Next == node)
            {
                Head = null;
            }
            else
            {
                node.Next.Prev = node.Prev;
                node.Prev.Next = node.Next;
                if (Head == node)
                {
                    Head = node.Next;
                }
            }
            node.Invalidate();
            Count--;
        }
        internal void ValidateNewNode(StringNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
        }
    }

    class StringNode
    {
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
            if (this == obj)
            {
                return true;
            }

            if (!(obj is StringNode))
            {
                return false;
            }

            var other = obj as StringNode;

            if (this.GetHashCode() != other.GetHashCode())
            {
                return false;
            }

            if (!(this.Data.Equals(other.Data)))
            {
                return false;
            }

            return true;
        }

        internal void Invalidate()
        {
            Next = null;
            Prev = null;
        }

        public override string ToString()
        {
            return this.Data;
        }
    }
}
