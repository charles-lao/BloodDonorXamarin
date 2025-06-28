using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodDonorXamarin.Fragments
{
    public class NewDonorFragment : AndroidX.Fragment.App.DialogFragment
    {
        MaterialSpinner materialSpinner;
        List<string> bloodGroupsList;
        ArrayAdapter<string> spinnerAdapter;
        string selectedBloodGroup;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.addnew, container, false);
            materialSpinner = (MaterialSpinner)view.FindViewById(Resource.Id.materialSpinner);
            SetupSpinner();
            return view;
            
        }

        void SetupSpinner()
        {
            bloodGroupsList = new List<string>();
            bloodGroupsList.Add("O+");
            bloodGroupsList.Add("O-");
            bloodGroupsList.Add("AB+");
            bloodGroupsList.Add("AB-");
            bloodGroupsList.Add("A+");
            bloodGroupsList.Add("A-");
            bloodGroupsList.Add("B+");
            bloodGroupsList.Add("B-");

            spinnerAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, bloodGroupsList);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            materialSpinner.Adapter = spinnerAdapter;
            materialSpinner.ItemSelected += MaterialSpinner_ItemSelected;
        }

        private void MaterialSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != -1)
            {
                selectedBloodGroup = bloodGroupsList[e.Position];
                Console.WriteLine(selectedBloodGroup);
            }
            
        }
    }
}