using System;
using System.Collections.Generic;

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

        void Prestamo(Node min)
        {
            Node p = Padre(Root, min);
            int posc = Array.IndexOf(p.Children, min);
            bool borrowed = false;
            //si no es el ultimo hijo
            if (posc + 1 < max)
            {
                //si el hermano existe
                if (p.Children[posc + 1] != null)
                {
                    //si hay más del minimo en el hermano
                    if (p.Children[posc + 1].Count > this.min)
                    {
                        //bajo raiz
                        min.Keys[min.Count] = p.Keys[posc];
                        //subo menor del hermano mayor
                        p.Keys[posc] = p.Children[posc + 1].Keys[0];
                        //reordenamos hermano mayor 
                        for (int i = 0; i < p.Children[posc + 1].Count; i++)
                        {
                            p.Children[posc + 1].Keys[i] = p.Children[posc + 1].Keys[i + 1];
                        }
                        //hijo menor a hijo mayor
                        min.Children[min.Count + 1] = p.Children[posc + 1].Children[0];
                        //reordenamos hijos del hermano
                        for (int i = 0; i < p.Children[posc + 1].Count + 1; i++)
                        {
                            p.Children[posc + 1].Children[i] = p.Children[posc + 1].Children[i + 1];
                        }
                        //acualizamos contadores
                        p.Children[posc + 1].Count--;
                        min.Count++;
                        borrowed = true;
                    }
                }
            }
            // si no es el primer hijo
            if (posc - 1 >= 0 && !borrowed)
            {
                //si hay más del minimo en el hermano
                if (p.Children[posc - 1].Count > this.min)
                {
                    //acomodar nodo
                    for (int i = min.Count; i >= 0; i--)
                    {
                        min.Keys[i + 1] = min.Keys[i];
                    }
                    //bajo raiz
                    min.Keys[0] = p.Keys[posc - 1];
                    //subo mayor del hermano menor
                    p.Keys[posc - 1] = p.Children[posc - 1].Keys[p.Children[posc - 1].Count - 1];
                    p.Children[posc - 1].Keys[p.Children[posc - 1].Count - 1] = default;
                    //reordenamos hijos
                    for (int i = min.Count; i <= 0; i--)
                    {
                        min.Children[i + 1] = min.Children[i];
                    }
                    //hijo mayor a hijo menor
                    min.Children[0] = p.Children[posc - 1].Children[p.Children[posc - 1].Count];
                    //actualizar contador
                    p.Children[posc - 1].Count--;
                    min.Count++;
                    borrowed = true;
                }
            }
            //si ninguno le puede prestar, se une 
            if (!borrowed)
            {
                //unión izquierda
                // si no es el primer hijo
                if (posc - 1 >= 0)
                {
                    //acomodar nodo
                    for (int i = 0; i < this.min; i++)
                    {
                        min.Keys[i + p.Children[posc - 1].Count + 1] = min.Keys[i];
                    }
                    //acomodar hijos
                    for (int i = 0; i < this.min + 1; i++)
                    {
                        min.Children[i + p.Children[posc - 1].Count] = min.Children[i];
                    }
                    //bajar raiz
                    min.Keys[p.Children[posc - 1].Count] = p.Keys[posc - 1];
                    //actualizar contadores
                    min.Count++;
                    //acomodar hermano en nodo
                    for (int i = 0; i < p.Children[posc - 1].Count; i++)
                    {
                        min.Keys[i] = p.Children[posc - 1].Keys[i];
                        min.Count++;
                    }
                    //vaciar posicion hermano
                    p.Children[posc - 1] = default;
                    //acomodar padre
                    if (p.Count == 0 && p == Root)
                    {
                        Root = min;
                    }
                    else
                    {
                        if (p.Count < this.min && p != Root)
                        {
                            Prestamo(p);
                        }
                        else
                        {
                            for (int i = posc - 1; i < p.Count; i++)
                            {
                                p.Keys[i] = p.Keys[i + 1];
                            }
                            //acomodar hijos padre
                            for (int i = posc - 1; i < p.Count + 1; i++)
                            {
                                p.Children[i] = p.Children[i + 1];
                            }
                        }
                    }
                    p.Count--;
                }
                //unión derecha
                else
                {
                    //bajar raiz
                    min.Keys[min.Count] = p.Keys[posc];
                    //actualizar contadores
                    min.Count++;
                    //acomodar hermano en nodo
                    for (int i = 0; i < p.Children[posc + 1].Count; i++)
                    {
                        min.Keys[min.Count] = p.Children[posc + 1].Keys[i];
                        min.Count++;
                    }
                    //vaciar posicion hermano
                    p.Children[posc + 1] = default;
                    //acomodar padre
                    if (p.Count == 0 && p == Root)
                    {
                        Root = min;
                    }
                    else
                    {
                        for (int i = posc; i < p.Count; i++)
                        {
                            p.Keys[i] = p.Keys[i + 1];
                        }
                        //acomodar hijos padre
                        for (int i = posc + 1; i < p.Count + 1; i++)
                        {
                            p.Children[i] = p.Children[i + 1];
                        }
                        p.Count--;
                        if (p.Count < this.min && p != Root)
                        {
                            Prestamo(p);
                        }
                    }
                }
            }
        }
        //limpia todo el árbol
        public void Clean()
        {
            Root = null;

        }

        //recorridos
        public List<T> InOrder()
        {
            List<T> val = new List<T>();
            InOrder(Root, val);
            return val;
        }
        void InOrder(Node raiz, List<T> val)
        {
            int i;
            for (i = 0; i < raiz.Count; i++)
            {
                if (raiz.Children[i] != null)
                {
                    InOrder(raiz.Children[i], val);
                }
                val.Add(raiz.Keys[i]); 
            }
            if (raiz.Children[i] != null)
                InOrder(raiz.Children[i], val);
        }

        public List<T> PostOrder()
        {
            List<T> val = new List<T>();
            PostOrder(Root, val);
            return val;
        }
        void PostOrder(Node raiz, List<T> val)
        {
            int i;
            for (i = 0; i < raiz.Count; i++)
            {
                if (raiz.Children[i] != null)
                {
                    PostOrder(raiz.Children[i], val);
                }
            }
            if (raiz.Children[i] != null)
            {
                InOrder(raiz.Children[i], val);
            }
           val.Add(raiz.Keys[i]);
        }

        public List<T> PreOrder()
        {
            List<T> val = new List<T>();
            PreOrder(Root, val);
            return val;
        }
        void PreOrder(Node raiz, List<T> val)
        {
            int i;
            for (i = 0; i < raiz.Count; i++)
            {
                val.Add(raiz.Keys[i]);
                if (raiz.Children[i] != null)
                {
                    PostOrder(raiz.Children[i], val);
                }
            }
            if (raiz.Children[i] != null)
            {
                InOrder(raiz.Children[i], val);
            }
        }
    }
}

