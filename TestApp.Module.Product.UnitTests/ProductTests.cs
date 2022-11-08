using FluentAssertions;
using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.UnitTests
{
    public class ProductTests
    {
        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void New_Product_requires_key_and_title(string key, string title, string version)
        {
            Action createProduct = () => Domain.Product.Product.Create(key, title, version);

            createProduct.Should().Throw<BusinessRuleValidationException>();
        }

        public static IEnumerable<object[]> ConstructorTestData() {
            yield return new object[] { "", "", "" };
            yield return new object[] { "", "title", "" };
            yield return new object[] { "key", "", "" };
            yield return new object[] { "", "", "version" };
            yield return new object[] { "key", "title", "" };
            yield return new object[] { "key", "", "version" };
            yield return new object[] { "", "title", "version" };
        }
    
        [Fact]
        public void New_product_can_be_created_with_key_and_title_and_version()
        {
            Action createProduct = () =>  Domain.Product.Product.Create("key", "title", "version");

            createProduct.Should().NotThrow();
        }

    
        [Fact]
        public void Draft_product_cannot_be_deprecated()
        {
            var product = CreateProduct();

            var deprecatedProduct = () => product.Deprecate();

            deprecatedProduct.Should().Throw<BusinessRuleValidationException>();
        }
        
        [Fact]
        public void Draft_product_cannot_be_draft_again()
        {
            var product = CreateProduct();

            var draftProduct = () => product.BackToDraft();

            draftProduct.Should().Throw<BusinessRuleValidationException>();
        }
        
        [Fact]
        public void Draft_product_can_be_approved()
        {
            var product = CreateProduct();
            
            var approvedProduct = () => product.Approve();

            approvedProduct.Should().NotThrow();
        }
    
        [Fact]
        public void Approved_for_use_product_cannot_be_approved_again()
        {
            var product = CreateProduct();
            product.Approve();

            var approvedProduct = () => product.Approve();

            approvedProduct.Should().Throw<BusinessRuleValidationException>();
        }
    
        [Fact]
        public void Approved_for_use_Product_can_be_deprecated()
        {
            var product = CreateProduct();
            product.Approve();

            var deprecatedProduct = () => product.Deprecate();

            deprecatedProduct.Should().NotThrow();
        }
        
        [Fact]
        public void Approved_for_use_Product_can_be_draft()
        {
            var product = CreateProduct();
            product.Approve();

            var draftProduct = () => product.BackToDraft();

            draftProduct.Should().NotThrow();
        }
    
        [Fact]
        public void Deprecated_Product_cannot_be_deprecated_again()
        {
            var product = CreateProduct();
            product.Approve();
            product.Deprecate();

            var deprecatedProduct = () => product.Deprecate();

            deprecatedProduct.Should().Throw<BusinessRuleValidationException>();
        }
        
        [Fact]
        public void Deprecated_Product_cannot_be_draft()
        {
            var product = CreateProduct();
            product.Approve();
            product.Deprecate();

            var draftProduct = () => product.BackToDraft();

            draftProduct.Should().Throw<BusinessRuleValidationException>();
        }
        
        [Fact]
        public void Deprecated_Product_cannot_be_approved_for_use()
        {
            var product = CreateProduct();
            product.Approve();
            product.Deprecate();

            var deprecatedProduct = () => product.Approve();

            deprecatedProduct.Should().Throw<BusinessRuleValidationException>();
        }

        private static Domain.Product.Product CreateProduct()
        {
            return Domain.Product.Product.Create("key", "title", "version");
        }
    }
}