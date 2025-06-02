namespace Products.Common.Entities
{
    // Клас для телевізорів, успадкований від Product
    public class Television : Product
    {
        public bool HasQualityDisplay { get; set; }

        // Конструктор класу Television
        public Television(Guid id, string name, decimal price, string description, bool hasQualityDisplay)
            : base(id, name, price, description)
        {
            HasQualityDisplay = hasQualityDisplay;
            ProductCount++;
        }

        public override string GetProductDetails()
        {
            string displayQuality = HasQualityDisplay ? "Якісний екран" : "Звичайний екран";
            return $"Телевізор: {Name}, {displayQuality}, Ціна: {Price:C}";
        }

        public static Television Create()
            => new Television(Guid.NewGuid(), string.Empty, 0, string.Empty, false);

        public static Television Create(string name, decimal price, string description, bool hasQualityDisplay)
            => new Television(Guid.NewGuid(), name, price, description, hasQualityDisplay);
    }
}
