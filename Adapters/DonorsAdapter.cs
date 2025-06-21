using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using BloodDonorXamarin.DataModels;
using System;
using System.Collections.Generic;

namespace BloodDonorXamarin.Adapters
{
    internal class DonorsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<DonorsAdapterClickEventArgs> ItemClick;
        public event EventHandler<DonorsAdapterClickEventArgs> ItemLongClick;
        List<Donor> DonorsList;

        public DonorsAdapter(List<Donor> data)
        {
            DonorsList = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;

            itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.donor_row, parent, false);

            var vh = new DonorsAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var donor = DonorsList[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as DonorsAdapterViewHolder;
            holder.donorLocationTextView.Text = donor.City + ", " + donor.Country;
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => DonorsList.Count;

        void OnClick(DonorsAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(DonorsAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class DonorsAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }

        public TextView donorNameTextView;
        public TextView donorLocationTextView;
        public ImageView bloodGroupImageView;
        public RelativeLayout callLayout;
        public RelativeLayout emailLayout;
        public RelativeLayout deleteLayout;


        public DonorsAdapterViewHolder(View itemView, Action<DonorsAdapterClickEventArgs> clickListener,
                            Action<DonorsAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            donorNameTextView = (TextView)itemView.FindViewById(Resource.Id.donorNameTextView);
            donorLocationTextView = (TextView)itemView.FindViewById(Resource.Id.donorLocationTextView);
            bloodGroupImageView = (ImageView)itemView.FindViewById(Resource.Id.bloodGroupImageView);
            callLayout = (RelativeLayout)itemView.FindViewById(Resource.Id.callLayout);
            emailLayout = (RelativeLayout)itemView.FindViewById(Resource.Id.emailLayout);
            deleteLayout = (RelativeLayout)itemView.FindViewById(Resource.Id.deleteLayout);

            itemView.Click += (sender, e) => clickListener(new DonorsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new DonorsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class DonorsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}