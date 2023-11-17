using RestWithASPNETErudio.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithASPNETErudio.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
