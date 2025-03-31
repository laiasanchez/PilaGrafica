using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilaGrafica
{
    internal class Pila<T> : IEnumerable<T>
    {
        // ATRIBUTS
        private T[] data;
        private const int DEFAULT_SIZE = 5;
        private int top = -1;

        // CONSTRUCTORS
        /// <summary>
        /// Inicialitza una nova pila amb la mida per defecte de 5 elements.
        /// </summary>
        public Pila()
        {
            data = new T[DEFAULT_SIZE];
        }

        /// <summary>
        /// Inicialitza una nova pila amb la mida especificada.
        /// </summary>
        /// <param name="size">La mida de la pila.</param>
        /// <exception cref="ArgumentException">Llançat si la mida és menor o igual a 0.</exception>
        public Pila(int size)
        {
            if (size <= 0) throw new ArgumentException("LA MIDA HA DE SER MAJOR DE 0");
            data = new T[size];
        }

        /// <summary>
        /// Inicialitza una nova pila a partir d'una col·lecció.
        /// </summary>
        /// <param name="coleccio">La col·lecció d'elements a afegir a la pila.</param>
        public Pila(IEnumerable<T> coleccio)
        {
            data = new T[DEFAULT_SIZE];
            foreach (var item in coleccio)
            {
                Push(item);
            }
        }

        // PROPIETATS
        /// <summary>
        /// Obtè un valor que indica si la pila està plena.
        /// </summary>
        public bool IsFull
        {
            get { return top == data.Length - 1; }
        }

        /// <summary>
        /// Obtè un valor que indica si la pila està buida.
        /// </summary>
        public bool IsEmpty
        {
            get { return top == -1; }
        }

        /// <summary>
        /// Obtè el número d'elements actuals a la pila.
        /// </summary>
        public int Count
        {
            get { return top + 1; }
        }

        /// <summary>
        /// Indica si la pila és només de lectura.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Obtè o estableix un element a un índex específic de la pila.
        /// </summary>
        /// <param name="index">L'índex de l'element a obtenir.</param>
        /// <exception cref="NotSupportedException">Llançat si la pila és només de lectura.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Llançat si l'índex està fora de rang.</exception>
        public T this[int index]
        {
            get
            {
                if (IsReadOnly) throw new NotSupportedException("ÉS READ-ONLY");
                if (index >= Count || index < 0) throw new ArgumentOutOfRangeException("AQUEST INDEX NO ESTÀ DINS DE LA LLISTA");
                return data[index];
            }
        }

        /// <summary>
        /// Obtè la capacitat màxima actual de la pila.
        /// </summary>
        public int Capacity
        {
            get { return data.Length; }
        }

        // SUBCLASSES
        /// <summary>
        /// Enumerador per a la pila.
        /// </summary>
        public class EnumeradorPila : IEnumerator<T>
        {
            private int posicio;
            private T[] dades;

            /// <summary>
            /// Inicialitza el enumerador per a la pila.
            /// </summary>
            /// <param name="dades">Els elements de la pila.</param>
            /// <param name="nElem">El nombre d'elements de la pila.</param>
            public EnumeradorPila(T[] dades, int nElem)
            {
                this.dades = dades;
                this.posicio = nElem + 1;
            }

            /// <summary>
            /// Obtè l'element actual de la pila.
            /// </summary>
            public T Current
            {
                get
                {
                    if (posicio < 0) throw new Exception("OUT OF RANGE");
                    return dades[posicio];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            /// <summary>
            /// Allibera els recursos utilitzats pel enumerador.
            /// </summary>
            public void Dispose()
            {
                this.dades = null;
            }

            /// <summary>
            /// Avança el cursor del enumerador una posició.
            /// </summary>
            /// <returns>True si hi ha més elements, de l'altra False.</returns>
            public bool MoveNext()
            {
                posicio--;
                return posicio >= 0;
            }

            /// <summary>
            /// Restableix el cursor a la posició inicial.
            /// </summary>
            public void Reset()
            {
                posicio = dades.Length;
            }
        }

        // MÈTODES
        /// <summary>
        /// Retorna un enumerador per la pila.
        /// </summary>
        /// <returns>Un enumerador per la pila.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeradorPila(this.data, this.top);
        }

        /// <summary>
        /// Retorna un enumerador per la pila (implementació d'IEnumerable).
        /// </summary>
        /// <returns>Un enumerador per la pila.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Afegeix un element a la pila.
        /// </summary>
        /// <param name="item">L'element a afegir a la pila.</param>
        public void Add(T item)
        {
            Push(item);
        }

        /// <summary>
        /// Esborra tots els elements de la pila.
        /// </summary>
        /// <exception cref="NotSupportedException">Si la pila és només de lectura.</exception>
        public void Clear()
        {
            if (IsReadOnly) throw new NotSupportedException("És read-only");

            for (int i = 0; i <= top; i++)
            {
                data[i] = default(T);
            }
            top = -1;
        }

        /// <summary>
        /// Comprova si un element està present a la pila.
        /// </summary>
        /// <param name="item">L'element a cercar a la pila.</param>
        /// <returns>True si l'element es troba a la pila, False en cas contrari.</returns>
        public bool Contains(T item)
        {
            bool trobat = false;
            int index = 0;

            while (index <= top && !trobat)
            {
                if (data[index].Equals(item))
                {
                    trobat = true;
                }
                index++;
            }
            return trobat;
        }

        /// <summary>
        /// Copia els elements de la pila a un altre array començant des de l'índex especificat.
        /// </summary>
        /// <param name="array">L'array de destinació.</param>
        /// <param name="arrayIndex">L'índex des de quan començar a copiar.</param>
        /// <exception cref="ArgumentNullException">Llançat si l'array de destinació és null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Llançat si l'índex de l'array és negatiu.</exception>
        /// <exception cref="ArgumentException">Llançat si l'array no té prou espai per allotjar els elements.</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("No pot ser null");
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException("No pot ser menor de 0");
            if (array.Length - arrayIndex < Count) throw new ArgumentException("L'array no té prou espai");

            IEnumerator<T> enumerador = GetEnumerator();
            while (enumerador.MoveNext())
            {
                array[arrayIndex++] = enumerador.Current;
            }
        }

        /// <summary>
        /// Elimina un element específic de la pila.
        /// </summary>
        /// <param name="item">L'element a eliminar.</param>
        /// <returns>True si l'element s'ha eliminat correctament, False en cas contrari.</returns>
        /// <exception cref="NotSupportedException">Sempre llançat, ja que no es pot eliminar un element específic d'una pila.</exception>
        public bool Remove(T item)
        {
            throw new NotSupportedException("No es pot eliminar un element específic de la pila");
        }

        /// <summary>
        /// Afegeix un element a la pila.
        /// </summary>
        /// <param name="item">L'element a afegir.</param>
        /// <exception cref="StackOverflowException">Si la pila està plena.</exception>
        public void Push(T item)
        {
            if (Count == Capacity) throw new StackOverflowException("La pila està plena");
            data[++top] = item;
        }

        /// <summary>
        /// Elimina i retorna l'element superior de la pila.
        /// </summary>
        /// <returns>L'element superior de la pila.</returns>
        /// <exception cref="InvalidOperationException">Llançat si la pila està buida.</exception>
        public T Pop()
        {
            if (Count == 0) throw new InvalidOperationException("La pila està buida");
            return data[top--];
        }

        /// <summary>
        /// Retorna l'element superior de la pila sense eliminar-lo.
        /// </summary>
        /// <returns>L'element superior de la pila.</returns>
        /// <exception cref="InvalidOperationException">Llançat si la pila està buida.</exception>
        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("La pila està buida");
            return data[top];
        }

        /// <summary>
        /// Converteix la pila en un array.
        /// </summary>
        /// <returns>Un array amb els elements de la pila.</returns>
        public T[] ToArray()
        {
            T[] array = new T[Count];
            CopyTo(array, 0);
            return array;
        }
        /// <summary>
        /// Amplia la capacitat de la pila si la capacitat actual és menor que la nova capacitat especificada.
        /// </summary>
        /// <param name="newCapacity">La nova capacitat desitjada per a la pila.</param>
        /// <returns>La nova capacitat de la pila.</returns>
        public int EnsureCapacity(int novaCapacitat)
        {
            if (novaCapacitat <= Capacity) throw new ArgumentOutOfRangeException("La nova capacitat no pot ser més petita o igual a la capacitat actual");
            T[] novaData = new T[novaCapacitat];
            CopyTo(novaData, 0);
            data = novaData;
            return data.Length;
        }

        /// <summary>
        /// Retorna una representació en cadena de la pila.
        /// </summary>
        /// <returns>Una cadena que representa la pila.</returns>
        public override string ToString()
        {
            StringBuilder sOut = new StringBuilder("[ ");
            IEnumerator<T> enumerador = this.GetEnumerator();

            if (enumerador.MoveNext())
            {
                sOut.Append(enumerador.Current);
                while (enumerador.MoveNext())
                {
                    sOut.Append(", ").Append(enumerador.Current);
                }
            }

            sOut.Append(" ]");
            return sOut.ToString();
        }
    }
}
