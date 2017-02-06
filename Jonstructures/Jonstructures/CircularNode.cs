using System;

namespace Jonstructures
{
    public class CircularNode<T> where T : IComparable
    {
        public T Item { get; set; }

        public CircularNode<T> Next { get; set; }

        public CircularNode<T> Previous { get; set; }


        public CircularNode(T newItem)
        {
            Item = newItem;
            Next = this;
            Previous = this;
        }

        public CircularNode(T newItem, CircularNode<T> previous, CircularNode<T> next)
        {
            Item = newItem;
            Next = next;
            Previous = previous;
        }

        public CircularNode<T> AddAfter(T newItem)
        {
            CircularNode<T> newNode = new CircularNode<T>(newItem, this, Next);
            Next = newNode;
            return newNode;
        }

        public CircularNode<T> AddBefore(T newItem)
        {
            CircularNode<T> newNode = new CircularNode<T>(newItem, Previous, this);
            Previous = newNode;
            return newNode;
        }

        public CircularNode<T> RemoveAfter()
        {
            CircularNode<T> toRemove = Next;
            Next = toRemove.Next;
            return toRemove;
        }

        public CircularNode<T> RemoveBefore()
        {
            CircularNode<T> toRemove = Previous;
            Previous = toRemove.Previous;
            return toRemove;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

    }
}
