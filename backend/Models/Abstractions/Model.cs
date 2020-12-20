namespace Models.Abstractions
{
    public abstract class Model<TId> where TId : struct
    {
        private TId? Id { get; set; }
    }
}