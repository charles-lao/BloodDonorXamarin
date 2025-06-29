using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.RecyclerView.Widget;
using BloodDonorXamarin.Adapters;
using BloodDonorXamarin.DataModels;
using BloodDonorXamarin.Fragments;
using Google.Android.Material.FloatingActionButton;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BloodDonorXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView donorsRecyclerView;
        DonorsAdapter donorsAdapter;
        List<Donor> listOfDonors = new List<Donor>();
        NewDonorFragment newDonorFragment;

        TextView noDonorTextView;

        ISharedPreferences pref = Application.Context.GetSharedPreferences("donors", FileCreationMode.Private);
        ISharedPreferencesEditor editor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Title = "Blood Donors";

            noDonorTextView = (TextView)FindViewById(Resource.Id.noDonorTextView);
            donorsRecyclerView = (RecyclerView)FindViewById(Resource.Id.donorsRecyclerView);
            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);
            fab.Click += Fab_Click;

            // CreateData();
            RetrieveData();

            if (listOfDonors.Count > 0)
            {
                SetupRecyclerView();
            }
            else
            {
                noDonorTextView.Visibility = Android.Views.ViewStates.Visible;
            }
            
            editor = pref.Edit();
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            newDonorFragment = new NewDonorFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            newDonorFragment.Show(trans, "new donor");
            newDonorFragment.OnDonorRegistered += NewDonorFragment_OnDonorRegistered;
        }

        private void NewDonorFragment_OnDonorRegistered(object sender, NewDonorFragment.DonorDetailsEventArgs e)
        {
            if (newDonorFragment != null)
            {
                newDonorFragment.Dismiss();
                newDonorFragment = null;
            }

            if (listOfDonors.Count > 0)
            {
                
                listOfDonors.Insert(0, e.Donor);
                donorsAdapter.NotifyItemInserted(0);

                string jsonString = JsonConvert.SerializeObject(listOfDonors);
                editor.PutString("donors", jsonString);
                editor.Apply();
            }
            else
            {
                listOfDonors.Add(e.Donor);

                string jsonString = JsonConvert.SerializeObject(listOfDonors);
                editor.PutString("donors", jsonString);
                editor.Apply();

                SetupRecyclerView();
            }
           
        }

        void CreateData()
        {
            listOfDonors = new List<Donor>();
            listOfDonors.Add(new Donor { BloodGroup = "AB+", City = "Delaware", Country = "USA", Email = "ufinixacademy@gmail.com", Fullname = "Clark Lao", Phone = "+01 76376 883" });
            listOfDonors.Add(new Donor { BloodGroup = "O+", City = "Munich", Country = "Germany", Email = "ufinixacademy@gmail.com", Fullname = "Wade Wilson", Phone = "+01 76376 883" });
            listOfDonors.Add(new Donor { BloodGroup = "O-", City = "Lagos", Country = "Nigeria", Email = "ufinixacademy@gmail.com", Fullname = "Bruce Wayne", Phone = "+01 76376 883" });
            listOfDonors.Add(new Donor { BloodGroup = "B-", City = "North Carolina", Country = "USA", Email = "ufinixacademy@gmail.com", Fullname = "Clark Kent", Phone = "+01 76376 883" });
            listOfDonors.Add(new Donor { BloodGroup = "O+", City = "Madrid", Country = "Spain", Email = "ufinixacademy@gmail.com", Fullname = "Naruto Uzumaki", Phone = "+01 76376 883" });
            listOfDonors.Add(new Donor { BloodGroup = "A+", City = "New York", Country = "USA", Email = "ufinixacademy@gmail.com", Fullname = "Sakura Haruno", Phone = "+01 76376 883" });
        }

        void RetrieveData()
        {
            string json = pref.GetString("donors", "");
            if (!string.IsNullOrEmpty(json))
            {
                listOfDonors = JsonConvert.DeserializeObject<List<Donor>>(json);
            }
        }

        void SetupRecyclerView()
        {
            donorsRecyclerView.SetLayoutManager(new LinearLayoutManager(donorsRecyclerView.Context));
            donorsAdapter = new DonorsAdapter(listOfDonors);
            donorsAdapter.ItemClick += DonorsAdapter_ItemClick;
            donorsAdapter.CallClick += DonorsAdapter_CallClick;
            donorsAdapter.EmailClick += DonorsAdapter_EmailClick;
            donorsAdapter.DeleteClick += DonorsAdapter_DeleteClick;

            donorsRecyclerView.SetAdapter(donorsAdapter);

            noDonorTextView.Visibility = Android.Views.ViewStates.Invisible;
        }

        private void DonorsAdapter_DeleteClick(object sender, DonorsAdapterClickEventArgs e)
        {
            var donor = listOfDonors[e.Position];

            AndroidX.AppCompat.App.AlertDialog.Builder DeleteAlert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);

            DeleteAlert.SetMessage("Are you sure");
            DeleteAlert.SetTitle("Delete Donor");

            DeleteAlert.SetPositiveButton("Delete", (alert, args) =>
            {
                listOfDonors.RemoveAt(e.Position);
                //donorsAdapter.NotifyDataSetChanged();
                donorsAdapter.NotifyItemRemoved(e.Position);

                string jsonString = JsonConvert.SerializeObject(listOfDonors);
                editor.PutString("donors", jsonString);
                editor.Apply();
            });

            DeleteAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                DeleteAlert.Dispose();
            });

            DeleteAlert.Show();
        }   


        private void DonorsAdapter_EmailClick(object sender, DonorsAdapterClickEventArgs e)
        {
            var donor = listOfDonors[e.Position];

            AndroidX.AppCompat.App.AlertDialog.Builder EmailAlert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            EmailAlert.SetMessage("Send Mail to " + donor.Fullname);

            EmailAlert.SetPositiveButton("Send", (alert, args) =>
            {
                // Send Email
                Intent intent = new Intent();
                intent.SetType("plain/text");
                intent.SetAction(Intent.ActionSend);
                intent.PutExtra(Intent.ExtraEmail, new string[] { donor.Email });
                intent.PutExtra(Intent.ExtraSubject, "Enquiry on your availability for blood donation" );
                StartActivity(intent);
            });

            EmailAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                EmailAlert.Dispose();
            });

            EmailAlert.Show();
        }

        private void DonorsAdapter_CallClick(object sender, DonorsAdapterClickEventArgs e)
        {
            var donor = listOfDonors[e.Position];

            AndroidX.AppCompat.App.AlertDialog.Builder CallAlert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
            CallAlert.SetMessage("Call " + donor.Fullname);

            CallAlert.SetPositiveButton("Call", (alert, args) =>
            {
                var uri = Android.Net.Uri.Parse("tel:" + donor.Phone);
                var intent = new Intent(Intent.ActionDial, uri);
                StartActivity(intent);
            });

            CallAlert.SetNegativeButton("Cancel", (alert, args) =>
            {
                CallAlert.Dispose();
            });

            CallAlert.Show();
        }

        private void DonorsAdapter_ItemClick(object sender, DonorsAdapterClickEventArgs e)
        {
            Toast.MakeText(this, "Row was clicked", ToastLength.Short).Show();
        }
    }
}