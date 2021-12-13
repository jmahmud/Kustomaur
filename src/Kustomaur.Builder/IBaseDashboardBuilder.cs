namespace Kustomaur.Dashboard
{
    /// <summary>
    /// Implement for creating a builder to manipulate the dashboard.
    /// </summary>
    public interface IBaseDashboardBuilder
    {
        /// <summary>
        /// Build will be called by the <see cref="DashboardBuilder"/> and should update the passed in <see cref="Models.Dashboard"/> object
        /// </summary>
        /// <param name="dashboard"></param>
        void Build(Models.Dashboard dashboard);

        void WithSubscription(string subscriptionId);

        void WithResourceGroup(string resourceGroup);
    }
}