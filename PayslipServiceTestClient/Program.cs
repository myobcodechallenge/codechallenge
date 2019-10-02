using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PayslipServiceTestClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(@"Please enter csv employee details to calculate payslip 
                                Sample Input: David,Rudd,60050,9%,01 March – 31 March ");
            var input = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input should not be empty. Please enter Input again");
                    input = Console.ReadLine();
                }
                else
                    break;
            }
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var inputData = new { input = "David,Rudd,60050,9%,01 March – 31 March" }; 
                var content = new StringContent(JsonConvert.SerializeObject("David,Rudd,60050,9%,01 March – 31 March"),System.Text.Encoding.UTF8, "application/json");
                var response = client.PostAsync($"https://localhost:44331/api/Payslip/Generatepayslip", content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var payslipdetails = response.Content.ReadAsStringAsync().Result;
                    payslipdetails = JsonConvert.DeserializeObject<string>(payslipdetails);
                    Console.WriteLine($"\nYour Payslip Details : {payslipdetails}");
                }
            }
            Console.ReadLine();
        }
    }
}
