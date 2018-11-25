using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;
using Piranha.Models;
using war3playground.BusinessLogic.Models.Interfaces;
using war3playground.BusinessLogic.Services.Interfaces;

namespace war3playground.Models
{
    [PostType(Title = "Blog post")]
    public class BlogPost : Post<BlogPost>, IW3Post
    {
        /// <summary>
        /// Gets/sets the heading.
        /// </summary>
        [Region]
        public Regions.Heading Heading { get; set; }

        public void Init(IPlayerService playerService)
        {
        }
    }
}