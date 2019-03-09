using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace Web
{
    public static class Util
    {
        public static string GetClientIp(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                return ((RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name]).Address;
            return null;
        }

        public static long GetAudFormat(this DateTime date)
        {
            return long.Parse(date.ToString("yyyyMMddHHmmss"));
        }

        public static string GetReportName(string File)
        {
            Random r = new Random();
            int aleat = r.Next(1, 9999);
            return  System.IO.Path.GetFileNameWithoutExtension(File) + "_" + aleat.ToString();
        }

        public static void MergeCollections<T>(this DbContext db, ICollection<T> source, ICollection<T> destination,
            Func<T, int> getKey, Action<T, T> processOld, Action<T> processNew = null)
            where T : class
        {
            var deleted = source.Where(x => getKey(x) != 0).Select(getKey).ToList();
            destination.Where(x => !deleted.Contains(getKey(x))).ToList().ForEach(x =>
            {
                db.Set<T>().Remove(x);
                destination.Remove(x);
            });

            foreach (var item in destination)
            {
                processOld(source.First(x => getKey(x) == getKey(item)), item);
            }

            source.Where(x => getKey(x) == 0).ToList().ForEach(x =>
            {
                if (processNew != null) processNew(x);
                destination.Add(x);
            });
        }

        public static void ReplaceCollection<T>(this DbContext db, ICollection<T> source, ICollection<T> destination)
            where T : class
        {
            destination.ToList().ForEach(x =>
            {
                db.Set<T>().Remove(x);
                destination.Remove(x);
            });

            source.ToList().ForEach(x =>
            {
                destination.Add(x);
            });
        }

        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                return binaryReader.ReadBytes(file.ContentLength);
            }
        }

        public static void Delete<T>(this DbSet<T> dbset, params object[] keyValues)
            where T : class
        {
            dbset.Remove(dbset.Find(keyValues));
        }
    }
}