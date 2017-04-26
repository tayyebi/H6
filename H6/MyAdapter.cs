using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Graphics;

namespace H6
{
    class MyAdapter : BaseAdapter
    {
        Activity context;
        List<PostModel> posts;

        public MyAdapter(Activity ApplicationContext, List<PostModel> Posts) : base()
        {
            context = ApplicationContext;
            posts = Posts;
        }

        public override int Count
        {
            get
            {
                return posts.Count();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            // This is not the whole object
            return posts[position].Title;
        }

        public override long GetItemId(int position)
        {
            // Its not the id
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItem1, null);

            TextView tv2 = view.FindViewById<TextView>(Resource.Id.textView2);
            TextView tv1 = view.FindViewById<TextView>(Resource.Id.textView1);
            ImageView imv = view.FindViewById<ImageView>(Resource.Id.imageView1);

            tv1.Text = posts[position].Title;
            tv2.Text = posts[position].Body;
            try
            {
                Bitmap bmp = BitmapFactory.DecodeByteArray(posts[position].Image, 0, posts[position].Image.Length);
                view.FindViewById<ImageView>(Resource.Id.imageView1).SetImageBitmap(bmp);
            }
            catch { }

            return view;
        }
    }
}