using System;

namespace Jonstructures
{
    public class SinglyLinkedNode<T> where T : IComparable
    {
        public T Item { get; set; }
        public SinglyLinkedNode<T> Next { get; set; }

        public SinglyLinkedNode(T newItem, SinglyLinkedNode<T> next = null)
        {
            Item = newItem;
            Next = next;
        }

        public SinglyLinkedNode<T> AddAfter(T newItem)
        {
            SinglyLinkedNode<T> newNode = new SinglyLinkedNode<T>(newItem, Next);
            Next = newNode;
            return newNode;
        }

        public SinglyLinkedNode<T> RemoveAfter()
        {
            SinglyLinkedNode<T> toRemove = Next;
            Next = toRemove.Next;
            return toRemove;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
