using System.Threading.Tasks;

namespace AvaloniaBlazorBindings.Framework
{
    /// <summary>
    /// Add an instance of this interface to the service collection for code to run when the
    /// app starts.
    /// </summary>
    public interface IAvaloniaStartup
    {
        Task OnStartAsync();
    }
}
