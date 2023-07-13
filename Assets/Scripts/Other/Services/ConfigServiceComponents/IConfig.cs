using System;

namespace Services.ConfigServiceComponents
{
    public interface IConfig
    {
        public string KeyID { get; }
        public Type ConfigType { get; }
    }
}
