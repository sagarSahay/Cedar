namespace Cedar.API
{
    using Autofac;

    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationController>()
                .AsSelf()
                .InstancePerLifetimeScope();
            
            base.Load(builder);
        }
    }
}