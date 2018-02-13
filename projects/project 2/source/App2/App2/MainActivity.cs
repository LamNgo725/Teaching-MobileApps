using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using System;

namespace App2
{
    [Activity(Label = "App2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Used to track the file that we're manipulating between functions
        /// </summary>
        public static Java.IO.File _file;

        /// <summary>
        /// Used to track the directory that we'll be writing to between functions
        /// </summary>
        public static Java.IO.File _dir;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            //StrictMode.SetVmPolicy(builder.Build());


            /*StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.setVmPolicy(builder.build());*/

            if (IsThereAnAppToTakePictures() == true)
            {
                CreateDirectoryForPictures();
                FindViewById<Button>(Resource.Id.launchCameraButton).Click += TakePicture;
            }

 
        }

        /// <summary>
        /// Apparently, some android devices do not have a camera.  To guard against this,
        /// we need to make sure that we can take pictures before we actually try to take a picture.
        /// </summary>
        /// <returns></returns>
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities
                (intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        /// <summary>
        /// Creates a directory on the phone that we can place our images
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            _dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraExample");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            //android.support.v4.content.FileProvider
            //getUriForFile(getContext(), "com.mydomain.fileprovider", newFile);
            //FileProvider.GetUriForFile

            //The line is a problem line for Android 7+ development
            //intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
            StartActivityForResult(intent, 0);
        }

        // <summary>
        // Called automatically whenever an activity finishes
        // </summary>
        // <param name = "requestCode" ></ param >
        // < param name="resultCode"></param>
        // <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //Make image available in the gallery

            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(_file);
            //Uri uri = contentUri.Data;
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);


            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;

            //AC: workaround for not passing actual files
            Android.Graphics.Bitmap bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");


            //Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);

            //this code removes all red from a picture
            /*for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int p = bitmap.GetPixel(i, j);
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }*/
            if (bitmap != null)
            {
                /*Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
                Android.Graphics.Bitmap OGBitmap = copyBitmap;*/
                Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;
            

             
            
                //allows user to see the hidden buttons for effects now
                Button negRed = FindViewById<Button>(Resource.Id.negatered);
                Button negGre = FindViewById<Button>(Resource.Id.negategreen);
                Button negBlu = FindViewById<Button>(Resource.Id.negateblue);
                Button remGre = FindViewById<Button>(Resource.Id.removegreen);
                Button highCon = FindViewById<Button>(Resource.Id.highcontrast);
                Button graSca = FindViewById<Button>(Resource.Id.greyscale);
                Button remRed = FindViewById<Button>(Resource.Id.removered);
                Button remBlu = FindViewById<Button>(Resource.Id.removeblue);

                negRed.Visibility = Android.Views.ViewStates.Visible;
                negGre.Visibility = Android.Views.ViewStates.Visible;
                negBlu.Visibility = Android.Views.ViewStates.Visible;
                remGre.Visibility = Android.Views.ViewStates.Visible;
                highCon.Visibility = Android.Views.ViewStates.Visible;
                graSca.Visibility = Android.Views.ViewStates.Visible;
                remRed.Visibility = Android.Views.ViewStates.Visible;
                remBlu.Visibility = Android.Views.ViewStates.Visible;

                //Negate red effect
                negRed.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            c.R = (byte)(255 - c.R);
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };

                //Negate green effect
                negGre.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            c.G = (byte)(255 - c.G);
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };

                //Negate blue effect
                negBlu.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            c.B = (byte)(255 - c.B);
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };

                //Grayscale effect
                graSca.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            int r = c.R;
                            int b = c.B;
                            int g = c.G;
                            c.R = (byte)((r+b+g)/3);
                            c.B = (byte)((r + b + g) / 3);
                            c.G = (byte)((r + b + g) / 3);
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };

                //Remove Red effect
                remRed.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            c.R = 0;
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };

                //Remove Blue effect
                remBlu.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            c.B = 0;
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };

                //Remove Green effect
                remGre.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            c.G = 0;
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };

                //High Contrast effect
                highCon.Click += delegate
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {
                        for (int j = 0; j < bitmap.Height; j++)
                        {
                            int p = bitmap.GetPixel(i, j);
                            Android.Graphics.Color c = new Android.Graphics.Color(p);
                            if (c.R > 177)
                            {
                                c.R = 255;
                            }
                            else
                            {
                                c.R = 0;
                            }
                            if (c.G > 177)
                            {
                                c.G = 255;
                            }
                            else
                            {
                                c.G = 0;
                            }
                            if (c.B > 177)
                            {
                                c.B = 255;
                            }
                            else
                            {
                                c.B = 0;
                            }
                            copyBitmap.SetPixel(i, j, c);
                            imageView.SetImageBitmap(copyBitmap);
                        }
                    }
                };
            }
            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }

        
    }
}

