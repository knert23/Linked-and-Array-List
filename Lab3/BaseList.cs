using System;

namespace MyBaseList
{
    abstract class BaseList<T> where T : IComparable
    {
        protected int count = 0; // может сделать его private и стучаться до переменной уже спомощью свойства?

        public void CheckIndex(int index, int flag = 0)
        {
            // для случаев, когда вызывается не метод Insert(int index, int data)
            if (flag != 1 && (index >= count || count == 0 || index < 0)) throw new EWrongIndex("Индекс вне диапазона");

            // для случаев, когда вызывается метод Insert(int index, int data)
            if (flag == 1 &&  index > count) throw new EWrongIndex("Индекс вне диапазона");
        }
        public static bool EqualsTo(BaseList<T> list1, BaseList<T> list2)
        {
            //Возможно стоит создать копии, чтобы после применения этого метода, не возвращались отсортированные массивы
            list1.Sort();
            list2.Sort();

            for (int i = 0; i < list1.GetCount; i++)
            {
                if (list1[i].CompareTo(list2[i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }
        public int GetCount { get { return count; } } // количество элементов

        public static BaseList<T> operator +(BaseList<T> list1, BaseList<T> list2)
        {
            BaseList<T> newList = list1.Clone(); // а может проблема в Assign? Возможно следует убрать Clear()?
            // просто почему мы не можем применить Clone() и с помощью Asssing(BaseList sourceList) дописать недостающие элементы?
            for (int i = 0; i < list2.GetCount; i++)
            {
                newList.Add(list2[i]);
            }
            return newList;
        }

        public static bool operator ==(BaseList<T> list1, BaseList<T> list2)
        {
            if (EqualsTo(list1, list2)) return true;
            return false;
        }

        public static bool operator !=(BaseList<T> list1, BaseList<T> list2)
        {
            if (EqualsTo(list1, list2)) return false;
            return true;
        }

        public abstract void Add(T data);
        public abstract void Insert(int index, T data);
        public abstract void Delete(int index);
        public abstract void Clear();
        public abstract T this[int index] { get; set; }
        public abstract void Print();
        public void Assign(BaseList<T> sourceList)
        {
            Clear();
            for (int i = 0; i < sourceList.GetCount; i++)
            {
                Add(sourceList[i]);
            }
        }
        public void AssignTo(BaseList<T> destList)
        {
            destList.Assign(this);
        }
        public abstract BaseList<T> Clone();

        //метод для обмена элементов массива
        private void Swap(int pivot, int index)
        {
            var temp = this[pivot];
            this[pivot] = this[index];
            this[index] = temp;
        }

        //метод возвращающий индекс опорного элемента
        private int Partition(int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (this[i].CompareTo(this[maxIndex]) == -1) // this[i] < this[maxIndex]
                {
                    pivot++;
                    Swap(pivot, i);
                }
            }

            pivot++;
            Swap(pivot, maxIndex);
            return pivot;
        }

        //быстрая сортировка
        private void QuickSort(int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return;
            }

            var pivotIndex = Partition(minIndex, maxIndex);
            QuickSort(minIndex, pivotIndex - 1);
            QuickSort(pivotIndex + 1, maxIndex);
        }

        public virtual void Sort()
        {
            QuickSort(0, this.GetCount - 1);
        }
    }
}