using System.Collections.Generic;

namespace JAAVLTreeLib
{
    public sealed class BinarySearchNode<T> : BinaryNode<T>
    {
        public BinarySearchNode(T key, BinaryNode<T> parent, BinaryNode<T> childA, BinaryNode<T> childB) :
            base(key, parent, childA, childB)
        {
        }

        public BinarySearchNode(T key, BinaryNode<T> parent) : base(key, parent)
        {
        }

        public BinarySearchNode(T key) : base(key)
        {
        }

        public override IEnumerator<T> GetEnumerator()
        {
            if (this.ChildA != null)
            {
                foreach (T v in this.ChildA)
                {
                    yield return v;
                }
            }

            yield return this.Key;

            if (this.ChildB != null)
            {
                foreach (T v in this.ChildB)
                {
                    yield return v;
                }
            }
        }
    }
}
