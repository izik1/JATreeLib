using System.Collections;
using System.Collections.Generic;

namespace JAAVLTreeLib
{
    public abstract class BinaryNode<T> : IEnumerable<T>
    {
        protected BinaryNode(T key, BinaryNode<T> parent, BinaryNode<T> childA, BinaryNode<T> childB)
        {
            this.Key = key;
            this.Parent = parent;
            this.ChildA = childA;
            this.ChildB = childB;
        }

        protected BinaryNode(T key, BinaryNode<T> parent)
        {
            this.Key = key;
            this.Parent = parent;
        }

        protected BinaryNode(T key) => this.Key = key;

        public BinaryNode<T> ChildA { get; set; }

        public BinaryNode<T> ChildB { get; set; }

        public T Key { get; }

        public BinaryNode<T> Parent { get; set; }

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
