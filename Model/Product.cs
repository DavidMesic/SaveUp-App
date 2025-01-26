namespace SaveUpApp.Models
{
    public class Product
    {
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime DateSaved { get; private set; } = DateTime.Now;
    }
}