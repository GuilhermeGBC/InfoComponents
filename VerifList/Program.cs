using System;
using System.Collections.Generic;
using System.Management;

namespace SystemInfoExample
{
    class Program
    {
        static void Main(string[] args)
        {
           var option = Menu();
            Dictionary<string, string> myDict = GetInformations(option);
            foreach (var kvp in myDict)
            {
                Console.WriteLine(kvp.Key + ": " + kvp.Value);
            }
        }

        public static string Menu()
        {
            Console.WriteLine("Selecione a opção desejada: ");
            Console.WriteLine("1 - Consultar informações do processador");
            Console.WriteLine("2 - Consultar informações da placa de vídeo");
            Console.WriteLine("3 - Consultar informações da memória RAM");
            int options = int.Parse(Console.ReadLine());
            string option = "";

            switch (options)
            {
                case 1:
                    option = "SELECT * FROM Win32_Processor";
                    break;
                case 2:
                    option = "SELECT * FROM Win32_VideoController";
                    break;
                case 3:
                    option = "SELECT * FROM Win32_PhysicalMemory";
                    break;
                default:
                    break;
            }
            Console.Clear();

            return option;
        }

        public static Dictionary<string, string> GetInformations(string option)
        {
            // Define a consulta WQL para obter informações sobre o componente.
            string query = $"{option}";

            // Cria um objeto ManagementObjectSearcher com a consulta WQL.
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            // Executa a consulta e obtém a coleção de objetos ManagementObject resultantes.
            ManagementObjectCollection results = searcher.Get();

            // Cria um Dictionary para armazenar as informações do processador/componentes.
            Dictionary<string, string> componentInfo = new Dictionary<string, string>();

            // Itera através dos objetos de gerenciamento na coleção de resultados.
            foreach (ManagementObject obj in results)
            {
                // Obtém as propriedades do objeto ManagementObject e as adiciona ao Dictionary.
                foreach (PropertyData property in obj.Properties)
                {
                    string value = property.Value != null ? property.Value.ToString() : "N/A";
                    //string value = property.Value == null ? "N/A" : property.Value.ToString();
                    componentInfo[property.Name] = value;
                }
            }

            return componentInfo;
        }
    }
}
