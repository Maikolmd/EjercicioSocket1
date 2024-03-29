﻿using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketServidor
{
    class Program
    {
        /// <summary>
        /// Inicia una conversación con un Cliente hasta que algun
        /// extremo de la comunicación se despida, con un chao
        /// </summary>
        /// <param name="clienteCom"></param>
        static void GenerarComunicacion(ClienteCom clienteCom)
        {
            bool terminar = false;
            while (!terminar)
            {
                string mensaje = clienteCom.Leer();
                if(mensaje != null)
                {
                    Console.WriteLine("C:{0}", mensaje);
                    if(mensaje.ToLower() == "chao")
                    {
                        terminar = true;
                    }else
                    {
                        Console.Write("Ingrese Respuesta:");
                        mensaje = Console.ReadLine().Trim();
                        clienteCom.Escribir(mensaje);
                        if(mensaje.ToLower() == "chao")
                        {
                            terminar = true;
                        }
                    }
                } else
                {
                    terminar = false;
                }
               
            }
            clienteCom.Desconectar();
        }
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Levantando servidor en puerto {0}", puerto);
            ServerSocket serverSocket = new ServerSocket(puerto);

            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("Esperando Clientes..");
                    Socket socket = serverSocket.ObtenerCliente();
                    Console.WriteLine("Cliente recibido");
                    // comunicarse con el cliente
                    ClienteCom clienteCom = new ClienteCom(socket);
                    GenerarComunicacion(clienteCom);
                }
            } else
            {
                Console.WriteLine("Error al tomar posesión del puerto {0}", puerto);
            }
           

        }
    }
}
