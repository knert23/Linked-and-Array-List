using System;

namespace MyBaseList
{
    class MyArrayList<T> : BaseList<T> where T : IComparable
    {
        private T[] arr = null;

        public void Expand()
        {
            if (arr == null)
            {
                arr = new T[1];
                return;
            }

            if (count < arr.Length) return;

            T[] buf = new T[arr.Length * 2];
            Array.Copy(arr, buf, arr.Length);
            arr = buf;
        }

        public override void Add(T data)
        {
            Expand();
            arr[count] = data;
            count++;
        }

        public override void Insert(int index, T data)
        {
            CheckIndex(index, 1);

            if (index == count || count == 0) // если вставляем на последнюю позицию, то просто вызываем метод Add()
            {
                Add(data);
                return;
            }

            Expand(); // проверяем есть ли место в массиве

            // вставляем элемент
            for (int i = count - 1; i >= index; i--)
            {
                arr[i + 1] = arr[i];
            }
            arr[index] = data;

            count++;
        }

        public override void Delete(int index)
        {
            CheckIndex(index);

            // просто переписываем все элементы, кроме удаленного
            T[] newArr = new T[arr.Length - 1];
            for (int i = 0, j = 0; i < arr.Length; i++, j++)
            {
                if (i == index)
                {
                    j--;
                    continue;
                }
                newArr[j] = arr[i];
            }
            arr = newArr;
            count--;
        }

        public override void Clear()
        {
            count = 0;
        }

        public override T this[int index]
        {
            get
            {
                CheckIndex(index);

                return arr[index];
            }
            set
            {
                CheckIndex(index);

                arr[index] = value;
            }
        }

        public override void Print()
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.Write('\n');
        }

        public override BaseList<T> Clone()
        {
            MyArrayList<T> arrayList = new MyArrayList<T>();
            arrayList.Assign(this);
            return arrayList;
        }
    }
}