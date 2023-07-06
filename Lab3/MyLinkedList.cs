using System;

namespace MyBaseList
{
    class Node<T> where T : IComparable
    {
        public T Data { set; get; }
        public Node<T>? Next { set; get; }
        public Node(T data, Node<T>? next)
        {
            Data = data;
            Next = next;
        }
    }

    class MyLinkedList<T> : BaseList<T> where T : IComparable
    {
        Node<T> head = null; // объект, который указывает на 1-ый узел в цепочке

        public void AddFirstNode(T data)
        {
            Node<T> newNode = new Node<T>(data, null);
            newNode.Next = head;
            head = newNode;
            count++;
        }

        public override void Add(T data)
        {
            if (head == null)
            {
                AddFirstNode(data);
                return;
            }

            Node<T> newNode = new Node<T>(data, null);
            Node<T> current = GetNode(count - 1); //находим последний элемент
            current.Next = newNode; // присваиваем ссылку последнего узла на новый узел

            count++;
        }

        public override void Insert(int index, T data)
        {
            CheckIndex(index, 1);
            if (head == null || index == 0)
            {
                AddFirstNode(data);
                return;
            }

            if(index == count)
            {
                Add(data);
                return;
            }

            Node<T> previous = GetNode(index - 1); // берем предыдущий узел
            Node<T> current = previous.Next; // берем узел, на место которого надо вставить новый узел - текущий по индексу узел
            Node<T> newNode = new Node<T>(data, current); // в новый узел кладём ссылку на текущий по индексу узел
            previous.Next = newNode; // предыдущему узлу даем ссылку на новый узел, ссылка на текущий по индексу узел перестаёт действовать
            /* или другая реализация
            Node previous = GetNode(index - 1);
            previous.Next = new Node(data, previous.Next);
            */

            count++;

        }

        public override void Delete(int index)
        {
            CheckIndex(index);
            if (index == 0)
            {
                head = GetNode(index + 1);
                count--;
                return;
            }

            Node<T> previous = GetNode(index - 1); // берем узел перед удаляемым
            Node<T> nextNode = previous.Next.Next;// берем узел, следующий за удаляемым
            previous.Next = nextNode; // ссылку с предыдущего бросаем на следующий за удаляемым
            count--;
        }

        private Node<T> GetNode(int index)
        {

            Node<T> current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }

        public override T this[int index]
        {
            get
            {
                CheckIndex(index);

                return GetNode(index).Data;
            }

            set
            {
                CheckIndex(index);

                GetNode(index).Data = value;
            }
        }

        public override void Print()
        {
            Node<T> current = head;
            while (current != null)
            {
                Console.Write($"{current.Data} ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public override void Clear()
        {
            head = null;
            count = 0;
        }

        public override BaseList<T> Clone()
        {
            MyLinkedList<T> linkedList = new MyLinkedList<T>();
            linkedList.Assign(this);
            return linkedList;
        }

        public override void Sort()
        {
            BubbleSort();
        }

        private void SwapLinkedList(Node<T> current, Node<T> currentNext)
        {
            T temp = current.Data;
            current.Data = currentNext.Data;
            currentNext.Data = temp;
        }

        private void BubbleSort()
        {
            for (int i = 0; i < GetCount; i++)
            {
                Node<T> current = head;
                int flag = 0;
                for (int j = 0; j < GetCount - i - 1; j++)
                {
                    if (current.Data.CompareTo(current.Next.Data) == 1)
                    {
                        SwapLinkedList(current, current.Next);
                        flag = 1;
                    }
                    current = current.Next;
                }
                if (flag == 0) break;
            }
        }
    }
}