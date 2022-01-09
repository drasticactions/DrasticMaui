using DrasticMaui.Logger;
using DrasticMaui.Services;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Maui Service Registration.
    /// </summary>
    public static class DrasticMauiServiceRegistration
    {
        /// <summary>
        /// Register Drastic Maui Services.
        /// </summary>
        /// <param name="builder">Builder.</param>
        /// <returns>Same builder.</returns>
        /// <exception cref="ArgumentNullException">Thrown if builder doesn't exist.</exception>
        public static MauiAppBuilder RegisterDrasticMauiServices(this MauiAppBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddSingleton<ILogger, ConsoleLogger>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IErrorHandlerService, ErrorHandlerService>();

            return builder;
        }
    }
}
