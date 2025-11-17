using System;
using System.Configuration;
using System.Linq;

namespace AutomationTestCSharp.Utilities
{
    public class DynamicDataSource : ConfigurationSection
    {
        /// <summary>
        /// The name of this section in the app.config.
        /// </summary>
        public const string sectionName = "DynamicDataSource";

        private const string _sourceCollectionName = "Source";

        [ConfigurationProperty(_sourceCollectionName)]
        [ConfigurationCollection(typeof(ConnectionManagerSourcesCollection), AddItemName = "add")]
        public ConnectionManagerSourcesCollection ConnectionManagerSources { get { return (ConnectionManagerSourcesCollection)base[_sourceCollectionName]; } }
    }

    public class ConnectionManagerSourcesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConnectionManagerSourceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionManagerSourceElement)element).Name;
        }
    }

    public class ConnectionManagerSourceElement : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("ConnectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)this["ConnectionString"]; }
            set { this["ConnectionString"] = value; }
        }

        [ConfigurationProperty("Query", IsRequired = true)]
        public string Query
        {
            get { return (string)this["Query"]; }
            set { this["Query"] = value; }
        }

        [ConfigurationProperty("RowIndexes")]
        public string RowIndexesCollection
        {
            get => this["RowIndexes"]?.ToString();
            set => this["RowIndexes"] = value;
        }

        public int[] RowIndexes => string.IsNullOrEmpty(RowIndexesCollection) ?
        Array.Empty<int>() :
        RowIndexesCollection.Split(',').Select(i => Convert.ToInt32(i, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
    }
}
