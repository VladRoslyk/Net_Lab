namespace Products.Common.Entities
{
    // Клас для моніторів, успадкований від Product
    public class Monitor : Product
    {
        public double DiagonalInInches { get; set; }       // Діагональ у дюймах
        public string Resolution { get; set; }             // Наприклад, 1920x1080
        public bool HasHDR { get; set; }                   // Чи підтримує HDR

        // Конструктор класу Monitor
        public Monitor(Guid id, string name, decimal price, string description, double diagonalInInches, string resolution, bool hasHDR)
            : base(id, name, price, description)
        {
            DiagonalInInches = diagonalInInches;
            Resolution = resolution;
            HasHDR = hasHDR;
            ProductCount++;
        }

        public override string GetProductDetails()
        {
            string hdrInfo = HasHDR ? "з HDR" : "без HDR";
            return $"Монітор: {Name}, Діагональ: {DiagonalInInches}\" , Роздільна здатність: {Resolution}, {hdrInfo}, Ціна: {Price:C}";
        }

        public static Monitor Create()
            => new Monitor(Guid.NewGuid(), string.Empty, 0, string.Empty, 0, string.Empty, false);

        public static Monitor Create(string name, decimal price, string description, double diagonalInInches, string resolution, bool hasHDR)
            => new Monitor(Guid.NewGuid(), name, price, description, diagonalInInches, resolution, hasHDR);
    }
}
