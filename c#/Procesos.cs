using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace cosicascSharp
{
    public class Procesos
    {
        DateTime fechahoraInicio;
        public void ConectarBD()
        {
            string connectionStr = @"Server=NAV2;Database=SaltoPlayground;Integrated Security=True;";
            fechahoraInicio = DateTime.Now;

            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionStr))
                {
                    connection.Open();
                    string qeryStr = "SELECT TOP (50000) * FROM [SaltoPlayground].[dbo].[SALTO SYSTEMS, S_L_$Customer]";
                    int index = 0;

                    var clientes = new List<customer>();
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(qeryStr, connection);
                    using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
                    {
                        customer newCust = new customer();
                        while (reader.Read())
                        {
                            index++;
                            newCust = new customer();
                            newCust.code = reader[1].ToString();
                            newCust.Name = reader[2].ToString();
                            newCust.cptoDecimal = 3 * index;
                            clientes.Add(newCust);
                        }
                    }
                    connection.Close();
                    ExpresionesLambda(clientes);
                    ExpresionesLambda();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void ExpresionesLambda(List<customer> daticos)
        {
            var consulta = daticos.Select(x => x).Where(x => x.Name.Contains("AR"));
            foreach (customer custUd in consulta)
            {
                Console.WriteLine(custUd.code + " " + custUd.Name + " " + custUd.cptoDecimal.ToString());
            }

            decimal decimalCalculado = consulta.Sum(x => x.cptoDecimal);
            decimal decSubConsulta = daticos.Where(cust => cust.Name.Contains("AR")).Sum(cust => cust.cptoDecimal);


            Console.WriteLine("Reg totales:" + consulta.Count());
            Console.WriteLine("total numeros sumado: " + decimalCalculado);
            Console.WriteLine("Numeros sumados ARC: " + decimalCalculado);
            Console.ReadLine();
        }

        public void ExpresionesLambda()
        {
            string[] datos = new string[5];

            datos[0] = "C001";
            datos[1] = "C001";
            datos[2] = "C001";
            datos[3] = "C004";
            datos[4] = "C005";

            var consulta = datos.GroupBy(x => x).Select(y => y.First());


            foreach (string data in consulta)
            {
                Console.WriteLine(data);
            }
            Console.WriteLine("Reg totales:" + consulta.Count());

            int[] arrayMofa = new int[3];
            arrayMofa[0] = 1;
            arrayMofa[1] = 2;
            arrayMofa[2] = 3;

            int x = arrayMofa.Sum();
            Console.WriteLine(x);

            string jiviri = string.Format("{0}m:{1}s", (DateTime.Now - fechahoraInicio).Minutes, (DateTime.Now - fechahoraInicio).Seconds);

            Console.WriteLine(jiviri);
        }

        public void connectWS(string token)
        {
            HttpWebRequest req;
            req = WebRequest.CreateHttp("");
            req.Method = "GET";
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            req.Headers.Add("Authorization", "Bearer " + token);
            req.ContentType = "application/json";
        }


        public void procesicos()
        {
            string rutaEtiqueCancelar = @"\\server20\Tasks\WSNav\Tookane\Etiquetas\EXXXXXX.pdf";

            if (File.Exists(rutaEtiqueCancelar))
            {
                File.Move(rutaEtiqueCancelar, rutaEtiqueCancelar.Replace(".pdf", "_cancel.pdf"));
            };

        }


        static public void PruebicasLambda()
        {

            //string[] textos = {"uno","dos","tres","cuatro"};
            int[] numeros = { 1, 2, 34, 5, 6, 7, 8, 9, 0 };
            //textos.Where(x=> x=="uno").ToList().ForEach(a=> Console.WriteLine(a));
            //numeros.Where((x,y)=> x<=y).ToList().ForEach(a=> Console.WriteLine(a));
            Console.WriteLine(numeros.Count(a => a > 3).ToString());
            Console.WriteLine("terminado");
            Console.ReadLine();
        }

        public void Imprimir()
        {

            string rutaEtiquetaImprimir = @"\\server20\Tasks\WSNav\Tookane\Etiquetas\E352211.pdf";
            string nombrePrint = @"\\pchq0375\UPS";
            string rutaImpresora = "\"" + nombrePrint + "\"";

            string argumentos = @"/s /h /t " + rutaEtiquetaImprimir + " " + @"" + rutaImpresora + "";
            Console.WriteLine(argumentos);

            //File.Copy(rutaEtiquetaImprimir, rutaImpresora);
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe ";
            proc.StartInfo.Arguments = argumentos;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            proc.Start();

            if (proc.HasExited == false)
            {
                proc.WaitForExit(1000);
            }
            proc.EnableRaisingEvents = true;
        }

        public void enviarTeleGram()
        {
            string response = string.Empty;
            string varTExto = "";
            for (int index = 0; index < 100; index++)
            {
                varTExto += "haver ";
            }
            string URLSend = @"https://api.telegram.org/bot867933183:AAG1rfrbsUv4lc965oSYSlZSPEPPWL2NPO8/sendMessage?chat_id=8965025&text=" + @"" + varTExto + "";

            WebResponse webResponse = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URLSend);
            request.Method = "POST";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            var responseReader = new StreamReader(webStream);
            response = responseReader.ReadToEnd();
            responseReader.Close();

        }
    }

    public class customer
    {
        public string code { get; set; }
        public string Name { get; set; }
        public decimal cptoDecimal { get; set; }
    }

}