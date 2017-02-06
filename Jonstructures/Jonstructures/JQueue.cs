using System;
using System.Collections.Generic;
using System.Collections;

namespace Jonstructures
{
    public class JQueue<T> : IEnumerable<T> where T : IComparable
    {
        CircularLinkedList<T> items;
        public int Count { get; private set; }

        public JQueue()
        {
            items = null;
            Count = 0;
        }

        public virtual void Enqueue(T item)
        {
            if (items == null)
            {
                items = new CircularLinkedList<T>();
                items.Add(item);
            }
            else
            {
                items.Add(item);
            }
            Count++;
        }
        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Cannot dequeue from an empty queue!");
            }
            var item = items.RemoveHead().Item;
            Count--;
            if(Count == 0)
            {
                items = new CircularLinkedList<T>();
            }
            return item;
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var item in items)
            {
                ret += item + " ";
            }
            return ret;
        }


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }
    }
}
