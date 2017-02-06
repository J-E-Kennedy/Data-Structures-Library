using System;
using System.Collections;
using System.Collections.Generic;

namespace Jonstructures
{
    public class JLinkedList<T> : IEnumerable<T>, ICollection<T> where T : IComparable
    {
        public SinglyLinkedNode<T> Head { get; set; }

        public SinglyLinkedNode<T> Tail => getElementAt(Count - 1);

        public int Count { get; set; }

        public bool IsReadOnly => false;

        public JLinkedList()
        {
            Head = null;
            Count = 0;
        }

        public SinglyLinkedNode<T> Add(T value)
        {
            if (Head == null)
            {
                Head = new SinglyLinkedNode<T>(value);
                Count++;
                return Head;
            }
            else
            {
                var Current = Head;
                while (Current.Next != null)
                {
                    Current = Current.Next;
                }
                Current.AddAfter(value);
                Count++;
                return Current.Next;
            }
        }

        public SinglyLinkedNode<T> AddToFront(T value)
        {
            Head = new SinglyLinkedNode<T>(value, Head);
            Count++;
            return Head;
        }

        public SinglyLinkedNode<T> RemoveFromFront()
        {
            if (Head == null)
            {
                throw new NullReferenceException("From what front?");
            }
            else
            {
                var toRemove = Head;
                Head = Head.Next;
                Count--;
                return toRemove;
            }
        }

        public SinglyLinkedNode<T> RemoveFromEnd()
        {
            if (Head == null)
            {
                throw new NullReferenceException("There is no end.");
            }
            else if(Head.Next == null)
            {
                Clear();
            }
            var current = Head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }
            var toRemove = current.Next;
            current.Next = null;
            Count--;
            return toRemove;
        }

        public SinglyLinkedNode<T> RemoveAt(int position)
        {
            if (IsEmpty() || position >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (position == 0)
            {
                return RemoveFromFront();
            }
            int currentPosition = 0;
            var precedingNode = Head;
            while (++currentPosition < position)
            {
                precedingNode = precedingNode.Next;
            }
            var toRemove = precedingNode.Next;
            precedingNode.RemoveAfter();
            Count--;
            return toRemove;
        }

        public bool IsEmpty()
        {
            return Head == null;
        }

        public override string ToString()
        {
            string items = "";
            if (Head == null)
            {
                return "Empty";
            }
            var current = Head;
            while (current.Next != null)
            {
                items += current.Item + ", ";
                current = current.Next;
            }
            items += current.Item + ".";
            return items;
        }

        public SinglyLinkedNode<T> RemoveItem(T item, bool allInstances = false)
        {
            var current = Head;
            int position = 0;
            bool itemRemoved = false;
            SinglyLinkedNode<T> lastItemRemoved = null;
            while (current != null && (!itemRemoved || allInstances))
            {
                if (current.Item.CompareTo(item) == 0)
                {
                    lastItemRemoved = RemoveAt(position);
                    itemRemoved = true;
                    position--;
                }
                current = current.Next;
                position++;
            }
            if (itemRemoved == false)
            {
                throw new ArgumentException("Didn't find any " + item);
            }
            return lastItemRemoved;
        }

        public int FindIndexOf(T item)
        {
            var current = Head;
            int position = 0;
            while (current != null)
            {
                if (current.Item.CompareTo(item) == 0)
                {
                    return position;
                }
                current = current.Next;
                position++;
            }
            throw new ArgumentException("Didn't find any " + item);
        }

        public SinglyLinkedNode<T> getElementAt(int element)
        {
            int position = 0;
            var current = Head;
            while (position < Count)
            {
                if (element == position)
                {
                    return current;
                }
                position++;
                current = current.Next;
            }
            throw new ArgumentException("Can't find that");
        }

        public IEnumerator GetEnumerator()
        {
            var current = Head;
            for (int i = 0; i < Count; i++)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
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
            try
            {
                FindIndexOf(item);
                return true;
            }
            catch (ArgumentException e)
            {
                if (e.Message == "Didn't find any " + item)
                {
                    return false;
                }
                throw e;
            }
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
            try
            {
                RemoveItem(item);
                return true;
            }
            catch (ArgumentException e)
            {
                if (e.Message == "Didn't find any " + item)
                {
                    return false;
                }
                throw e;
            }
        }
    }
}
