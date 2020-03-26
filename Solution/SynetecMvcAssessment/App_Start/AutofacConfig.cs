using Autofac;
using Autofac.Integration.Mvc;
using InterviewTestTemplatev2.Controllers;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Services;
using System.Web.Mvc;

namespace InterviewTestTemplatev2.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<BonusPoolController>().InstancePerRequest();
            builder.Register(c => new MvcInterviewV3Entities1());
            builder.RegisterType<HrEmployeesRepository>().As<IHrEmployeesRepository>();
            builder.RegisterType<BonusCalculatorService>().As<IBonusCalculatorService>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}