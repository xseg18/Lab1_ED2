using System;

namespace Parte_1
{
    public class B<T> where T : IComparable
    {
        internal int min;
        internal int max;
        internal Node Root;
        public B(int grade)
        {
            if (grade % 2 == 0)
            {
                throw new Exception("El grado no puede ser par");
            }
            else
            {
                min = (grade / 2);
                Root = new Node(grade);
                max = grade;
            }
        }

        internal class Node
        {
            internal T[] Keys;
            internal Node[] Children;
            internal int Count;
            internal Node(int g)
            {
                Keys = new T[g];
                Children = new Node[g + 1];
                Count = 0;
            }


        }

        //metodo público de inserción
        public void Add(T value)
        {
            if (Root.Count == 0)
            {
                Root.Keys[0] = value;
                Root.Count++;
            }
            else
            {
                Add(value, Root);
            }
        }

        //metodo recursivo
        void Add(T value, Node Raiz)
        {
            //si es una hoja
            if (Raiz.Children[0] == null)
            {
                //si es igual
                for (int i = 0; i < Raiz.Count - 1; i++)
                {
                    if (Raiz.Keys[i].CompareTo(value) == 0)
                    {
                        throw new Exception("Numeros repetidos");
                    }
                }
                //llenar en posición vacía
                Raiz.Keys[Raiz.Count] = value;
                //actualizar contador
                Raiz.Count++;
                //mandar a ordenar
                OrdenarHoja(Raiz);
                //si el nodo se llena, separar
                if (Raiz.Count == max)
                {
                    Separar(Raiz);
                }
            }
            // si no es una hoja
            else
            {
                bool added = false;
                for (int i = 0; i < Raiz.Count; i++)
                {
                    // evalua si el valor es menor a la raiz
                    if (Raiz.Keys[i].CompareTo(value) > 0)
                    {
                        Add(value, Raiz.Children[i]);
                        added = true;
                        break;
                    }
                }
                //si no se ha ingresado y es mayor
                if (!added)
                {
                    Add(value, Raiz.Children[Raiz.Count]);
                }
            }
        }

        void OrdenarHoja(Node Nodo)
        {
            for (int i = 0; i < Nodo.Count - 1; i++)
            {
                for (int j = 0; j < Nodo.Count - i - 1; j++)
                {
                    if (Nodo.Keys[j].CompareTo(Nodo.Keys[j + 1]) > 0)
                    {
                        T prev = Nodo.Keys[j];
                        Nodo.Keys[j] = Nodo.Keys[j + 1];
                        Nodo.Keys[j + 1] = prev;
                    }
                }
            }
        }

        void Separar(Node Nodo)
        {
            //medio de la raiz
            int medio = (max / 2);
            //cuando es raíz
            if (Nodo == Root)
            {
                //nuevos hijos
                Node Izquierdo = new Node(max);
                Node Derecho = new Node(max);
                //llenado de hijos
                for (int i = 0; i < medio; i++)
                {
                    Izquierdo.Keys[i] = Nodo.Keys[i];
                    Izquierdo.Count++;
                    Nodo.Keys[i] = default;
                }
                for (int i = 0; i < medio + 1; i++)
                {
                    Izquierdo.Children[i] = Nodo.Children[i];
                    Nodo.Children[i] = null;
                }
                int pos = 0;
                for (int i = medio + 1; i < max; i++)
                {
                    Derecho.Keys[pos] = Nodo.Keys[i];
                    Derecho.Count++;
                    Nodo.Keys[i] = default;
                    pos++;
                }
                pos = 0;
                for (int i = medio + 1; i < max + 1; i++)
                {
                    Derecho.Children[pos] = Nodo.Children[i];
                    Nodo.Children[i] = null;
                    pos++;
                }
                Nodo.Count = 1;
                Nodo.Keys[0] = Nodo.Keys[medio];
                Nodo.Keys[medio] = default;
                Nodo.Children[0] = Izquierdo;
                Nodo.Children[1] = Derecho;
            }
            //cuando no es raíz
            else
            {
                //encontrar nodo padre
                Node P = Padre(Root, Nodo);
                //posicion hijo/padre
                int pnode = 0;
                for (int i = 0; i < max + 1; i++)
                {
                    if (P.Children[i] == Nodo)
                    {
                        pnode = i;
                        break;
                    }
                }
                //nodo hermano
                Node hermano = new Node(max);
                int pos = 0;
                for (int i = medio + 1; i < max; i++)
                {
                    hermano.Keys[pos] = Nodo.Keys[i];
                    Nodo.Keys[i] = default;
                    hermano.Count++;
                    Nodo.Count--;
                    pos++;
                }
                for (int i = P.Count - 1; i >= pnode; i--)
                {
                    P.Keys[i + 1] = P.Keys[i];
                }
                P.Keys[pnode] = Nodo.Keys[medio];
                for (int i = P.Count; i >= pnode + 1; i--)
                {
                    P.Children[i + 1] = P.Children[i];
                }
                P.Children[pnode + 1] = hermano;
                Nodo.Keys[medio] = default;
                Nodo.Count--;
                P.Count++;
                if (P.Count == max)
                {
                    Separar(P);
                }
            }
        }

        Node Padre(Node Raiz, Node Search)
        {
            int i = 0;
            while (Raiz != null && i <= Raiz.Count)
            {
                if (Raiz.Children[i] == Search || Raiz.Children[i + 1] == Search)
                {
                    return Raiz;
                }
                else
                {
                    if (Padre(Raiz.Children[i], Search) != null)
                    {
                        return Raiz.Children[i];
                    }
                }
                i++;
            }
            return null;
        }
        public void Delete(T value)
        {
            //encontramos nodo a eliminar
            Node found = Search(value, Root);
            //posición del valor a eliminar dentro del nodo
            int pos = Array.IndexOf(found.Keys, value);
            //si el nodo es hoja
            if (found.Children[0] == null)
            {
                for (int i = pos; i < found.Count; i++)
                {
                    found.Keys[i] = found.Keys[i + 1];
                }
                found.Count--;
                if (found.Count < min)
                {
                    Prestamo(found);
                }
            }
            //si no es hoja
            else
            {
                //mayor de los menores
                Node change = MayorMenor(found.Children[pos]);
                //cambiar el mayor por eliminado
                found.Keys[pos] = change.Keys[change.Count];
                //vaciar posición en el cambio
                change.Keys[change.Count] = default;
                change.Count--;
                if (change.Count < min)
                {
                    Prestamo(change);
                }
            }
        }
        Node MayorMenor(Node Raiz)
        {
            if (Raiz.Children[0] == null)
            {
                return Raiz;
            }
            else
            {
                return Raiz.Children[Raiz.Count];
            }
        }

        Node Search(T value, Node Raiz)
        {
            int i = 0;
            while (Raiz != null && i <= Raiz.Count)
            {
                if (Raiz.Keys[i].Equals(value))
                {
                    return Raiz;
                }
                else
                {
                    if (Search(value, Raiz.Children[i]) != null)
                    {
                        return Search(value, Raiz.Children[i]);
                    }
                }
                i++;
            }
            return null;
        }

        void Unir(Node min)
        {
            Node p = Padre(Root, min);
            int posc = Array.IndexOf(p.Children, min);

            min.Keys[min.Count] = p.Keys[posc];
            min.Count++;
            p.Count--;
            p.Keys[posc] = default;
            for (int i = 0; i < p.Children[posc + 1].Count; i++)
            {
                min.Keys[min.Count] = p.Children[posc + 1].Keys[i];
                min.Count++;
            }
            p.Children[posc + 1] = null;
            if (p.Count == this.min)
            {
                Unir(p);
            }
            if (min.Count == )
        }
    }
}

