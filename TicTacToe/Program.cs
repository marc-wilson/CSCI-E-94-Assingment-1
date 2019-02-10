using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TicTacToe
{
	/// <summary>
	/// Main program class for application
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Main Method for Application
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			// Create the Web Host Builder
			CreateWebHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// WebHost Builder creation method.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
