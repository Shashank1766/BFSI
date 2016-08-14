using BidKaro.Common.Common;
using BidKaro.Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace BidKaro.Common
{
    public static class Helpers
    {
        /// <summary>
        /// Gets the linker time.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
        {
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            return localTime;
        }

        /// <summary>
        /// Gets the current assembly ticks.
        /// </summary>
        /// <value>
        /// The current assembly ticks.
        /// </value>
        public static long CurrentAssemblyTicks
        {
            get
            {
                var tt = Assembly.GetExecutingAssembly().GetLinkerTime().Ticks;
                return tt;
            }
        }

        /// <summary>
        /// To the data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            var Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (var item in items)
            {
                var values = new object[Props.Length];
                for (var i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public static void EncryptConnString()
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");
            var section = config.GetSection("connectionStrings");

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }
        }

        public static void EncryptMailSettings()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConfigurationSection section = config.GetSection("system.net/mailSettings/smtp");

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }
        }

        public static int GetLoggedInUserId()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session[SessionNames.SessionClass.ToString()] != null)
            {
                var session = HttpContext.Current.Session[SessionNames.SessionClass.ToString()] as SessionClass;
                var userid = session.UserId;
                return userid;
            }

            return 0;
        }

        /// <summary>
        /// Gets the user_ ip.
        /// </summary>
        /// <returns></returns>
        public static string GetUser_IP()
        {
            var VisitorsIPAddr = string.Empty;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else if (!string.IsNullOrEmpty(HttpContext.Current.Request.UserHostAddress))
                {
                    VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
                }

                if (string.IsNullOrEmpty(VisitorsIPAddr))
                {
                    VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (string.IsNullOrEmpty(VisitorsIPAddr))
                {
                    VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
                }
            }

            return VisitorsIPAddr;
        }

    }

    /// <summary>
    /// The billing system extensions.
    /// </summary>
    public static class BidKaroExtensions
    {
        /*Converts DataTable To List*/
        #region Public Methods and Operators

        /// <summary>
        /// The to list.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="dataTable">The data table.</param>
        /// <returns>
        /// The <see cref="List" />.
        /// </returns>
        public static List<TSource> ToList<TSource>(this DataTable dataTable) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select
                                     new
                                     {
                                         aProp.Name,
                                         Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                                     })
                .ToList();
            var dataTblFieldNames =
                (from DataColumn aHeader in dataTable.Columns
                 select new { Name = aHeader.ColumnName, Type = aHeader.DataType }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            //foreach (var dataRow in dataTable.AsEnumerable().ToList())
            //{
            //    var aTSource = new TSource();
            //    foreach (var aField in commonFields)
            //    {
            //        var propertyInfos = aTSource.GetType().GetProperty(aField.Name);
            //        var value = (dataRow[aField.Name] == DBNull.Value) ? null : dataRow[aField.Name];
            //        propertyInfos.SetValue(aTSource, value, null);
            //    }

            //    dataList.Add(aTSource);
            //}

            return dataList;
        }

        #endregion


    }

    /// <summary>
    /// The log file.
    /// </summary>
    public class LogFile
    {
        #region Public Methods and Operators

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="Message">
        /// The message.
        /// </param>
        /// <param name="MethodName">
        /// The method name.
        /// </param>
        /// <param name="ErrorPageFilePath">
        /// The error page file path.
        /// </param>
        /// <param name="ERPVAlue">
        /// The erpv alue.
        /// </param>
        /// <param name="UserID">
        /// The user id.
        /// </param>
        public static void LogException(
            string Message,
            string MethodName,
            string ErrorPageFilePath,
            int ERPVAlue,
            long UserID)
        {
        }

        /// <summary>
        /// The write log.
        /// </summary>
        /// <param name="TimeDate">
        /// The time date.
        /// </param>
        /// <param name="Message">
        /// The message.
        /// </param>
        public static void WriteLog(DateTime TimeDate, string Message)
        {
            try
            {
                StreamWriter Temp_StreamWriter = null;

                // Temp_StreamWriter = System.IO.File.AppendText(System.AppDomain.CurrentDomain.BaseDirectory + "\\ErrorLog.txt");
                Temp_StreamWriter = File.AppendText("C:\\ErrorLog.txt");
                Temp_StreamWriter.WriteLine(
                    "[" + TimeDate.ToShortDateString() + " - " + TimeDate.Hour.ToString("00") + ":"
                    + TimeDate.Minute.ToString("00") + "] " + "Message: " + Message);
                Temp_StreamWriter.Close();
            }
            catch (Exception ex)
            {
                WriteLog(DateTime.Now, ex.Message);
            }
        }

        #endregion
    }
}