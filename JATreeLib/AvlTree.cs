using System;

namespace JAAVLTreeLib
{
    public class AvlTree<T> : BinarySearchTree<T> where T : IComparable<T>, IEquatable<T>
    {
        public override BinaryNode<T> Insert(T key)
        {
            BinaryNode<T> node = base.Insert(key);
            BinaryNode<T> z = node;
            BinaryNode<T> g;
            BinaryNode<T> n;
            for (BinaryNode<T> x = z.Parent; x != null; x = z.Parent)
            {
                if (z.IsRightChild)
                {
                    if (x.Balance > 0)
                    {
                        g = x.Parent;
                        n = z.Balance < 0 ? RotateRightLeft(x, z) : RotateLeft(x, z);
                    }
                    else
                    {
                        if (x.Balance < 0)
                        {
                            x.Balance = 0;
                            break;
                        }

                        x.Balance = 1;
                        z = x;
                        continue;
                    }
                }
                else
                {
                    if (x.Balance < 0)
                    {
                        g = x.Parent;
                        n = z.Balance > 0 ? RotateLeftRight(x, z) : RotateRight(x, z);
                    }
                    else
                    {
                        if (x.Balance > 0)
                        {
                            x.Balance = 0;
                            break;
                        }

                        z = x;
                        x.Balance = -1;
                        continue;
                    }
                }

                n.Parent = g;
                if (g != null)
                {
                    if (g.LeftChild == x)
                    {
                        g.LeftChild = n;
                    }
                    else
                    {
                        g.RightChild = n;
                    }

                    break;
                }

                this.Root = n;
                break;
            }

            return node;
        }

        public override bool Remove(T item)
        {
            BinaryNode<T> node = Search(item);
            if (node == null)
            {
                return false;
            }

            BinaryNode<T> n = Remove(node);
            if (n == null)
            {
                return true;
            }

            BinaryNode<T> g;
            BinaryNode<T> z;
            int b;
            for (BinaryNode<T> x = n.Parent; x != null; x = g)
            {
                g = x.Parent;

                if (n.IsLeftChild)
                {
                    if (x.Balance > 0)
                    {
                        z = x.RightChild;
                        b = z.Balance;
                        if (b < 0)
                        {
                            n = RotateRightLeft(x, z);
                        }
                        else
                        {
                            n = RotateRight(x, z);
                        }
                    }
                    else
                    {
                        if (x.Balance == 0)
                        {
                            x.Balance = 1;
                            break;
                        }

                        n = x;
                        n.Balance = 0;
                        continue;
                    }
                }
                else
                {
                    if (x.Balance < 0)
                    {
                        z = x.LeftChild;
                        b = z.Balance;
                        if (b > 0)
                        {
                            n = RotateLeftRight(x, z);
                        }
                        else
                        {
                            n = RotateLeft(x, z);
                        }
                    }
                    else
                    {
                        if (x.Balance == 0)
                        {
                            x.Balance = -1;
                            break;
                        }

                        n = x;
                        n.Balance = 0;
                        continue;
                    }
                }

                n.Parent = g;
                if (g != null)
                {
                    if (g.LeftChild == x)
                    {
                        g.LeftChild = n;
                    }
                    else
                    {
                        g.RightChild = n;
                    }
                    if (b == 0)
                    {
                        break;
                    }
                }
                else
                {
                    this.Root = n;
                }
            }

            return true;
        }

        private static BinaryNode<T> RotateLeft(BinaryNode<T> root, BinaryNode<T> rootRight)
        {
            BinaryNode<T> tmp = rootRight.LeftChild;
            root.RightChild = tmp;
            if (tmp != null)
            {
                tmp.Parent = root;
            }

            rootRight.LeftChild = root;
            root.Parent = rootRight;

            if (rootRight.Balance == 0)
            {
                root.Balance = 1;
                rootRight.Balance = -1;
            }
            else
            {
                root.Balance = 0;
                rootRight.Balance = 0;
            }

            return rootRight;
        }

        private BinaryNode<T> RotateLeftRight(BinaryNode<T> root, BinaryNode<T> rootLeft)
        {
            BinaryNode<T> y = rootLeft.RightChild;
            BinaryNode<T> yLeft = y.LeftChild;
            rootLeft.RightChild = yLeft;
            if (yLeft != null)
            {
                yLeft.Parent = rootLeft;
            }

            y.LeftChild = rootLeft;
            rootLeft.Parent = y;
            BinaryNode<T> yRight = y.RightChild;
            root.LeftChild = yRight;
            if (yRight != null)
            {
                yRight.Parent = root;
            }

            y.RightChild = root;
            root.Parent = y;

            if (y.Balance > 0)
            {
                root.Balance = -1;
                rootLeft.Balance = 0;
            }

            if (y.Balance == 0)
            {
                root.Balance = 0;
                rootLeft.Balance = 0;
            }
            else
            {
                root.Balance = 0;
                rootLeft.Balance = 1;
            }

            y.Balance = 0;
            return y;
        }

        private BinaryNode<T> RotateRight(BinaryNode<T> root, BinaryNode<T> rootLeft)
        {
            BinaryNode<T> tmp = rootLeft.RightChild;
            root.LeftChild = tmp;
            if (tmp != null)
            {
                tmp.Parent = root;
            }

            rootLeft.RightChild = root;
            root.Parent = rootLeft;

            if (rootLeft.Balance == 0)
            {
                root.Balance = -1;
                rootLeft.Balance = 1;
            }
            else
            {
                root.Balance = 0;
                rootLeft.Balance = 0;
            }

            return rootLeft;
        }

        private static BinaryNode<T> RotateRightLeft(BinaryNode<T> root, BinaryNode<T> rootRight)
        {
            BinaryNode<T> y = rootRight.LeftChild;
            BinaryNode<T> yRight = y.RightChild;
            rootRight.LeftChild = yRight;
            if (yRight != null)
            {
                yRight.Parent = rootRight;
            }

            y.RightChild = rootRight;
            rootRight.Parent = y;
            BinaryNode<T> yLeft = y.LeftChild;
            root.RightChild = yLeft;
            if (yLeft != null)
            {
                yLeft.Parent = root;
            }

            y.LeftChild = root;
            root.Parent = y;

            if (y.Balance > 0)
            {
                root.Balance = -1;
                rootRight.Balance = 0;
            }

            if (y.Balance == 0)
            {
                root.Balance = 0;
                rootRight.Balance = 0;
            }
            else
            {
                root.Balance = 0;
                rootRight.Balance = 1;
            }

            y.Balance = 0;
            return y;
        }
    }
}
