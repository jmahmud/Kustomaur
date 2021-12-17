namespace Kustomaur.Dashboard
{
    /// <summary>
    /// Implement for creating a builder to manipulate a part.
    /// </summary>
    public interface IBasePartsBuilder
    {
        /// <summary>
        /// Should update the passed in <see cref="Models.Part"/> object
        /// </summary>
        /// <param name="part"></param>
        void Build(Models.Part part);
    }
}