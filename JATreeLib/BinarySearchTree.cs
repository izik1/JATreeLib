using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAAVLTreeLib
{
    public sealed class BinarySearchTree<T> : ICollection<T> where T : IComparable<T>, IEquatable<T>
    {
        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public Node<T> Root { get; private set; }

        public void Add(T item) => Insert(item);

        public void Clear()
        {
            this.Root = null;
            this.Count = 0;
        }

        public bool Contains(T item) => Search(item) != null;

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (arrayIndex + this.Count > array.Length)
            {
                throw new ArgumentException("The number of elements in the source" + nameof(BinarySearchTree<T>) +
                    "is greater than the available space from" + nameof(arrayIndex) +
                    "to the end of the destination " + nameof(array) + ".");
            }

            int i = 0;
            foreach (T item in this)
            {
                array[arrayIndex + i] = item;
                i++;
            }
        }

        public IEnumerator<T> GetEnumerator() => this.Root.GetEnumerator();

        public Node<T> Insert(T value)
        {
            if (this.Root == null)
            {
                this.Root = new Node<T>(value);
                this.Count++;
                return this.Root;
            }

            Node<T> currentNode = this.Root;
            while (true)
            {
                int compare = Compare(value, currentNode.Key);

                if (compare < 0)
                {
                    if (currentNode.ChildA == null)
                    {
                        currentNode.ChildA = new Node<T>(value, currentNode);
                        this.Count++;
                        return currentNode.ChildA;
                    }

                    currentNode = currentNode.ChildA;
                }

                if (compare == 0)
                {
                    return currentNode;
                }

                if (currentNode.ChildB == null)
                {
                    currentNode.ChildB = new Node<T>(value, currentNode);
                    this.Count++;
                    return currentNode.ChildB;
                }

                currentNode = currentNode.ChildB;
            }
        }

        public bool Remove(T item)
        {
            if (this.Root == null)
            {
                return false;
            }

            Node<T> node = Search(item);
            if (node == default(Node<T>))
            {
                return false;
            }

            this.Count--;
            if (node.ChildA == null && node.ChildB == null)
            {
                if (node.Parent == null)
                {
                    this.Root = null;
                }
                else if (node.Parent.ChildA == node)
                {
                    node.Parent.ChildA = null;
                }
                else
                {
                    node.Parent.ChildB = null;
                }

                return true;
            }

            Node<T> successer;
            if (node.ChildA == null || node.ChildB == null)
            {
                successer = node.ChildA ?? node.ChildB;
            }
            else
            {
                successer = node.ChildB;
                while (successer.ChildA != null)
                {
                    successer = successer.ChildA;
                }
            }

            if (node.Parent == null)
            {
                this.Root = successer;
                successer.Parent = null;
            }
            else if (node.Parent.ChildA == node)
            {
                node.Parent.ChildA = successer;
                successer.Parent = node.Parent;
            }
            else
            {
                node.Parent.ChildB = successer;
                node.ChildA.Parent = successer;
            }

            return true;
        }

        public Node<T> Search(T key)
        {
            Node<T> currentNode = this.Root;
            while (currentNode != null)
            {
                int compare = Compare(key, currentNode.Key);

                if (compare < 0)
                {
                    currentNode = currentNode.ChildA;
                }

                if (compare == 0)
                {
                    return currentNode;
                }

                currentNode = currentNode.ChildB;
            }

            return default(Node<T>);
        }

        private static int Compare(T x, T y)
        {
            if (IsDefault(x) && IsDefault(y))
            {
                return 0;
            }

            if (IsDefault(x))
            {
                return 1;
            }

            if (IsDefault(y))
            {
                return -1;
            }

            return x.CompareTo(y);
        }

        private static bool IsDefault(T val) => EqualityComparer<T>.Default.Equals(val, default(T));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
