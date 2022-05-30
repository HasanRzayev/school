using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using school.Models;
using SCHOOL_BUS.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace school
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<SbDbContext>(o => o.UseSqlServer("Source=WIN-EA8010O87DM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddSingleton<WindowViewModel>();
           
            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            
            var view = serviceProvider.GetService<MainWindow>();
            view?.Show();
        }

    }
}
