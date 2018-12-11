using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebServiceHelper
{
    public static class ContextExtensions
    {
        public static string DefaultContentType { get; set; }
        public static Encoding DefaultEncoding { get; set; }

        static ContextExtensions()
        {
            DefaultContentType = "text/plain";
            DefaultEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// 기본 값으로 Response
        /// </summary>
        /// <param name="self"></param>
        /// <param name="content"></param>
        public static void Response(this HttpContext self, object content)
        {
            self.Response(content, DefaultContentType, DefaultEncoding);
        }

        /// <summary>
        /// 설정 값으로 Response
        /// </summary>
        /// <param name="self"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <param name="encoding"></param>
        public static void Response(this HttpContext self, object content, string contentType, Encoding encoding)
        {
            self.Response.Clear();
            self.Response.ContentType = contentType;
            self.Response.Charset = encoding.BodyName;
            self.Response.ContentEncoding = encoding;
            if (self.Request.Headers.AllKeys.Contains("Accept-Encoding"))
            {
                string acceptEncoding = self.Request.Headers["Accept-Encoding"];
                if (acceptEncoding.Contains("gzip"))
                {
                    self.Response.Headers.Add("Content-Encoding", "gzip");
                    self.Response.Filter = new GZipStream(self.Response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("deflate"))
                {
                    self.Response.Headers.Add("Content-Encoding", "deflate");
                    self.Response.Filter = new DeflateStream(self.Response.Filter, CompressionMode.Compress);
                }
            }
            self.Response.Write(content);
            self.Response.Flush();
        }
    }
}
