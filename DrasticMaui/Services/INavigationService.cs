using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Services
{
    /// <summary>
    /// Navigation Service.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Gets the window.
        /// </summary>
        DrasticMauiWindow Window { get; }

        /// <summary>
        /// Close the current dialog message menu if it's on screen.
        /// </summary>
        /// <returns>Task.</returns>
        Task PopModalAsync();
    }
}
