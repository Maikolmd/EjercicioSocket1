using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServidorSocketUtils
{
   public class ServerSocket
    {
        private int puerto;
        private Socket servidor;
        /// <summary>
        /// Para construir el Servidor socket, requiere de un puerto
        /// </summary>
        /// <param name="puerto"></param>
        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
        }

        /// <summary>
        /// Inicia la conexión del servidor, tomando posesión del puerto
        /// </summary>
        /// <returns>true en caso de conexión exitosa, false en caso contrario</returns>
        public bool Iniciar()
        {
            try
            {
                this.servidor = new Socket(AddressFamily.InterNetwork
                    , SocketType.Stream, ProtocolType.Tcp);
                //tomar posesión del puerto
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));
                //definir cola de espera (acuerdese el chevasco)
                this.servidor.Listen(10);

                return true;
            }catch(SocketException ex)
            {
                return false;
            }
        }

        public Socket ObtenerCliente()
        {
            return this.servidor.Accept();
        }

        public void Cerrar()
        {
            try
            {
                this.servidor.Close();
            }catch(Exception ex)
            {

            }
        }

    }
}
