﻿using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin
{
    public static class BackendSelector
    {
        public static Backend EvaluateBackend()
        {
            // evaluate SP 2019
            if (CheckFarmExists<Backends.Sp2013.Services.SpDataService>(17))
            {
                return Backend.SP2013;
                // commented out until implemented
                // return Backend.SP2019;
            }

            // evaluate SP 2016
            if (CheckFarmExists<Backends.Sp2013.Services.SpDataService>(16))
            {
                return Backend.SP2013;
                // commented out until implemented
                // return Backend.SP2016;
            }

            // evaluate SP 2019
            if (CheckFarmExists<Backends.Sp2013.Services.SpDataService>(15))
            {
                return Backend.SP2013;
            }

            //// evaluate SP 2019
            //if (CheckFarmExists<Backends.Sp2010.Services.SpDataService>(14))
            //{
            //    return Backend.SP2010;
            //}

            //// evaluate SP 2019
            //if (CheckFarmExists<Backends.Sp2007.Services.SpDataService>(12))
            //{
            //    return Backend.SP2007;
            //}

            return Backend.DEMO;
        }

        private static bool CheckFarmExists<T>(int compatibilityLevel) where T : class, IDataService, new()
        {
            string SharePointDllPath = string.Format(
                @"C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\{0}\ISAPI\Microsoft.SharePoint.dll",
                compatibilityLevel
                );


            if (!System.IO.File.Exists(SharePointDllPath))
            {

                return false;
            }

            try
            {
                T dataService = new T();

                var farm = dataService.LoadFarm();

                return farm != null;

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Error when locating farm: {0}", ex.Message);
                return false;

            }
        }
    }
}
