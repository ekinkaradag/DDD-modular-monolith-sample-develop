using System;

namespace IoCore.SharedReadKernel.Product
{
    public class Product
    {
        public Product(string key, string title, string version, string status)
        {
            Key = key;
            Title = title;
            Version = version;
            Status = status;
        }

        public Guid Id { get; private set; }
        
        public string Key { get; private set; }
        public string Title { get; private set; }
        public string Version { get; private set; }
        public string Status { get; private set; }
    }
}