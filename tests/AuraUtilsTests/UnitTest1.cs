using AuraUtilities;
using Xunit;

namespace AuraUtilsTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("google.com")]
        [InlineData("https://wikipedia.org")]
        [InlineData("https://www.youtube.com")]
        public void UrlTest(string url)
        {
            UrlUtils.OpenUrl(url);
        }
    }
}