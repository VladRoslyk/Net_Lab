using Products.Common.Contracts;

namespace Products.Common.Entities
{
    // Клас для клієнтів
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Конструктор класу Customer
        public Customer(Guid id, string name, string email, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
