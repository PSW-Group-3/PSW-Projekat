namespace IntegrationLibrary.Core.Service.Generators
{
    public class APIKeyGenerator
    {
        private int _length = 64;
        private RandomStringGenerator _randomStringGenerator = new RandomStringGenerator();

        public string GenerateKey()
        {
            return _randomStringGenerator.GenerateBase64(_length);
        }
    }
}
