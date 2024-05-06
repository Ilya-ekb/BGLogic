namespace Core
{
    public class ROProperty<T> : DiProperty<T>
    {
        public T RValue => Value;

        public ROProperty()
        {
        }

        public ROProperty(T initValue) : base(initValue)
        {
        }

        public void SetValue(T value)
        {
            if (Value is null || !Value.Equals(value))
                Value = value;
        }
    }
}