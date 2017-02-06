using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jonstructures
{
    public class JStack<T> : IEnumerable<T> where T : IComparable
    {
        public int Count { get; private set; }
        SinglyLinkedNode<T> top;

        public JStack()
        {
            top = null;
            Count = 0;
        }

        public void Push(T item)
        {
            top = new SinglyLinkedNode<T>(item, top);
            Count++;
        }

        public T Pop()
        {
            if (top == null)
            {
                throw new InvalidOperationException("The stack is empty!");
            }
            var item = top.Item;
            top = top.Next;
            Count--;
            return item;
        }

        public T Peek()
        {
            if (top == null)
            {
                throw new InvalidOperationException("The stack is empty!");
            }
            return top.Item;
        }

        public bool IsEmpty()
        {
            return top == null;
        }

        public override string ToString()
        {
            string items = "";
            foreach (var item in this)
            {
                items += item + " ";
            }
            return items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = top;
            for (int i = 0; i < Count; i++)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var current = top;
            for (int i = 0; i < Count; i++)
            {
                yield return current.Item;
                current = current.Next;
            }
        }
    }
}
