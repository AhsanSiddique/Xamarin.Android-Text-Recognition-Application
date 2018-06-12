using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Gms.Vision.Texts;
using Android.Util;
using Android.Gms.Vision;
using System.Text;

namespace TextRecognitionApp
{
    [Activity(Label = "TextRecognitionApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ImageView imageview;
        private Button btnProcess;
        private TextView txtView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            imageview = FindViewById<ImageView>(Resource.Id.image_view);
            btnProcess = FindViewById<Button>(Resource.Id.btnProcess);
            txtView = FindViewById<TextView>(Resource.Id.txtView);

            Bitmap bitmap = BitmapFactory.DecodeResource(ApplicationContext.Resources, Resource.Drawable.csharpcorner);
            imageview.SetImageBitmap(bitmap);

            btnProcess.Click += delegate
             {
                 TextRecognizer txtRecognizer = new TextRecognizer.Builder(ApplicationContext).Build();
                 if (!txtRecognizer.IsOperational)
                 {
                     Log.Error("Error", "Detector dependencies are not yet available");
                 }
                 else
                 {
                     Frame frame = new Frame.Builder().SetBitmap(bitmap).Build();
                     SparseArray items = txtRecognizer.Detect(frame);
                     StringBuilder strBuilder = new StringBuilder();
                     for (int i = 0; i < items.Size(); i++)
                     {
                         TextBlock item = (TextBlock)items.ValueAt(i);
                         strBuilder.Append(item.Value);
                         strBuilder.Append("/");
                     }
                     txtView.Text = strBuilder.ToString();
                 }
             };
        }
    }
}

