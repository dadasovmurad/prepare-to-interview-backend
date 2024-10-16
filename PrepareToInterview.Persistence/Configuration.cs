﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence
{
    public class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();

                try
                {
                    string df = Directory.GetCurrentDirectory();
                    configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()));
                    configurationManager.AddJsonFile("appsettings.json");
                //C: \Users\Murad Dadashov\source\repos\PrepareToInterview\PrepareToInterview.API\PrepareToInterview.API.csproj
                }
                catch(Exception ex)
                {
                    //configurationManager.AddJsonFile("appsettings.Production.json");
                }

                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}