using PrismAria.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismAria.Services
{
    public class ArticlesService
    {
        public ObservableCollection<ArticlesModel> GetArticles()
        {
            ObservableCollection<ArticlesModel> articles = new ObservableCollection<ArticlesModel>() {
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"},
                new ArticlesModel(){ BandName="Maroon 5", BandPic ="sample_pic.png", ArticleTitle="New Album", Article="Our album named shit is very nice and shit"}
            };

            return articles;
        }

        public void AddArticles(ObservableCollection<ArticlesModel> collection) {
            collection.Add(new ArticlesModel() { BandName = "Maroon 5", BandPic = "sample_pic.png", ArticleTitle = "New Album", Article = "Our album named shit is very nice and shit" });
        }
    }
}
