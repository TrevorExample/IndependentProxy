using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfClient1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "HTTPWcfServiceLibrary1.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            Console.WriteLine("enter get data");
            Service1Reference.Service1Client client = new Service1Reference.Service1Client();
            var response = client.GetData(1);
            Console.WriteLine("response from WcfServer " + response);
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service1));
            host.Open();
            Console.WriteLine("Service Hosted Sucessfully");

            Console.ReadLine();
            Task.Run(()=>DoWork());
            Console.WriteLine("do work");

            Console.ReadKey();
        }

        static void DoWork()
        {
            Console.WriteLine("Do work enter");
            Service1Reference.Service1Client client = new Service1Reference.Service1Client();
           
            for (int i =0; i<100; i++)
            {
                //Service1Reference.Service1Client client = new Service1Reference.Service1Client();
                try
                {
                    var response = client.GetData(i);
                    Console.WriteLine("response from WcfServer " + response);
                }
                catch (Exception ex)
                { Console.WriteLine("exception on " + ex);
                    client = new Service1Reference.Service1Client();
                }
            }
          
        }

    }
}
