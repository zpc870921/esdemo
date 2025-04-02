using Elasticsearch.Net;
using ESDemo.Models;
using ESDemo.ViewModel;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var uris = new[] { new Uri("http://localhost:9200")};
            //var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            SniffingConnectionPool pool = new SniffingConnectionPool(new[]{ new Node(new Uri("http://127.0.0.1:9200")) });
            //SingleNodeConnectionPool pool = new SingleNodeConnectionPool(new Uri("http://127.0.0.1:9200"));
            //var pool = new SniffingConnectionPool(uris);
            ConnectionSettings settings = new ConnectionSettings(pool).DefaultIndex("forum")
                .DefaultMappingFor<Article>(m => m.TypeName("article"))
                .DefaultMappingFor<ParentWithStringId>(m => m.TypeName("parent").Ignore(f => f.Description).Ignore(f => f.IgnoreMe))
                .DefaultMappingFor<MyDocument>(m => m.TypeName("doc").IndexName("indexrelation"))
                .DefaultMappingFor<MyChild>(m => m.TypeName("doc").IndexName("indexrelation").RelationName("child2"))
                .DefaultMappingFor<MyParent>(m => m.TypeName("doc").IndexName("indexrelation").RelationName("parent2"))
                .DefaultMappingFor<CompanyWithAttributesAndPropertiesToIgnore>(m => m.Ignore(i => i.AnotherToIgnore))
                .DefaultMappingFor<Child>(m => m.PropertyName(p => p.Description, "desc").Ignore(i => i.IgnoreMe))
                .DefaultMappingFor<MyDTO>(m => m.IdProperty(f => f.Name).IndexName("dtos"))
                .DefaultMappingFor<Precedence>(m => m.PropertyName(p => p.RenamedOnConnectionSettings, "rename"))
                .DefaultMappingFor<News>(m => m.IndexName("news_website")).DisableDirectStreaming()
                .DefaultMappingFor<Merchant>(m=>m.IndexName("geoindex"))
                //.DefaultFieldNameInferrer(m=>m.ToUpperInvariant())
                //.DefaultTypeNameInferrer(t=>t.Name.ToLower()+"-suffix")
                ;

            ElasticClient client = new ElasticClient(settings);
            ElasticClient transportClient = new ElasticClient();

            //var data = client.Search<Article>(s=>s.Query(q=>q.MatchAll()).Size(10).From(0));
            //var data = client.Search<Article>(s=>s.Query(q=>q.Match(m=>m.Field("title").Query("elasticsearch"))));

            //foreach (var item in data.Documents)
            //{
            //    Console.WriteLine($"id:{item.ArticleID},title:{item.Title},post_date:{item.Post_Date}");
            //}

            //var aggs = client.Search<Article>(s=>s.Size(0).Aggregations(a=>a.Terms("group_by_tag",g=>g.Field("tag.keyword"))));
            //var result= aggs.Aggregations.Terms("group_by_tag").Buckets;
            //foreach (var item in result)
            //{
            //    Console.WriteLine($"key:{item.Key},count:{item.DocCount}");
            //}


            //var result=  client.CreateIndex("myindex",
            //     s => s.Mappings(ms => ms.Map<Article>(m =>
            //         m.Properties(props => props.Custom(new MyPluginProperty("fieldname", "dutch"))))));
            // Console.WriteLine(result);


            //var result = client.CreateIndex("myindex",
            //    ms => ms.Mappings(m => m.Map<Article>(props => props.AutoMap())));

            //var dleete= client.DeleteIndex("geoindex");

            //var result2323 = client.CreateIndex("geoindex",
            //        ms => ms.Mappings(m => m.Map<Merchant>(props => props.AutoMap<Merchant>())));




            //var merchantresult = client.IndexMany(new[]
            //{
            //    new Merchant
            //    {
            //         Id=1,
            //         Category=1,
            //         CreateDate=DateTime.Now,
            //         District=1,
            //         Location=new GeoLocation(40.061209,116.307782),//华控大厦
            //         Name="鑫明明拉面",
            //         Status=1
            //    },
            //    new Merchant
            //    {
            //        Id=2,
            //        Category=2,
            //        CreateDate=DateTime.Now.AddDays(-1),
            //        District=1,
            //        Location=new GeoLocation(40.061464,116.308447),//彭欢国际
            //        Name="东方宫兰州拉面",
            //        Status=1
            //    },
            //    new Merchant
            //    {
            //        Id=3,
            //        Category=1,
            //        CreateDate=DateTime.Now.AddDays(-2),
            //        District=1,
            //        Location=new GeoLocation(40.060243,116.308806),//环洋大厦
            //        Name="开海饭店",
            //        Status=1
            //    }
            //}, "geoindex");



            //var scriptQuery = client.Search<Merchant>(s=>s.Query(q=>q.Script(st=>st.Lang("painless").Source("doc['category'].value==1"))));




            var updateResult = client.Update<Merchant,object>(2, upd => upd.Doc(new
            {
                Address="北京市东城区珠市口东大街16号",
                Img= "http://jbcdn2.b0.upaiyun.com/2017/10/c6cf4b2000277c64f55e00cf6d2f294f.png",
                Level=98
            }));

            //var result = client.CreateIndex("myindex",m=>m.Mappings(mp=>mp.Map<Document>(ms=>ms.AutoMap<Company>().AutoMap(typeof(Employee)))));

            //var result = client.CreateIndex("myindex",m=>m.Mappings(mp=>mp.Map<ParentWithStringId>(ms=>ms.AutoMap())));
            //var result = client.CreateIndex("myindex", m => m.Mappings(mp => mp.Map<A>(ms => ms.AutoMap(3))));

            //var result = client.CreateIndex("myindex",m=>m.Mappings(mp=>mp.Map<Company>(mps=>mps.AutoMap().Properties(p=>p.Nested<Employee>(f=>f.Name(prop=>prop.Employees))))));
            //var result = client.CreateIndex("myindex",m=>m.Mappings(ms=>ms.Map<Employee>(mp=>mp.AutoMap(new DisableDocValuesPropertyVisitor()))));

            //var result = client.CreateIndex("myindex", m => m.Mappings(ms => ms.Map<Employee>(mp => mp.AutoMap(new EverythingIsATextPropertyVisitor()))));

            //var result = client.CreateIndex("index",c=>c.Mappings(ms=>ms.Map<MyDocument>(m=>
            //m.RoutingField(r=>r.Required())
            //.AutoMap<MyParent>()
            //.AutoMap<MyChild>()
            //.Properties(props=>props.Join(j=>
            //j.Name(n=>n.MyJoinField)
            //.Relations(r=>r.Join<MyParent,MyChild>())))
            //)));


            //var result= client.IndexDocument(new MyParent
            //{
            //     Id=1,
            //     ParentProperty="a parent prop",
            //     MyJoinField=typeof(MyParent)
            //});

            //var result = client.IndexDocument(new MyChild
            //{
            //     Id=2,
            //     MyJoinField=JoinField.Link<MyChild>(1)
            //});

            // var result = client.CreateIndex("index",m=>m.Mappings(ms=>ms.Map<CompanyWithAttributesAndPropertiesToIgnore>(mp=>mp.AutoMap())));

            //var result55 = client.CreateIndex("index2",m=>m.Mappings(ms=>ms.Map<Child>(mp=>mp.AutoMap())));

            //var result = client.Search<Film>(s=>s.Index("film").Type("doc").Query(q=>q.Match(m=>m.Field(f=>f.Title).Query("行动"))).Aggregations(aggs=>aggs.Terms("tags",t=>t.Field(f=>f.Tags))));

            //foreach (var item in result.Documents)
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in result.Aggregations.Terms("tags").Buckets)
            //{
            //    Console.WriteLine($"key:{item.Key},count:{item.DocCount}");
            //}


            //var result = client.CreateIndex("myindex",m=>m.Mappings(ms=>ms.Map<Person>(mp=>mp.Properties(props=>props.Text(f=>f.Name(nn=>nn.Name).Fields(ff=>ff.Keyword(tt=>tt.Name("keyword").IgnoreAbove(256)).Text(tt=>tt.Name("stop").Analyzer("stop"))))))));

            //var result = client.CreateIndex("myindex3",m=>
            //m.Settings(s=>s.Analysis(aa=>aa.Analyzers(az=>az.Standard("standard_analyzer",al=>al.StopWords("_english_")))))
            //.Mappings(ms=>ms.Map<Person>(mp=>mp.Properties(props=>props.Text(ff=>ff.Name(tt=>tt.Name).Analyzer("standard_analyzer"))))));


            //var result = client.CreateIndex("myindex4",m=>
            //m.Settings(s=>s.Analysis(sa=>sa.CharFilters(cf=>cf.Mapping("testmapping",mcf=>mcf.Mappings("c#=>csharp","C#=>csharp"))).Analyzers(az=>az.Custom("mapanalyzer",cad=>cad.CharFilters("html_strip","testmapping").Tokenizer("standard").Filters("standard","lowercase","stop")))))
            //.Mappings(ms=>ms.Map<Question>(mp=>mp.AutoMap().Properties(props=>props.Text(ff=>ff.Name(tt=>tt.Body).Analyzer("mapanalyzer")))))
            //);

            //var result = client.Analyze(az =>
            //    az.Index("myindex4").Field<Question>(ff => ff.Body).Text("c# is very good programming language"));


            //var result= client.CloseIndex("myindex4");
            //var result2 = client.UpdateIndexSettings("myindex4",
            //    usd => usd.IndexSettings(s => s.Analysis(sa =>
            //        sa.CharFilters(cf => cf.Mapping("my_char_filter", fcd => fcd.Mappings("f#=>fsharp"))).TokenFilters(tf=>tf.Synonym("mysynonym",sy=>sy.Synonyms("superior,great"))).Analyzers(ad=>ad.Custom("my_analyzer",cad=>cad.Tokenizer("standard").CharFilters("my_char_filter","html_strip").Filters("lowercase","stop","mysynonym"))))));
            //client.OpenIndex("myindex4");
            //client.ClusterHealth(chd => chd.Index("myindex4"));


            //var result3 = client.Analyze(az =>
            //       az.Index("myindex4").Analyzer("my_analyzer").Text("f# is superior good programming language"));


            //var result = client.Search<Article>(s =>
            //s.Index("forum")
            //.Type("article")
            //    .Query(q => q.DateRange(dr => dr.Field(ff=>ff.Post_Date).GreaterThanOrEquals("2017-01-01").LessThanOrEquals("2017-01-05"))));
            //foreach (var item in result.Documents)
            //{
            //    Console.WriteLine($"title:{item.Title},post_date:{item.Post_Date}");
            //}

            //var result = client.Search<Article>(s=>s.Query(q=>q.Bool(b=>b.Filter(f=>f.DateRange(dr=>dr.Field("post_Date").GreaterThanOrEquals("2017-01-01").LessThanOrEquals("2017-01-11"))))));
            //foreach (var item in result.Documents)
            //{
            //    Console.WriteLine($"title:{item.Title},post_date:{item.Post_Date}");
            //}

            //var result = client.Search<StoIndex>(s => s.Index("stoindex").Type("doc").StoredFields(sf=>sf.Fields(f=>f.Id,f=>f.Title,f=>f.Post_Date)).Query(q => q.MatchAll()));


            //var data = new List<StoIndex>(10);
            //StoIndex document = null;
            //foreach (var item in result.Fields)
            //{
            //    document = new StoIndex
            //    {
            //         Id=item.ValueOf<StoIndex, string>(p=>p.Id),
            //         Title=item.Value<string>(Infer.Field<StoIndex>(f=>f.Title)),
            //         Post_Date=item.Value<DateTime>(Infer.Field<StoIndex>(f=>f.Post_Date))
            //    };
            //    data.Add(document);
            //}
            //Console.WriteLine(JsonConvert.SerializeObject(data));

            //var result = client.Search<StoIndex>(s=>s
            //.Index("stoindex")
            //.Type("doc")
            ////.Source(false)
            //.Source(sc=>sc.Includes(fs=>fs.Fields(ff=>ff.Id,ff=>ff.Title,ff=>ff.Post_Date,ff=>ff.View_CntZ)))
            //.Query(q=>q.MatchAll()));

            //foreach (var item in result.Documents)
            //{
            //    Console.WriteLine($"title:{item.Title},post_date:{item.Post_Date}");
            //}

            //Console.WriteLine(JsonConvert.SerializeObject(result.Documents));


            //var result = client.Search<answer>(s => s.Index("child_example").Type("doc").Size(0).Aggregations(
            //    aggs => aggs.Children<answer>("group_by_id",
            //            cc => cc.Aggregations(
            //                ac => ac.Average("avg_id", avg => avg.Field(ff=>ff.Owner.id))
            //                .Min("min_id",avg=>avg.Field(ff=>ff.Owner.id))
            //                .Max("max_id",avg=>avg.Field(ff=>ff.Owner.id))))));


            //var group= result.Aggregations.Children("group_by_id");


            //Console.WriteLine(group.Average("avg_id").Value);
            //Console.WriteLine(group.Min("min_id").Value);
            //Console.WriteLine(group.Max("max_id").Value);


            //var path = new DocumentPath<Article>(1).Index("forum").Type("article");
            //var path2 = DocumentPath<Article>.Id(2);
            //Console.WriteLine();

            //var article = new Article
            //{
            //     ArticleID="1"
            //};
            //var request = new IndexRequest<Article>(2)
            //{
            //    Document=article
            //};

            //IndexName.From<Article>()

            //var fieldString = new Field("name");
            //var fieldValue = new Field(typeof(Article).GetProperty(nameof(Article.Title)));
            //Expression<Func<Article, object>> expression = a => a.Title;
            //var fieldExpression = new Field(expression);


            //var fieldStringWithBoostTwo = new Field("title^2");
            //var fieldStringWithBoostThree = new Field("title^3");


            //Expression<Func<Article, object>> expression = a => a.Title;
            //var fieldExpression = new Field(expression);
            //var fieldProperty = new Field(typeof(Article).GetProperty(nameof(Article.Title)));

            //var field1 = new Field("title^2");
            //var field2 = new Field("title^2",3);
            //Field field3 = "title^2";
            //Console.WriteLine(field3.Boost);
            //Console.WriteLine(field2.Boost);

            //Field field4 = expression;
            //Field field5 = typeof(Article).GetProperty(nameof(Article.Title));


            //Field fieldstring = "title";
            //Field fieldexp = Infer.Field<Article>(p => p.Title);

            //Field fieldwithboost = Infer.Field<Article>(p => p.Title, 2.2);




            //var expresson = new List<Expression<Func<Article, object>>>
            //{
            //    e=>e.Title,
            //    e=>e.UserID,
            //    e=>e.Sub_Title
            //};
            //var result= expresson.Select(e=>e.AppendSuffix("raw")).ToList();
            //var result2 = expresson.Select<Expression<Func<Article, object>>, Field>(e => e.AppendSuffix("raw"))
            //    .ToList();




            //Id id1 = "111";
            //Id id2 = 11;
            //Id id3 = 22;
            //Id id4 = new Guid("11");


            //var dto = new MyDTO
            //{
            //    Id=new Guid("D70BD3CF-4E38-46F3-91CA-FCBEF29B148E"),
            //    Name="x",
            //    OtherName="y"
            //};
            //var id= Id.From(dto);
            //Console.WriteLine(id.ToString());

            //var resolver = new IndexNameResolver(settings);
            //string indexname = resolver.Resolve<Article>();
            //Console.WriteLine(indexname);


            //Indices indices1 = "index1";
            //Indices indices2 = "index1,index2";
            //Indices indices3 =new []{"index1"};
            //Indices indices4 = "_all";
            //Indices indices5=typeof(Article);
            //Indices indices6 = IndexName.From<Article>();
            //var indices7 = Nest.Indices.Index("forum");
            //var indices8 = Nest.Indices.Index<Article>();

            //var manyindex1 = Nest.Indices.Index("index1","index2");
            //var manyindex2 = Nest.Indices.Index<Article>().And<MyDTO>();

            //var index9= Indices.All;
            //var index10 = Indices.AllIndices;

            //var resolver = new TypeNameResolver(settings);
            //var type1 = resolver.Resolve<Article>();
            //Console.WriteLine(type1);


            //var time1 = new Time("2d");
            //var time2 = new Time(2,TimeUnit.Day);
            //var time3 = new Time(TimeSpan.FromDays(2));
            //var time4 = new Time(2*24*60*60*1000);

            //Time oneMinute = "1m";
            //Time fourteenDays = TimeSpan.FromDays(14);
            //Time twodays = 2 * 24 * 60 * 60 * 1000;

            //if (fourteenDays > twodays)
            //{

            //}
            //if (Time.MinusOne >Time.Zero)
            //{
            //}

            //var day= DateInterval.Day;
            //Console.WriteLine(day);

            //var distance1 = new Distance("2.5km");
            //var distance2 = new Distance(3, DistanceUnit.Kilometers);

            //Distance distancestring = "25m";
            //Distance distancenumber = "25";


            //var start = Nest.DateMath.Anchored(new DateTime(2015, 5, 5));
            //var end = Nest.DateMath.Now.Add("1d");



            //Union<bool, Article> union1 = false;
            //Union<bool, Article> union2 = new Article { };
            //var union3 = new Union<bool, Article>(true);
            //var union4 = new Union<bool, Article>(new Article{ ArticleID="abd", Content="content", Follower_Num=1, Title="title"});
            //var result= union4.Match(t1 => null, t2 => JsonConvert.SerializeObject(t2));
            //Console.WriteLine(result);



            //client.CreateIndex("testindex", cid =>
            //{
            //    cid.InitializeUsing(new IndexState
            //    {
            //         Settings=new IndexSettings
            //         {
            //              NumberOfReplicas=1,
            //              NumberOfShards=5
            //         }
            //    });
            //    cid.Mappings(m => m.Map<Merchant>(mp => mp.AutoMap()));
            //   return cid;
            //});


            //var result= client.CreateIndex("indexrelation", 
            //    r => r.Mappings(m => m.Map<MyDocument>(ms =>ms
            //    .RoutingField(rt=>rt.Required())
            //    .AutoMap<MyChild>()
            //    .AutoMap<MyParent>()
            //    .Properties(ps=>ps.Join(j=>j.Name(jt=>jt.MyJoinField).Relations(rt=>rt.Join<MyParent,MyChild>())))
            //    )));

            //var result = client.IndexDocument(new MyParent
            //{
            //     Id=1,
            //     MyJoinField=JoinField.Root<MyParent>(),
            //     ParentProperty="c# is the best programming language"
            //});
            //Console.WriteLine(result);
            //result = client.IndexDocument(new MyParent
            //{
            //    Id = 2,
            //    MyJoinField = JoinField.Root<MyParent>(),
            //    ParentProperty = "f# is a superior language"
            //});

            //client.IndexDocument(new MyParent
            //{
            //    Id=5,
            //    ParentProperty= "a parent prop",
            //    MyJoinField="parent2"
            //});
            //client.IndexDocument(new MyParent
            //{
            //    Id = 6,
            //    ParentProperty = "a parent prop",
            //    MyJoinField = typeof(MyParent)
            //});


            //var result = client.IndexDocument(new MyChild
            //{
            //     Id=4,
            //     ChildProperty= "F# tokens F# 程序由各种tokens组成.",
            //     MyJoinField=JoinField.Link<MyChild>(2)
            //});


            //client.CreateIndex("testcase", f => f.Aliases(a => a.Alias("upperlower")));

            //client.Index(new NewsInfo
            //{
            //    ArticleID = "1111",
            //    Content = "有关.net core的相关技术",
            //    Follower_Num = 1,
            //    Hidden = true,
            //    Post_Date = DateTime.Now,
            //    Sub_Title = "sub title",
            //    Tag = new[] { "c#", ".net", ".net core" },
            //    Tag_Cnt = 2,
            //    Title = ".net core入门到精通",
            //    UserID = 22,
            //    View_Cnt = 3
            //}, f => f.Index("testcase"));




            //client.IndexDocument<Article>(new Article
            //{
            //    ArticleID = "1111",
            //    Content = "有关.net core的相关技术",
            //    Follower_Num = 1,
            //    Hidden = true,
            //    Post_Date = DateTime.Now,
            //    Sub_Title = "sub title",
            //    Tag = new[] { "c#", ".net", ".net core" },
            //    Tag_Cnt = 2,
            //    Title = ".net core入门到精通",
            //    UserID = 22,
            //    View_Cnt = 3
            //});
            //SearchDescriptor<Shirts> searchDescriptor = new SearchDescriptor<Shirts>();
            // FilterDescriptor<Shirts> filterDescriptor = new FilterDescriptor<Shirts>();
            // searchDescriptor.Query();

            SearchModel searchCond = new SearchModel
            {
                Status = new[]{1},
                Category = new[]{1,2},
                //District = 1,
                Top_Left = new GeoLocation(40.06192,116.30665),//116.30665,40.06192
                Bottom_Right = new GeoLocation(40.059697,116.311061),//116.311061,40.059697
                //Keyword = "拉面"
            };

            QueryContainer qc = new QueryContainer();
            if (searchCond.District > 0)
            {
                qc = qc && +new TermQuery
                {
                    Field = Infer.Field<Merchant>(f => f.District),
                    Value = searchCond.District
                };
            }
            if (searchCond.Status!=null&&searchCond.Status.Any())
            {
                qc = qc && +new TermsQuery
                {
                    Field = Infer.Field<Merchant>(f => f.Status),
                    Terms=searchCond.Status.OfType<object>()
                    //Value = searchCond.Status
                };
            }

            

            if (searchCond.Category!=null&&searchCond.Category.Any())
            {
                qc = qc && +new TermsQuery
                {
                    Field = Infer.Field<Merchant>(f => f.Category),
                    Terms=searchCond.Category.OfType<object>()
                 //   Value = searchCond.Category
                };
            }

            if (searchCond.Top_Left != null && searchCond.Bottom_Right != null)
            {
                qc = qc && +new GeoBoundingBoxQuery
                {
                    Field = Infer.Field<Merchant>(p => p.Location),
                    BoundingBox = new BoundingBox
                    {
                        TopLeft = searchCond.Top_Left, // new GeoLocation(34, -34),
                        BottomRight = searchCond.Bottom_Right //new GeoLocation(-34, 34),
                    },
                    Type = GeoExecution.Indexed
                };
            }

            if (!string.IsNullOrWhiteSpace(searchCond.Keyword))
            {
                qc = qc && new MatchQuery
                {
                    Field = Infer.Field<Merchant>(f => f.Name),
                    Query = searchCond.Keyword
                };
            }
            else
            {
                qc = qc && new MatchAllQuery();
            }
            
            ISearchResponse<Merchant> merchantSearch = client.Search<Merchant>(new SearchRequest<Merchant>("geoindex")
            {
                Query = qc,
                From = (searchCond.PageIndex - 1) * 20,
                Size = 20,
                Sort = new List<ISort>
                {
                    new SortField { Field = "category", Order = SortOrder.Ascending },
                    new GeoDistanceSort
                    {
                        DistanceType=GeoDistanceType.Arc,
                        Field=Infer.Field<Merchant>(f=>f.Location),
                        GeoUnit=DistanceUnit.Meters,
                        Order=SortOrder.Descending,
                        Points=new []{new GeoLocation(40.061409, 116.309237) }//,
                    },
                    new SortField { Field = "id", Order = SortOrder.Descending }
                },
                Aggregations = new GlobalAggregation("globalagg")
                {
                    Aggregations =new TermsAggregation("termscate")
                            {
                                Field=Infer.Field<Merchant>(m=>m.Category)
                            }
                            &&new ValueCountAggregation("value_count",Infer.Field<Merchant>(f=>f.Category))
                            &&new CardinalityAggregation("cardinality_count",Infer.Field<Merchant>(f=>f.Category))
                },
                Source = new SourceFilter
                {
                    Includes=Infer.Fields<Merchant>(f=>f.Id,f=>f.Name,f=>f.Status,f=>f.Category,f=>f.Location)
                },
                ScriptFields = new ScriptFields
                {
                    {"distance",new InlineScript("doc['location'].planeDistance(params.lat, params.lng)")
                    {
                        Lang = "painless",
                        Params = new FluentDictionary<string, object>
                        {
                            { "lat",40.061409 },
                            { "lng",  116.309237 }
                        }
                    } }
                },
                //StoredFields = Infer.Fields<Merchant>(
                //    f => f.Name,
                //     f => f.CreateDate,
                //    f => f.District,
                //    f => f.Location,
                //    f => f.Id,
                //    f => f.Status
                //    )
            });






            List<Merchant> resultQuery = merchantSearch.Hits.Select(h => new Merchant
            {
                Distance = h.Fields.Value<double>("distance"),
                Id = int.Parse(h.Id),
                Category = h.Source.Category,
                CreateDate = h.Source.CreateDate,
                District = h.Source.District,
                Location = h.Source.Location,
                Name = h.Source.Name,
                Status = h.Source.Status
            }).ToList();






            //foreach (IHit<Merchant> hit in merchantSearch.Hits)
            //{
            //    double distance = hit.Fields.Value<double>("distance");
            //}



            List<Func<QueryContainerDescriptor<Merchant>, QueryContainer>> condquery = new List<Func<QueryContainerDescriptor<Merchant>, QueryContainer>>();

            List<Func<QueryContainerDescriptor<Merchant>, QueryContainer>> condfilter = new List<Func<QueryContainerDescriptor<Merchant>, QueryContainer>>();


            if (string.IsNullOrWhiteSpace(searchCond.Keyword))
            {
                condquery.Add(s => s.MatchAll());
            }
            else
            {
                condquery.Add(s => s.Match(f => f.Field(fd => fd.Name).Query(searchCond.Keyword)));
            }
            if (searchCond.Status !=null&&searchCond.Status.Any())
            {
                condfilter.Add(s => s.Terms(f => f.Field(d=>d.Status).Terms(searchCond.Status)));
            }

            if (searchCond.Category!=null&&searchCond.Category.Any())
            {
                condfilter.Add(s => s.Terms(f => f.Field(d=>d.Category).Terms(searchCond.Category)));
            }

            if (searchCond.District > 0)
            {
                condfilter.Add(s => s.Term(f => f.District, searchCond.District));
            }

            if (searchCond.Top_Left != null && searchCond.Bottom_Right != null)
            {
                condfilter.Add(s => s.GeoBoundingBox(gbb =>
                    gbb.Field(f => f.Location).BoundingBox(bb =>
                        bb.TopLeft(searchCond.Top_Left).BottomRight(searchCond.Bottom_Right))));
            }

            Func<SortDescriptor<Merchant>, IPromise<IList<ISort>>> sortfunc = sd =>
            {
                sd.Ascending(Infer.Field<Merchant>(f => f.Category));
               
                sd.GeoDistance(sdd =>
                    sdd.Field(Infer.Field<Merchant>(f => f.Location)).DistanceType(GeoDistanceType.Plane)
                        .Unit(DistanceUnit.Meters).Points(new GeoLocation(40.061409, 116.309237)).Descending());
                sd.Descending(Infer.Field<Merchant>(f=>f.Id));
                return sd;
            };

            //动态排序
            //var dynamicSort = new SortDescriptor<Merchant>();
            //dynamicSort.Field(Infer.Field<Merchant>(f => f.Category), SortOrder.Ascending);
            //dynamicSort.Field(Infer.Field<Merchant>(f => f.Id), SortOrder.Descending);
            //dynamicSort.GeoDistance(sdd =>
            //    sdd.Field(Infer.Field<Merchant>(f => f.Location)).DistanceType(GeoDistanceType.Plane)
            //        .Unit(DistanceUnit.Meters).Points(new GeoLocation(1, 1)));

            ISearchResponse<Merchant> merchantSearch2 = client.Search<Merchant>(s => s.Index("geoindex").Query(q => q.Bool(b => b.Must(condquery).Filter(condfilter))).Sort(st => st.Ascending(p => p.Category).Descending(p => p.Id)).Source(f=>f.Includes(fd=>fd.Fields(d=>d.Id,d=>d.Name,d=>d.Status,d=>d.Status,d=>d.Location,d=>d.Category,d=>d.Location))).From((searchCond.PageIndex - 1) * 20).Size(20).Sort(sortfunc));


            //aggs
            ISearchResponse<Merchant> merchantAgg = client.Search<Merchant>(s => s.Index("geoindex").Size(0).Query(q => q.MatchAll()).Aggregations(agg => agg.Terms("termscategory", ag => ag.Field(f => f.Category))));

            IReadOnlyCollection<KeyedBucket<string>> aggs = merchantAgg.Aggregations.Terms("termscategory").Buckets;
            foreach (KeyedBucket<string> item in aggs)
            {
                Console.WriteLine($"key:{item.Key},value:{item.DocCount}");
            }



            


            Console.ReadLine();
            return;




















            QueryContainer filters_complete = new QueryContainer();
            QueryContainer dateFilter = new QueryContainerDescriptor<Shirts>().DateRange(t => t.Field("student.age").GreaterThanOrEquals(DateTime.MinValue).LessThanOrEquals(DateTime.MaxValue));
            filters_complete &= (new QueryContainerDescriptor<Shirts>().Nested(n => n.Path("student").Query(q2 => q2.Bool(bq => bq.Filter(dateFilter)))));
            //var items2 = client.Search<Shirts>(searchDescriptor);
            //ISearchResponse<Shirts> items3 = client.Search<Shirts>(s => s.Query(q => q.Bool(b=>b.Must().Filter())));



            ISearchResponse<Shirts> items = client.Search<Shirts>(s => s.Query(q => q.Term(t => t.Brand, 22)));

            IReadOnlyCollection<Shirts> itemlist = client.Search<Shirts>(s => s.Index("shirts").Type("item")).Documents;
            foreach (Shirts item in itemlist)
            {
                Console.WriteLine($"brand:{item.Brand},color:{item.Color},model:{item.Model}");
            }




            ISearchResponse<Shirts> query = client.Search<Shirts>(s => s.Query(q => q.Match(m => m.Field(f => f.Brand).Query("")) || q.Match(m => m.Field(f => f.Color).Query(""))));

            QueryContainer queryContainer = new QueryContainer();


            string keyword = "";
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                queryContainer = queryContainer || new MatchQuery
                {
                    Field = Infer.Field<Shirts>(f => f.Color),
                    Query = ""
                };
            }

            BoolQuery querybool = new BoolQuery
            {
                Should = new QueryContainer[] {

                }
            };

            ISearchResponse<Shirts> query2 = client.Search<Shirts>(new SearchRequest<Shirts>()
            {
                Query = queryContainer
            });

            QueryContainer queryitems = new TermQuery
            {
                Field = Infer.Field<Shirts>(p => p.Brand),
                Value = "11"
            } && new TermQuery
            {
                Field = Infer.Field<Shirts>(p => p.Color),
                Value = "22"
            } && +new DateRangeQuery
            {
                Field = Infer.Field<Shirts>(p => p.CreateDate),
                GreaterThanOrEqualTo = "2018-01-01",
                LessThanOrEqualTo = "2019-01-01"
            };

            //if (!string.IsNullOrWhiteSpace(keyword))
            //{
            //    queryitems = queryitems && new TermQuery {

            //    };
            //}

            ISearchResponse<Shirts> query3 = client.Search<Shirts>(new SearchRequest<Shirts>
            {
                Query = new BoolQuery
                {
                    Must = null
                }
            });



            List<Func<QueryContainerDescriptor<Shirts>, QueryContainer>> mustQuery = new List<Func<QueryContainerDescriptor<Shirts>, QueryContainer>>();
            mustQuery.Add(t => t.Term(f => f.Brand, "22"));


            ISearchResponse<News> result = client.Search<News>(s => s.Suggest(sug => sug.Completion("my-suggest", su => su.Prefix("大话西游").Field(f => f.Title.Suffix("suggest")).Size(2))));
            Suggest<News> mySuggest = result.Suggest["my-suggest"].FirstOrDefault();

            foreach (SuggestOption<News> item in mySuggest.Options)
            {
                Console.WriteLine(item.Source);
            }

            Console.WriteLine(result);



            Console.ReadLine();
        }
    }
}
