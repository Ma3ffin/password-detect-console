using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PasswordDetect.Controller;
using PasswordDetect.Data;
using PasswordDetect.Handler;
using PasswordDetect.Model;
using PasswordDetect.View;

namespace PasswordDetect
{
    public class Program
    {

        public static void SetupContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleErrorHandler>().As<IErrorHandler>();
            builder.RegisterType<DataAccess>().SingleInstance();
            builder.RegisterType<DetectionContext>().SingleInstance();

            builder.RegisterType<MainView>();

            builder.RegisterType<RegisterView>().As<MenuItem>();
            builder.RegisterType<TrainingView>().As<MenuItem>();
            builder.RegisterType<LoginView>().As<MenuItem>();
            builder.RegisterType<ImportExportView>().As<MenuItem>();


            builder.RegisterType<UserController>().SingleInstance();
            builder.RegisterType<TrainingController>().SingleInstance();
            builder.RegisterType<LoginController>().SingleInstance();
            builder.RegisterType<KeyInputController>().SingleInstance();

            builder.RegisterType<ImportExportJsonController>().As<IImportExportController>().SingleInstance();
            



        Shared.Container = builder.Build();
        }


        static void Main(string[] woller)
        {


            SetupContainer();

            var mainView = Shared.Container.Resolve<MainView>();
            
            mainView.Start();

            
            //RegisterView register = new RegisterView();

            //register.Start();

        }
    }
}
