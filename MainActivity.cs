using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using AndroidX.AppCompat.App;
using BloodDonorXamarin.Adapters;
using BloodDonorXamarin.DataModels;
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

            donorsRecyclerView = (RecyclerView)FindViewById(Resource.Id.donorsRecyclerView);
            CreateData();
            SetupRecyclerView();
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
            donorsRecyclerView.SetAdapter(donorsAdapter);
        }
    }
}