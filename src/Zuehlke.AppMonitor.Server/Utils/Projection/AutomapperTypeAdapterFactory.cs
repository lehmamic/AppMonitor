namespace Zuehlke.AppMonitor.Server.Utils.Projection
{
    public class AutoMapperTypeAdapterFactory
        : ITypeAdapterFactory
    {
        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutoMapperTypeAdapter();
        }

        #endregion
    }
}