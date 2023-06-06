using System;

namespace cosicascSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var proc = new Procesos();
            proc.tratarInfo();
            //proc.expresionesLambda(150);
            //proc.conectarBD();
            //proc.pruebicasLambda();
            //proc.enviarTeleGram();

            //proc.procesicos();
            //proc.enviarTeleGram();
            proc.PruebasTexto();
        }
    }
}
