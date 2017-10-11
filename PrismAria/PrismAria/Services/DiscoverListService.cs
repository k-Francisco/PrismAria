
using PrismAria.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace PrismAria.Services
{
    public class DiscoverListService
    {
        ObservableCollection<DiscoverPageModel> discoverList = new ObservableCollection<DiscoverPageModel>();
        List<BandModel> bandList = new List<BandModel>();
        public ObservableCollection<DiscoverPageModel> GetDiscoverList() {
            if (discoverList.Count == 0) {

                discoverList.Add(new DiscoverPageModel() { categoryName = "Top 10", bandList = GetBands(), isTop = true});
                var categories = new List<string>() {
                    "Trending",
                    "Most Favorite",
                    "Recommended",
                    "Rising Stars",
                    "Rockers",
                    "Showbands",
                    "BisRock",
                    "New"
                };
                foreach (var item in categories) {
                    discoverList.Add(new DiscoverPageModel() { categoryName = item, bandList = GetBands() , isTop= false});
                }
            }


            return discoverList;
        }

        private List<BandModel> GetBands() {
            if(bandList.Count == 0)
            {
                bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Maroon 5" });
                bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Paramore" });
                bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Panic at the Disco" });
                bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Sleeping with the sirens"});
                bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Up Dharma Down" });
                bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "Fall Out Boys" });
                bandList.Add(new BandModel() { imgSource = "sample_pic.png", bandName = "All Time Low" });
            }
            return bandList;
        }
    }
}
