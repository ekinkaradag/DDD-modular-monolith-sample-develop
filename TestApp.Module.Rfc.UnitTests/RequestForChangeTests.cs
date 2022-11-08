using FluentAssertions;
using TestApp.BuildingBlocks.Domain;
using TestApp.Module.Rfc.Domain.RequestForChange;

namespace TestApp.Module.Rfc.UnitTests
{
    public class RequestForChangeTests
    {
        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void New_RFC_requires_key_and_title(string key, string title)
        {
            Action createRfc = () =>  RequestForChange.Create(key, title);

            createRfc.Should().Throw<BusinessRuleValidationException>();
        }

        public static IEnumerable<object[]> ConstructorTestData() {
            yield return new object[] { "", "" };
            yield return new object[] { "", "title" };
            yield return new object[] { "key", "" };
        }
    
        [Fact]
        public void New_RFC_can_be_created_with_key_and_title()
        {
            Action createRfc = () =>  RequestForChange.Create("key", "title");

            createRfc.Should().NotThrow();
        }
    
        [Fact]
        public void New_RFC_can_be_started()
        {
            var rfc = CreateRfc();

            var startRfc = () => rfc.Start();

            startRfc.Should().NotThrow();
        }
    
        [Fact]
        public void New_RFC_cannot_be_withdrawn()
        {
            var rfc = CreateRfc();

            var withDrawRfc = () => rfc.WithDraw();

            withDrawRfc.Should().Throw<BusinessRuleValidationException>();
        }
    
        [Fact]
        public void InProgress_RFC_cannot_be_started_again()
        {
            var rfc = CreateRfc();
            rfc.Start();

            var startRfc = () => rfc.Start();

            startRfc.Should().Throw<BusinessRuleValidationException>();
        }
    
        [Fact]
        public void InProgress_RFC_can_be_withdrawn()
        {
            var rfc = CreateRfc();
            rfc.Start();

            var withDrawRfc = () => rfc.WithDraw();

            withDrawRfc.Should().NotThrow();
        }
    
        [Fact]
        public void Withdrawn_RFC_cannot_be_withdrawn_again()
        {
            var rfc = CreateRfc();
            rfc.Start();
            rfc.WithDraw();

            var startRfc = () => rfc.WithDraw();

            startRfc.Should().Throw<BusinessRuleValidationException>();
        }

        private static RequestForChange CreateRfc()
        {
            return RequestForChange.Create("key", "title");
        }
    }
}