using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using BloodDonorXamarin.Adapters;
using BloodDonorXamarin.DataModels;
using Google.Android.Material.FloatingActionButton;
using System.Collections.Generic;

namespace BloodDonorXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        RecyclerView donorsRecyclerView;
        DonorsAdapter donorsAdapter;
        List<Donor> listOfDonors;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Title = "Blood Donors";
            donorsRecyclerView = (RecyclerView)FindViewById(Resource.Id.donorsRecyclerView);
            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.fab);
            fab.Click += Fab_Click;
            CreateData();
            SetupRecyclerView();
        }

        private void Fab_Click(object sender, System.EventArgs e)
        {
            Toast.MakeText(this, "Floating Action Button Clicked", ToastLength.Short).Show();
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

        void SetupRecyclerView()
        {
            donorsRecyclerView.SetLayoutManager(new LinearLayoutManager(donorsRecyclerView.Context));
            donorsAdapter = new DonorsAdapter(listOfDonors);
            donorsAdapter.ItemClick += DonorsAdapter_ItemClick;
            donorsAdapter.CallClick += DonorsAdapter_CallClick;
            donorsAdapter.EmailClick += DonorsAdapter_EmailClick;
            donorsAdapter.DeleteClick += DonorsAdapter_DeleteClick;

            donorsRecyclerView.SetAdapter(donorsAdapter);
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