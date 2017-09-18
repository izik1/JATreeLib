using System;

namespace JAAVLTreeLib
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>, IEquatable<T>
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
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = new BinarySearchNode<T>(key, currentNode);
                        this.Count++;
                        return currentNode.LeftChild;
                    }

                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (compare == 0)
                {
                    return currentNode; // Allow duplicates?
                }

                if (currentNode.RightChild == null)
                {
                    currentNode.RightChild = new BinarySearchNode<T>(key, currentNode);
                    this.Count++;
                    return currentNode.RightChild;
                }

                currentNode = currentNode.RightChild;
            }
        }

        public override bool Remove(T item) => Remove(Search(item)) != null;

        protected BinaryNode<T> Remove(BinaryNode<T> node)
        {
            if (node == default(BinaryNode<T>))
            {
                return null;
            }

            this.Count--;
            if (node.LeftChild == null && node.RightChild == null)
            {
                if (node.Parent == null)
                {
                    this.Root = null;
                }
                else if (node.Parent.LeftChild == node)
                {
                    node.Parent.LeftChild = null;
                }
                else
                {
                    node.Parent.RightChild = null;
                }

                return node.Parent;
            }

            BinaryNode<T> successer;
            BinaryNode<T> parent;
            if (node.LeftChild == null || node.RightChild == null)
            {
                successer = node.LeftChild ?? node.RightChild;
                parent = node.Parent;
            }
            else
            {
                successer = node.RightChild;
                while (successer.LeftChild != null)
                {
                    successer = successer.LeftChild;
                }

                parent = successer.Parent;
            }

            if (node.Parent == null)
            {
                this.Root = successer;
                successer.Parent = null;
            }
            else if (node.Parent.LeftChild == node)
            {
                node.Parent.LeftChild = successer;
                successer.Parent = node.Parent;
            }
            else
            {
                node.Parent.RightChild = successer;
                node.LeftChild.Parent = successer;
            }

            return parent;
        }

        public override BinaryNode<T> Search(T key)
        {
            if (this.Root == null)
            {
                return default(BinaryNode<T>);
            }

            BinaryNode<T> currentNode = this.Root;
            while (currentNode != null)
            {
                int compare = Compare(key, currentNode.Key);

                if (compare < 0)
                {
                    currentNode = currentNode.LeftChild;
                    continue;
                }

                if (compare == 0)
                {
                    return currentNode;
                }

                currentNode = currentNode.RightChild;
            }

            return default(BinaryNode<T>);
        }
    }
}
