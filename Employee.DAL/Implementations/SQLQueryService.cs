
using AWE.Employee.DAL.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace AWE.Employee.DAL.Implementations
{
    public class SQLQueryService : ISQLQueryService
    {
        private StringDictionary Queries = new StringDictionary();
        public IConfiguration _configuration { get; }

        public SQLQueryService(IConfiguration configuration)
        {
            _configuration = configuration;
            string databaseType = _configuration["appSettings:DataBase"];
            string sqlId = "";
            var assembly = Assembly.GetExecutingAssembly();
            IEnumerable<string> resourceNameList = assembly.GetManifestResourceNames().Where(str => str.EndsWith(".xml"));

            foreach (string resourceName in resourceNameList)
            {
                Stream xmlStream = assembly.GetManifestResourceStream(resourceName);

                XmlTextReader textReader = new XmlTextReader(xmlStream);
                if (Queries == null)
                    Queries = new StringDictionary();
                while (textReader.Read())
                {
                    switch (textReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (textReader.Name.Equals("SQL"))
                            {
                                while (textReader.MoveToNextAttribute())
                                {
                                    if (textReader.Name.Equals("ID"))
                                    {
                                        sqlId = textReader.Value.Replace(databaseType + "_",string.Empty);
                                        break;
                                    }
                                }
                            }
                            break;
                        case XmlNodeType.Comment:
                            if (!string.IsNullOrEmpty(sqlId))
                            {
                                Queries.Add(sqlId, textReader.Value);
                            }
                            sqlId = "";
                            break;
                    }
                }
            }
        }

        public string GetQuery(string key)
        {
            return Queries[key];
        }
    }
}
