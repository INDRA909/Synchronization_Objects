using System;
using System.Collections.Generic;

namespace _4._2._1
{
    public class MyQueue<T>
    {
        // Поля
        private T[] _array;
        private int _head;
        private int _tail;

        // Методы
        /// <summary>
        /// Создаём очередь. Ёмкость по умолнчанию - 0;
        /// </summary>
        public MyQueue()
        {
            _array = new T[0];
        }

        /// <summary>
        /// Создаём очередь на основе коллекции.
        /// </summary>
        /// <param name="collection">Исходная коллекция.</param>
        public MyQueue(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            _array = new T[4];
            Count = 0;
            foreach (T variable in collection)
                Enqueue(variable);
        }

        /// <summary>
        /// Создаём очередь с заданной начальной ёмкостью. 
        /// Если количество добавленных элементов превысит заданную ёмкость, то она будет автоматически увеличена.
        /// </summary>
        /// <param name="capacity">Начальная ёмкость.</param>
        public MyQueue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            _array = new T[capacity];
            _head = 0;
            _tail = 0;
            Count = 0;
        }

        /// <summary>
        /// Количество элементов в очереди.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Очистка очереди.
        /// </summary>
        public void Clear()
        {
            if (_head < _tail)
                Array.Clear(_array, _head, Count);
            else
            {
                Array.Clear(_array, _head, _array.Length - _head);
                Array.Clear(_array, 0, _tail);
            }
            _head = 0;
            _tail = 0;
            Count = 0;
        }

        /// <summary>
        /// Проверка на нахождении элемента в очереди.
        /// </summary>
        /// <param name="item">Элемент.</param>
        /// <returns>true, если элемент содержится в очереди.</returns>
        public bool Contains(T item)
        {
            int index = _head;
            int num2 = Count;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            while (num2-- > 0)
            {
                if (item == null)
                {
                    if (_array[index] == null)
                        return true;
                }
                else if ((_array[index] != null) && comparer.Equals(_array[index], item))
                    return true;
                index = (index + 1) % _array.Length;
            }
            return false;
        }

        /// <summary>
        /// Извлечение элемента из очереди.
        /// </summary>
        /// <returns>Извлечённый элемент.</returns>
        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            T local = _array[_head];
            _array[_head] = default(T);
            _head = (_head + 1) % _array.Length;
            Count--;
            return local;
        }

        /// <summary>
        /// Добавление элемента в очередь.
        /// </summary>
        /// <param name="item">Добавляемый элемент.</param>
        public void Enqueue(T item)
        {
            if (Count == _array.Length)
            {
                var capacity = (int)((_array.Length * 200L) / 100L);
                if (capacity < (_array.Length + 4))
                    capacity = _array.Length + 4;
                SetCapacity(capacity);
            }
            _array[_tail] = item;
            _tail = (_tail + 1) % _array.Length;
            Count++;
        }

        /// <summary>
        /// Просмотр элемента на вершине очереди.
        /// </summary>
        /// <returns>Верхний элемент.</returns>
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException();
            return _array[_head];
        }

        // Изменение ёмкости очереди.
        private void SetCapacity(int capacity)
        {
            var destinationArray = new T[capacity];
            if (Count > 0)
            {
                if (_head < _tail)
                    Array.Copy(_array, _head, destinationArray, 0, Count);
                else
                {
                    Array.Copy(_array, _head, destinationArray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, destinationArray, _array.Length - _head, _tail);
                }
            }
            _array = destinationArray;
            _head = 0;
            _tail = (Count == capacity) ? 0 : Count;
        }

        /// <summary>
        /// Преобразование очереди в массив.
        /// </summary>
        /// <returns>Массив с элементами из очереди.</returns>
        public T[] ToArray()
        {
            var destinationArray = new T[Count];
            if (Count != 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_array, _head, destinationArray, 0, Count);
                    return destinationArray;
                }
                Array.Copy(_array, _head, destinationArray, 0, _array.Length - _head);
                Array.Copy(_array, 0, destinationArray, _array.Length - _head, _tail);
            }
            return destinationArray;
        }
    }
}

