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
            string query = $"{option}";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection results = searcher.Get();
            Dictionary<string, string> componentInfo = new Dictionary<string, string>();

            foreach (ManagementObject obj in results)
            {             
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
