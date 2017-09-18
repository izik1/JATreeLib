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
            this.LeftChild = childA;
            this.RightChild = childB;
        }

        protected BinaryNode(T key, BinaryNode<T> parent)
        {
            this.Key = key;
            this.Parent = parent;
        }

        protected BinaryNode(T key) => this.Key = key;

        public int Balance { get; set; }

        public BinaryNode<T> LeftChild { get; set; }

        public BinaryNode<T> RightChild { get; set; }

        public T Key { get; }

        public BinaryNode<T> Parent { get; set; }

        public abstract IEnumerator<T> GetEnumerator();

        public bool IsLeftChild => this.Parent != null && this.Parent.LeftChild == this;

        public bool IsRightChild => this.Parent != null && this.Parent.RightChild == this;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
