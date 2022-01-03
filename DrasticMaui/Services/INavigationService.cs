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
        /// Close the current dialog message menu if it's on screen.
        /// </summary>
        /// <param name="window">Window.</param>
        /// <returns>Task.</returns>

        Task PopModalAsync(Window window);
    }
}
