﻿using ClienteSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1Cliente
{
    class Program
    {
        static void GenerarComunicacion(ClienteSocket clienteSocket)
        {
            Console.Clear(); //limpia la pantalla
            bool terminar = false;
            while (!terminar)
            {
                Console.WriteLine("Ingrese que quiere decir:");
                string mensaje = Console.ReadLine().Trim();
                clienteSocket.Escribir(mensaje);
                if(mensaje.ToLower() == "chao")
                {
                    terminar = true;
                } else
                {
                    mensaje = clienteSocket.Leer();
                    if (mensaje != null)
                    {
                        Console.WriteLine("S:{0}", mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            terminar = true;
                        }
                    } else
                    {
                        terminar = true;
                    }
                }
            }
            clienteSocket.Desconectar();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese servidor");
            string servidor = Console.ReadLine().Trim();
            int puerto;
            do
            {
                Console.WriteLine("Ingrese puerto");
            } while (!Int32.TryParse(Console.ReadLine().Trim(), out puerto));

            Console.WriteLine("Conectando..");
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

            if (clienteSocket.Conectar())
            {
                GenerarComunicacion(clienteSocket);
            } else
            {
                Console.WriteLine("Error de comunicación ");
            }
            Console.ReadKey();

        }
    }
}
