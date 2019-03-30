using FCM.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Notifications
{
    public class NotificationFireBase
    {


        public string SendMessage(NotificationParams notification , List<string> registrationIdList )
        {
            var registrationId = "cwPwKvIM2pw:APA91bGMT-7eTQsp5i_8VlTMPG_YjryBXYRsETUl7OkvEvJTTZ6tzsUovT1WDen55NckbRy2kZ45WF2sLaqeTz8JtNeiRvWf7oyr5Hj1Ppz7k1s2Ah8TMS1r1lcCi2DoNOQg3n7mo22b";

            
            string serverKey = "AAAAg6qNHaQ:APA91bHU5M8W4z7dCtms8MvwSOQBPYV1A7Vko-mL3R5KI5KDyRnLN7pS2yNgGF2oahuP-WC9UwxQJI51TnpAvS-OXew9DvE8mS107f_CfOr0tw5_G0uOfiH3cAlikCYdSCDRQ_r0_cy0";

            try
            {
                var result = "-1";
                var webAddr = "https://gcm-http.googleapis.com/gcm/send";
                
                var regID = registrationId;

                //var regID = registrationIdList.ToArray();


                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"to\": \"" + regID + "\",\"notification\": {\"title\": \""+notification.Title+"\",\"body\": \""+notification.Body+"\"},\"priority\":10}";
                    //registration_ids, array of strings -  to, single recipient
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "err";
            }
        }

        //public void Send()
        //{

        //    var registrationId = "cwPwKvIM2pw:APA91bGMT-7eTQsp5i_8VlTMPG_YjryBXYRsETUl7OkvEvJTTZ6tzsUovT1WDen55NckbRy2kZ45WF2sLaqeTz8JtNeiRvWf7oyr5Hj1Ppz7k1s2Ah8TMS1r1lcCi2DoNOQg3n7mo22b";

        //    string serverKey = "AAAAg6qNHaQ:APA91bHU5M8W4z7dCtms8MvwSOQBPYV1A7Vko-mL3R5KI5KDyRnLN7pS2yNgGF2oahuP-WC9UwxQJI51TnpAvS-OXew9DvE8mS107f_CfOr0tw5_G0uOfiH3cAlikCYdSCDRQ_r0_cy0";


        //    using (var firebase = new FireBase.Notification.Firebase())
        //    {
        //        firebase.ServerKey = serverKey;
        //        var id = registrationId;
        //        firebase.PushNotifyAsync(id, "Hello", "World").Wait();
        //        Console.ReadLine();
        //    }

        //}




    }
}
