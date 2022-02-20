using Shop.Domain;
using Xunit;

namespace Shop.DomainTest
{
    public class UserTest
    {
        [Fact]
        public void IsCellPhone_WithNull_ReturnFalse()
        {
            bool actual = User.IsNumberPhone(null, out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithNonsense_ReturnFalse()
        {
            bool actual = User.IsNumberPhone("213124321209429213", out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithSpace_ReturnFalse()
        {
            bool actual = User.IsNumberPhone("          ", out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithVoidString_ReturnFalse()
        {
            bool actual = User.IsNumberPhone("", out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithNoCorrectForInternationalRule_ReturnFalse()
        {
            bool actual = User.IsNumberPhone("89173620902", out string form);

            Assert.False(actual);
        }

        [Fact]
        public void IsCellPhone_WithCorectNumber_ReturnTrue()
        {
            bool actual = User.IsNumberPhone("+79876543210", out string form);

            Assert.True(actual);
        }



        // checking Email 

        [Fact]
        public void IsEmail_WithNull_ReturnFalse()
        {
            bool actual = User.IsEmail(null);

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithVoidString_ReturnFalse()
        {
            bool actual = User.IsEmail("");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithNonsense_ReturnFalse()
        {
            bool actual = User.IsEmail("asfigoasnaewiodsjnadgs");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithNotFullCorrectVale_ReturnFalse()
        {
            bool actual = User.IsEmail("gusakseva8@gmail");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithoutSymbolDog_ReturnFalse()
        {
            bool actual = User.IsEmail("gusakseva8gmail.com");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithCorrectValue_ReturnTrue()
        {
            bool actual = User.IsEmail("gusakseva8@gmail.com");

            Assert.True(actual);
        }
    }
}
