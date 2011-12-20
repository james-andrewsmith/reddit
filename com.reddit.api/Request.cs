using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace com.reddit.api
{
    internal sealed class Request
    {
        internal string Url
        {
            get;
            set;
        }

        internal string Method
        {
            get;
            set;
        }

        internal string Cookie
        {
            get;
            set;
        }

        internal string Content
        {
            get;
            set;
        }

        // captures the caching logic
        // of not hitting any resource 
        // more frequently than every
        // 30 seconds

        // store a hash of the cookie 
        // and the URL in a dictionary
        // and a timestamp of the last 
        // refresh

        /// <summary>
        /// If we are behind a proxy server this object is setup
        /// </summary>
        internal IWebProxy Proxy
        {
            get;
            set;
        }

        /// <summary>
        /// Timeout the request after 30 seconds
        /// </summary>
        private const int RequestTimeout = 1000 * 30;

        internal bool HasRequestBeingMadeWithin30Seconds(string url)
        {
            // check the request is of a type of resource which 
            // has a cache limit of 30 seconds
            throw new NotImplementedException();

            // if so check the last time the resource was accessed            
        }

        /// <summary>
        /// Request a resource from Reddit
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        internal HttpStatusCode Execute(out string json)
        {
            // Default empty Json
            json = "[]";
            string Data = json;
            HttpStatusCode Status = HttpStatusCode.Unused;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.Url);
            if (Proxy != null)
                request.Proxy = Proxy;

            request.ServicePoint.ConnectionLimit = 100;
            request.Timeout = RequestTimeout;
            request.Method = Method;

            if (request is HttpWebRequest && !string.IsNullOrEmpty(Cookie))
            {                
                (request as HttpWebRequest).CookieContainer = new CookieContainer();
                (request as HttpWebRequest).CookieContainer.SetCookies(request.RequestUri, Cookie);
            }

            using (var handle = new ManualResetEvent(false))
            {
                if (!string.IsNullOrEmpty(Content))
                {
                    // set the content type of the posted data
                    request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

                    // Write the XML data to the post form, as part of the 'xml' variable            
                    request.BeginGetRequestStream(ar =>
                    {
                        try
                        {
                            using (var requestStream = request.EndGetRequestStream(ar))
                            using (var writeStream = new StreamWriter(requestStream, Encoding.ASCII))
                            {
                                writeStream.Write(Content);
                            }
                        }
                        catch (Exception exp)
                        {
                            System.Diagnostics.Debug.WriteLine("Request.Execute: " + exp.Message);
                            Console.WriteLine("Request.Execute: " + exp.Message); 
                            throw exp;
                        }
                        finally
                        {
                            handle.Set();
                        }

                    }, new object() /* state */);
                    handle.WaitOne();
                    handle.Reset();
                }

                request.BeginGetResponse(ar =>
                {
                    try
                    {
                        var response = (HttpWebResponse)request.EndGetResponse(ar);

                        using (var receiveStream = response.GetResponseStream())
                        using (var readStream = new StreamReader(receiveStream, Encoding.ASCII))
                        {
                            Data = readStream.ReadToEnd();
                        }

                        Status = response.StatusCode;


                        // Update the cache hash & the time the URL was requested
                        // if the request was successful, that way we don't count 
                        // failed requests in the 30 second limit 
                        // throw new NotImplementedException();

                    }
                    catch (Exception exp)
                    {
                        System.Diagnostics.Debug.WriteLine("Request.Execute: " + exp.Message);
                        Console.WriteLine("Request.Execute: " + exp.Message);
                    }
                    finally
                    {
                        handle.Set();
                    }

                }, new object() /* state */);

                // In case the first timeout doesn't work then move onto then
                // we'll let the wait request handle timeout as well
                handle.WaitOne(RequestTimeout + (RequestTimeout / 10));
            }
            json = Data;
            return Status;
        }

    }
}
