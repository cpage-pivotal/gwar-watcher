using Elasticsearch.Net;
using Nest;
using OTF.GwarWatcher.Common.Extensions;
using OTF.GwarWatcher.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OTF.GwarWatcher.Elastic
{
    public class MessageProvider
    {
        public string[] Nodes { get; set; } = new string[0];
        public int MaxValuesPerQuery { get; set; } = 1000;
        public async Task<IEnumerable<MessageModel>> GetMessages<T>(Expression<Func<MessageModel, T>> valuePath, IEnumerable<T> values, string environmentTldCsv, int count = -1)
        {
            List<MessageModel> toReturn = new List<MessageModel>();
            if (count <= 0)
            {
                count = values.Count();
            }
            if (this.Nodes != null && this.Nodes.Any())
            {
                IElasticClient client = new ElasticClient(new ConnectionSettings(new StaticConnectionPool(this.Nodes.Select(n => new Uri(n.Trim())).ToArray()))
                    .EnableDebugMode()
                    .DisableDirectStreaming()
                    .PrettyJson());

                for (int idx = 0; idx * this.MaxValuesPerQuery < count; idx++)
                {
                    try
                    {
                        ISearchResponse<MessageModel> resp = await client.SearchAsync<MessageModel>(s =>
                            s.Index("*gwar*")
                            .From(0)
                            .Size(Math.Min(this.MaxValuesPerQuery, count))
                            .Query(q => this.GenerateQuery(valuePath, values.Skip(idx * this.MaxValuesPerQuery).Take(this.MaxValuesPerQuery), environmentTldCsv))
                            .Sort(ss => ss.Descending("@timestamp")));

                        while (resp.Documents.Any())
                        {
                            toReturn.AddRange(resp.Documents);
                            resp = client.Scroll<MessageModel>("10m", resp.ScrollId);
                        }
                        client.ClearScroll(new ClearScrollRequest(resp.ScrollId));
                    }
                    catch (UnexpectedElasticsearchClientException) { }
                }
            }
            return toReturn;
        }

        private QueryContainer GenerateQuery<T>(Expression<Func<MessageModel, T>> valuePath, IEnumerable<T> values, string environmentTldCsv)
        {
            // A qyery for each ID passed in
            QueryContainer idQuery = new QueryContainer();
            values.ForEach(v => idQuery = idQuery || new QueryContainerDescriptor<MessageModel>().MatchPhrase(mp => mp.Field(valuePath).Query(v.ToString())));

            // A query to make sure we're in the right environment
            QueryContainer tldQuery = new QueryContainer();// new QueryContainerDescriptor<MessageModel>().Wildcard(w => w.Field(m => m.SourcePageUrl).Value($"*.{environmentTld}*"));
            if(!string.IsNullOrEmpty(environmentTldCsv))
            {
                environmentTldCsv.Split(',').ForEach(tld => tldQuery = tldQuery || new QueryContainerDescriptor<MessageModel>().Wildcard(w => w.Field(m => m.SourcePageUrl).Value($"*.{tld.Trim()}*")));
            }

            return idQuery && tldQuery;
        }
    }
}
