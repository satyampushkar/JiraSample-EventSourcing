using MediatR;

namespace JiraSample.Auth.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        //services.AddScoped(
        //    typeof(IPipelineBehavior<,>),
        //    typeof(ValidationBehavior<,>));

        ////services.AddScoped(
        ////    typeof(IPipelineBehavior<,>),
        ////    typeof(AuthValidationBehavior<,>));

        //services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
