using System;
using System.Collections;
using System.Collections.Generic;

namespace JAAVLTreeLib
{
    public abstract class BinaryTree<T> : ICollection<T> where T : IComparable<T>, IEquatable<T>
    {
        public int Count { get; protected set; }

        public bool IsReadOnly { get; }

        public BinaryNode<T> Root { get; protected set; }

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
                throw new ArgumentException("The number of elements in the source" + this.GetType().FullName +
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

        public abstract BinaryNode<T> Insert(T key);

        public abstract bool Remove(T item);

        public abstract BinaryNode<T> Search(T key);

        protected static int Compare(T x, T y)
        {
            if (IsDefault(x) && IsDefault(y))
            {
                return 0;
            }

            if (IsDefault(x))
            {
                return -1;
            }

            if (IsDefault(y))
            {
                return 1;
            }

            // x isn't null at this point.
#pragma warning disable S3900 // Arguments of public methods should be validated against null
            return x.CompareTo(y);
#pragma warning restore S3900 // Arguments of public methods should be validated against null
        }

        protected static bool IsDefault(T val) => EqualityComparer<T>.Default.Equals(val, default(T));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
