using Catalog.Domain.Authors.Models;
using System;
using System.Collections.Generic;

namespace Catalog.Domain.Apps.Models
{
    public class App
    {
        public string Id { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public DateTime LastUpdate { get; private set; } = DateTime.Now;

        public DateTime? DeletedAt { get; private set; }

        public bool Deleted => DeletedAt.HasValue;

        public string Name { get; set; }

        public string Title { get; set; }

        public int Size { get; set; }

        public string Version { get; set; }

        public string ExternalId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string AppLogo { get; set; }

        public List<string> LanguageList { get; set; }

        public decimal Price { get; set; }

        public Author Author { get; set; }

        private App() { }

        public App(string name, string title, int size, string version, string externalId, DateTime releaseDate, string appLogo,
            List<string> languageList, decimal price, Author author)
        {
            Name = name;
            Title = title;
            Size = size;
            Version = version;
            ExternalId = externalId;
            ReleaseDate = releaseDate;
            AppLogo = appLogo;
            LanguageList = languageList;
            Price = price;
            Author = author;
        }
    }
}
