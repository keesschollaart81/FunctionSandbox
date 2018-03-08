using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Listeners;
using Microsoft.Azure.WebJobs.Host.Protocols;
using Microsoft.Azure.WebJobs.Host.Triggers;
using uPLibrary.Networking.M2Mqtt;

namespace Company.Function
{
    public static class TimerTrigger
    {
        [FunctionName("TimerTrigger")]
        public static void Run([MqttTrigger]MqttEventArgs mqttEventArgs, TraceWriter log)
        {
            // create client instance 
            MqttClient client = new MqttClient("192.168.2.2");
            try
            {
                // register to message received 
                // client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                string clientId = Guid.NewGuid().ToString();
                client.Connect(clientId, "kees", "");

                // client.Subscribe(new string[] { "cmnd/keukenlamp/POWER" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            }
            finally
            {
                client.Disconnect();
            }
            Console.WriteLine("test2");
            // Console.ReadLine();
        }
    }
    // public interface IMqttProcessorFactory
    // {
    //     MqttProcessor CreateMqttProcessor(MqttProcessorFactoryContext context);
    // }
    // internal class DefaultMqttProcessorFactory : IMqttProcessorFactory
    // {
    //     public MqttProcessor CreateMqttProcessor(MqttProcessorFactoryContext context)
    //     {
    //         return new MqttProcessor(context);
    //     }
    // }
 public class MqttEventArgs : EventArgs
    {
      
   public MqttEventArgs( ){}
  }
    public class MqttProcessor
    { }

    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public class MqttTriggerAttribute : Attribute
    {
    }
    internal class MqttTriggerBinding : ITriggerBinding
    {
        public Type TriggerValueType => throw new NotImplementedException();

        public IReadOnlyDictionary<string, Type> BindingDataContract => throw new NotImplementedException();

        public Task<ITriggerData> BindAsync(object value, ValueBindingContext context)
        {
            throw new NotImplementedException();
        }

        public Task<IListener> CreateListenerAsync(ListenerFactoryContext context)
        {
            throw new NotImplementedException();
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            throw new NotImplementedException();
        }
    }

    internal class MqttTriggerAttributeBindingProvider : ITriggerBindingProvider
    {
        public Task<ITriggerBinding> TryCreateAsync(TriggerBindingProviderContext context)
        {
            throw new NotImplementedException();
        }
    }
    public class Test : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            Console.WriteLine("test");
            var rule2 = context.AddBindingRule<FileTriggerAttribute>();
            rule2.BindToTrigger(new MqttTriggerAttributeBindingProvider());

        }
    }
}
