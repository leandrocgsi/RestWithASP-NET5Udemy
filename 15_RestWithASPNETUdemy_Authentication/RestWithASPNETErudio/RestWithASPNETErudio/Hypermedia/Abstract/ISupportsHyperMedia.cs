using System.Collections.Generic;

namespace RestWithASPNETErudio.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
