using System;

namespace JAAVLTreeLib
{
    public sealed class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>, IEquatable<T>
    {
        public override BinaryNode<T> Insert(T key)
        {
            if (this.Root == null)
            {
                this.Root = new BinarySearchNode<T>(key);
                this.Count++;
                return this.Root;
            }

            BinaryNode<T> currentNode = this.Root;
            while (true)
            {
                int compare = Compare(key, currentNode.Key);

                if (compare < 0)
                {
                    if (currentNode.ChildA == null)
                    {
                        currentNode.ChildA = new BinarySearchNode<T>(key, currentNode);
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
                    currentNode.ChildB = new BinarySearchNode<T>(key, currentNode);
                    this.Count++;
                    return currentNode.ChildB;
                }

                currentNode = currentNode.ChildB;
            }
        }

        public override bool Remove(T item)
        {
            if (this.Root == null)
            {
                return false;
            }

            BinaryNode<T> node = Search(item);
            if (node == default(BinaryNode<T>))
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

            BinaryNode<T> successer;
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

        public override BinaryNode<T> Search(T key)
        {
            BinaryNode<T> currentNode = this.Root;
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

            return default(BinaryNode<T>);
        }
    }
}
