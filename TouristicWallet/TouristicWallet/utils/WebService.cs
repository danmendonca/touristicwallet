using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TouristicWallet.utils
{
    class WebService
    {
        /// <summary>
        /// Web Async Get, as url and a callback as parameters
        /// </summary>
        /// <param name="uri"> url </param>
        /// <param name="onResponse"> callback that will receive the response as string </param>
        public static void CallWebAsync(string uri, Action<String> onResponse)
        {
            var request = HttpWebRequest.Create(uri);
            request.Method = "GET";
            var state = new Tuple<Action<String> , WebRequest>(onResponse, request);

            var cb = new AsyncCallback(CallHandler);
            request.BeginGetResponse(cb, state);
        }

        public static void CallHandler(IAsyncResult ar)
        {
            var state = (Tuple<Action<String>, WebRequest>)ar.AsyncState;
            var request = state.Item2;

            using (HttpWebResponse response = request.EndGetResponse(ar) as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        state.Item1(content);
                    }
            }
        }
    }
}
