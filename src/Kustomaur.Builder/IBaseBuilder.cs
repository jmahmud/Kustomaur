namespace Kustomaur.Dashboard
{
    public interface IBaseBuilder
    {
        void Build(Models.Dashboard dashboard);

        void WithSubscription(string subscriptionId);

        void WithResourceGroup(string resourceGroup);
    }
}