using System;
using System.Collections;
using System.Collections.Generic;

namespace Jonstructures
{
    public class CircularLinkedList<T> : IEnumerable<T>, ICollection<T> where T : IComparable
    {
        public CircularNode<T> Head;

        public CircularNode<T> Tail => Head.Previous;

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public CircularLinkedList()
        {
            Head = null;
            Count = 0;
        }

        public CircularNode<T> Add(T item)
        {
            if (Head == null)
            {
                Head = new CircularNode<T>(item);
                Count++;
                return Head;
            }
            var current = Head;
            int position = 0;
            while (position < Count - 1)
            {
                current = current.Next;
                position++;
            }
            Count++;
            current.AddAfter(item);
            return current.Next;
        }

        CircularNode<T> nodeAt(int index)
        {
            var current = Head;
            int position = 0;
            while (position < Count)
            {
                if (position == index)
                {
                    return current;
                }
                current = current.Next;
                position++;
            }
            return current;
        }

        public CircularNode<T> AddToFront(T item)
        {
            if (Head == null)
            {
                Head = new CircularNode<T>(item);
                Count++;
                return Head;
            }
            return Head.AddBefore(item);

        }

        public CircularNode<T> AddAt(int index, T item)
        {
            Count++;
            return nodeAt(index).AddBefore(item);
        }

        public T ElementAt(int index)
        {
            return nodeAt(index).Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            for (int i = 0; i < Count; i++)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var current = Head;
            for (int i = 0; i < Count; i++)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            foreach (var current in this)
            {
                if (item.CompareTo(current) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            array = new T[Count - arrayIndex];
            var current = Head;
            for (int i = 0; i < Count; i++)
            {
                if (i >= arrayIndex)
                {
                    array[i - arrayIndex] = current.Item;
                }
                current = current.Next;
            }
        }

        public bool Remove(T item)
        {
            var current = Head;
            int position = 0;
            while (position < Count)
            {
                if (current.Item.CompareTo(item) == 0)
                {
                    current.Previous.RemoveAfter();
                    Count--;
                    return true;
                }
                current = current.Next;
                position++;
            }
            return false;
        }

        public CircularNode<T> RemoveHead()
        { 
            if(Count == 0)
            {
                throw new InvalidOperationException("No head to remove!");
            }
            var oldHead = Head;
            if(Count == 1)
            {
                Head = null;
                Count = 0;
            }
            else
            {
                var next = Head.Next;
                var previous = Tail;
                next.Previous = previous;
                previous.Next = next;
                Head = next;
                Count--;
            }
            return oldHead;
        }

        public CircularNode<T> RemoveTail()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("No head to remove!");
            }
            var oldTail = Tail;
            if (Count == 1)
            {
                Head = null;
                Count = 0;
            }
            else
            {
                var next = Head;
                var previous = Tail.Previous;
                next.Previous = previous;
                previous.Next = next;
                Count--;
            }
            return oldTail;
        }

        public int GetIndexOf(T item)
        {
            int position = 0;
            foreach (var current in this)
            {
                if (item.CompareTo(current) == 0)
                {
                    return position;
                }
                position++;
            }
            return -1;
        }

        public bool AddRange(ICollection<T> items)
        {
            int initialLength = Count;
            foreach(var item in items)
            {
                Add(item);
            }
            return initialLength != Count;
        }
    }
}
