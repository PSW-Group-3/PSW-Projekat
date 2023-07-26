using HospitalAPI;
using HospitalTests.Setup;
using System;
using Xunit;
using HospitalLibrary.Core.Model;

namespace HospitalTests.Unit
{
    public class AddressVOTests : BaseIntegrationTest
    {
        public AddressVOTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        [Fact]
        public void AddressVO_constructor_empty()
        {
            // Arrange
            String street = "Mise Dimitrijevica";
            String number = "24";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "21000";

            // Act
            Address address = new Address();

            // Assert
            Assert.NotNull(address);
        }

        [Fact]
        public void AddressVO_constructor_wrong_street_throws_exception()
        {
            // Arrange
            String street = "Mise Dimitrijevica1";
            String number = "24A";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "21000";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Address(
                street,
                number,
                city,
                township,
                postCode
                )
            );
        }

        [Fact]
        public void AddressVO_constructor_wrong_number_throws_exception()
        {
            // Arrange
            String street = "Mise Dimitrijevica";
            String number = "24[";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "21000";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Address(
                street,
                number,
                city,
                township,
                postCode
                )
            );
        }

        [Fact]
        public void AddressVO_constructor_wrong_city_throws_exception()
        {
            // Arrange
            String street = "Mise Dimitrijevica";
            String number = "24A";
            String city = "Novi-Sad";
            String township = "Brdjanlija";
            String postCode = "21782";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Address(
                street,
                number,
                city,
                township,
                postCode
                )
            );
        }

        [Fact]
        public void AddressVO_constructor_wrong_township_throws_exception()
        {
            // Arrange
            String street = "Bulevar Mise Dimitrijevica Petra";
            String number = "24A/103";
            String city = "Zrenjanin";
            String township = "K";
            String postCode = "21000";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Address(
                street,
                number,
                city,
                township,
                postCode
                )
            );
        }

        [Fact]
        public void AddressVO_constructor_wrong_postCode_throws_exception()
        {
            // Arrange
            String street = "Mise Dimitrijevica1";
            String number = "1A/1";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "02353";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Address(
                street,
                number,
                city,
                township,
                postCode
                )
            );
        }

        [Fact]
        public void AddressVO_equals()
        {
            // Arrange
            bool isValid = false;
            String street = "Mise Dimitrijevica";
            String number = "1/1";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "27120";

            // Act
            Address address1 = new Address(street, number, city, township, postCode);
            Address address2 = new Address(street, number, city, township, postCode);
            isValid = address1.Equals(address2);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void AddressVO_operator_equalsequals()
        {
            // Arrange
            bool isValid = false;
            String street = "Mise Dimitrijevica";
            String number = "1/1";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "27120";

            // Act
            Address address1 = new Address(street, number, city, township, postCode);
            Address address2 = new Address(street, number, city, township, postCode);
            isValid = address1 == address2;

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void AddressVO_operator_notequals()
        {
            // Arrange
            bool isValid = true;
            String street1 = "Mise Dimitrijevica";
            String street2 = "Paja";
            String number = "1/1";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "27120";

            // Act
            Address address1 = new Address(street1, number, city, township, postCode);
            Address address2 = new Address(street2, number, city, township, postCode);
            isValid = address1 == address2;

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void AddressVO_toString()
        {
            // Arrange
            bool isValid = true;
            String street = "Mise Dimitrijevica";
            String number = "1/1";
            String city = "Novi Sad";
            String township = "Novi Sad";
            String postCode = "27120";

            // Act
            Address address = new Address(street, number, city, township, postCode);
            string addressToString =
                "Street: {Mise Dimitrijevica} " +
                "Number: {1/1} " +
                "City: {Novi Sad} " +
                "Township: {Novi Sad} " +
                "PostCode: {27120} ";
            // Assert
            string a = address.ToString();
            Assert.True(addressToString.Equals(address.ToString()));
        }
    }
}
