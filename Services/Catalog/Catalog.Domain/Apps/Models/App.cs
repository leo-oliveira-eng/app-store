using Catalog.Domain.Authors.Models;
using Catalog.Domain.Common;
using Messages.Core;
using System;
using System.Collections.Generic;

namespace Catalog.Domain.Apps.Models
{
    public class App : Entity
    {
        public string Name { get; private set; }

        public string Title { get; private set; }

        public int Size { get; private set; }

        public string Version { get; private set; }

        public string ExternalId { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public string AppLogo { get; private set; }

        public List<string> LanguageList { get; private set; }

        public decimal Price { get; private set; }

        public Author Author { get; private set; }

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

        public static implicit operator App(Maybe<App> entity) => entity.Value;

        public static implicit operator App(Response<App> entity) => entity.Data;
    }
}
