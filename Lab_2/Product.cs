namespace Lab_2
{
    class Product
    {
        private static ulong _nextTaskId = 1000;
        public readonly ulong Id;
        public string Name;

        public Product(string name)
        {
            Id = _nextTaskId++;
            Name = name;
        }
    }
}