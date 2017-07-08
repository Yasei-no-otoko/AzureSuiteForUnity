using AzureSuiteForUnity.CognitiveServices;
using AzureSuiteForUnity.CognitiveServices.BingSpeech;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoBehaviour = UnityEngine.MonoBehaviour;

namespace AzureSuiteForUnity.CognitiveServices
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                               Component.For<MonoBehaviour>()
                                    .Instance(CognitiveServicesServiceFactory.Instance),
                               Component.For<IBingSpeechAPI>()
                                    .ImplementedBy<BingSpeechAPI>()
                                    .LifeStyle.Transient,
                               Component.For<LoggingInterceptor>().LifeStyle.Transient);
        }
    }

    public class CognitiveServicesServiceFactory : SelfInstantiatingSingletonBehaviour<CognitiveServicesServiceFactory>
    {
        private WindsorContainer container;
        
        public new void Awake()
        {
            base.Awake();
            container = new WindsorContainer();
            container.Install(FromAssembly.This());
        }

        public IBingSpeechAPI GetBingSpeechAPI(string APIKey)
        {
            //ProxyGenerator generator = new ProxyGenerator();
            //BingSpeechAPI service = null;// new BingSpeechAPIService();
                                                //IService proxyService = generator.CreateInterfaceProxyWithTarget<IService>(service, new KeyInterceptor());
                                                //IBingSpeechAPIService proxyService = generator.CreateInterfaceProxyWithTarget<IBingSpeechAPIService>(service, new LoggingInterceptor());

            var api = container.Resolve<IBingSpeechAPI>();
            api.APIKey = APIKey;

            return api;
        }
    }
}
