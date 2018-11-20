using Piranha.AttributeBuilder;
using Piranha.Extend.Fields;

namespace war3playground.Models.Regions
{
    public class Heading
    {
        [Field(Title = "Primary image")]
        public ImageField PrimaryImage { get; set; }

        [Field]
        public HtmlField Ingress { get; set; }
    }
}