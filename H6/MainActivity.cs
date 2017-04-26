using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace H6
{
    [Activity(Label = "کنکور امسال", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            try
            {
                using (WebClient client = new WebClient())
                {
                    // -- image
                    var imageBytes = client.DownloadData(About.WebServer + "Home/CountDown");
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        FindViewById<ImageView>(Resource.Id.imageView1).SetImageBitmap(BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length));
                    }

                    // -- posts
                    string t = client.DownloadString(About.WebServer + "api/Values");
                    List<PostModel> pmList = JsonConvert.DeserializeObject<List<PostModel>>(t);

                    t = null;

                    ListView l1 = FindViewById<ListView>(Resource.Id.listView1);
                    l1.Adapter = new MyAdapter(this, pmList);

                }
            }
            catch {
                Toast.MakeText(this, "Application is offline", ToastLength.Long).Show();
            }
        }
    }
}