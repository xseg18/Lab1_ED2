using System;
using System.Collections.Generic;
using Parte_1;

namespace Parte_2
{
    class Program
    {
        static void Main(string[] args)
        {
        //    B<int> BTree;
        //    Console.WriteLine("Laboratorio 1 - Parte II");
        //    Console.WriteLine("");
        //Grado:
        //    Console.WriteLine("Indique el grado del árbol (Ej. 5)");
        //    try
        //    {
        //        int grado = Convert.ToInt32(Console.ReadLine());
        //        if (grado % 2 != 0)
        //        {
        //            BTree = new(grado);
        //            Console.Clear();
        //        }
        //        else
        //        {
        //            Console.Clear();
        //            Console.WriteLine("El grado solo puede ser un número impar, por favor intente nuevamente");
        //            goto Grado;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("El grado solo puede ser un número impar, por favor intente nuevamente");
        //        goto Grado;
        //    }

        //Menu:
        //    Console.WriteLine("¿Qué acción desea realizar?");
        //    Console.WriteLine("1)   Añadir");
        //    Console.WriteLine("2)   Eliminar");
        //    Console.WriteLine("0)   Salir");
        //    try
        //    {
        //        int sele = Convert.ToInt32(Console.ReadLine());
        //        Console.Clear();
        //        switch (sele)
        //        {
        //            case 0:
        //                break;
        //            case 1:
        //            Add:
        //                Console.WriteLine("Ingrese el número entero que desea agregar al árbol (Ej. 12)");
        //                try
        //                {
        //                    BTree.Add(Convert.ToInt32(Console.ReadLine()));

        //                Optio:
        //                    Console.Write("¿Desea agregar otro valor? S/N");
        //                    string optio = Console.ReadLine().ToUpper();
        //                    switch (optio)
        //                    {
        //                        case "S":
        //                            Console.Clear();
        //                            goto Add;
        //                        case "N":
        //                            Console.Clear();
        //                            goto Menu;
        //                        default:
        //                            Console.Clear();
        //                            Console.WriteLine("Selección incorrecta, por favor intente nuevamente");
        //                            goto Optio;
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    Console.Clear();
        //                    Console.WriteLine("El dato a ingresar puede ser únicamente un número entero, por favor intente nuevamente");
        //                    goto Add;
        //                }
        //            case 2:
        //            Delete:
        //                Console.WriteLine("Ingrese el número entero que desea eliminar del árbol (Ej. 12)");
        //                try
        //                {
        //                    int valor = Convert.ToInt32(Console.ReadLine());
        //                    try
        //                    {
        //                        BTree.Delete(valor);
        //                    }
        //                    catch (Exception)
        //                    {
        //                        Console.Clear();
        //                        Console.WriteLine("El dato a eliminar no existe, por favor intente nuevamente");
        //                        goto Delete;
        //                    }

        //                Optio:
        //                    Console.Write("¿Desea eliminar otro valor? S/N");
        //                    string optio = Console.ReadLine().ToUpper();
        //                    switch (optio)
        //                    {
        //                        case "S":
        //                            Console.Clear();
        //                            goto Add;
        //                        case "N":
        //                            Console.Clear();
        //                            goto Menu;
        //                        default:
        //                            Console.Clear();
        //                            Console.WriteLine("Selección incorrecta, por favor intente nuevamente");
        //                            goto Optio;
        //                    }
        //                }
        //                catch (Exception)
        //                {
        //                    Console.Clear();
        //                    Console.WriteLine("El dato a eliminar puede ser únicamente un número entero, por favor intente nuevamente");
        //                    goto Delete;
        //                }
        //            default:
        //                Console.Clear();
        //                Console.WriteLine("Selección incorrecta, por favor intente nuevamente");
        //                goto Menu;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Selección incorrecta, por favor intente nuevamente");
        //        goto Menu;
        //    }

            B<int> Prueba = new(5);
            Prueba.Add(10);
            Prueba.Add(54);
            Prueba.Add(25);
            Prueba.Add(81);
            Prueba.Add(86);
            Prueba.Add(87);
            Prueba.Add(9);
            Prueba.Add(74);
            Prueba.Add(51);
            Prueba.Add(47);
            Prueba.Add(46);
            Prueba.Add(12);
            Prueba.Add(16);
            Prueba.Add(34);
            Prueba.Add(36);
            Prueba.Add(96);
            Prueba.Add(44);
            Prueba.Add(6);
            Prueba.Add(19);
            Prueba.Add(64);
            Prueba.Add(21);
            Prueba.Add(60);
            Prueba.Add(50);
            Prueba.Add(90);
            Prueba.Add(82);
            List<int> p = Prueba.InOrder();
        }
    }
}
