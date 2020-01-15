namespace JC.Samples.Web.Api.Errors
{
    /// <summary>
    /// Represents a model Validation Error.
    /// </summary>
    public class ValidationError
    {
        #region Properties

        /// <summary>
        /// The Name of the invalid model property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Description of the model error.
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}