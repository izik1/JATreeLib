using System.Collections;
using System.Collections.Generic;

namespace JAAVLTreeLib
{
    public class Node<T> : IEnumerable<T>
    {
        public Node(T key, Node<T> parent, Node<T> childA, Node<T> childB)
        {
            this.Key = key;
            this.Parent = parent;
            this.ChildA = childA;
            this.ChildB = childB;
        }

        public IEnumerator<T> GetEnumerator()
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Node(T key, Node<T> parent)
        {
            this.Key = key;
            this.Parent = parent;
        }

        public Node(T key) => this.Key = key;

        public Node<T> ChildA { get; set; }

        public Node<T> ChildB { get; set; }

        public T Key { get; }

        public Node<T> Parent { get; set; }
    }
}
