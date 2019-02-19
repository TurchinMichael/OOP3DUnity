namespace GeekBrains.Data
{
    public struct Player
    {
        public string Name;
        public float Hp;
        public bool IsVisible;

        public override string ToString() => $"Name {Name} Hp {Hp} IsVisible {IsVisible}";
    }
}