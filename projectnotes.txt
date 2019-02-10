TicTacToe Application / REST API

The Tic Tac Toe REST API supports two methods:
 - /executemove -- HTTP POST only
 - /calculate move -- HTTP POST only

 Execute Move (/executemove)
 The Execute Move endpoint supports a GameBoard payload (an object with a 'gameBoard' property of strings. See requirements, swagger documentation or code comments for specific detils).
 Execute Move will return a response that includes a detailed evaluation on the game state as well as the move in which the Azure Player has taken.

 Calculate Move (/calculatemove)
 The Calculate Move endpoint is essentially the same thing as the Execute Move endpoint, but acceps an additional property (playerSymbol) that execute a move on behalf of someone. (See requirements,
 swagger documentation or code comments for specific detils)

The innerworkings and plumbing of the application use models to define data contracts and inherits form base classes that share common functionality.

Models
 - GameBoardRequest - Data Contract for the incomming payload of a gameboard.
 - GameBoardResponse - Data Contract for the response after a GameBoardRequest is received. The GameBoardResponse derives from the GameManager class
 - GameBoardRequestEC - Data Contract for the Extra Credit option for the calculatemove endpoint. It derives from GameBoardRequest and also supports an additional property of playerSymbol
 - GameBoardResponseEC - Data Contract for the Extra Credit option for calculatemove endpoint. It derives from GameManager and is very similar to the GameBoardResponse class with the only
 difference being in AzurePlayerSymbol vs PlayerSymbol.
 - GameEvaluationResult - Data Contract for the outcome of a gameboard evaluation.
 - GameManager - Data Contract for the management of a given game. Supports methods for evaluating a gameboard, making a move, guessing a move, and validating a game board

 Enums
 - GameWinnerStates - Enum (aka class with string constatnts becuase enums can't be strings (???) ) for saving human error of mistyped strings throughout the application

 In addition to the REST API, a user interface is provided to play game from a visual standpoint. The UI is rendered on the home page (/index.html). It supports the ability
 to select a player symbol and play a game of tic tac toe which leverages the REST API

 UI Files
 - wwwroot/scripts/tictactoe.js - Main JavaScript file with classes to support tic tac toe operations in the UI
 - wwwroot/styles/tictactoe.css - Main css file for page styling
 - wwwroot/index.html - Main HTML code for the UI
 - wwwroot/faveicon.ico - Faveico for the site (barrowed from favicon.cc (https://www.favicon.cc/?action=icon&file_id=99873))

 Running the Project/Solution
 - Open the TicTacToe.sln file in VisualStudio or whatever your favorite IDE of the month is.
 - Restore Nuget Packages
 - Build the Solution
 - Run the TicTacToe.csproj

 Running Unit Test
 - Unit tests are provided in the TicTacToeTest project. Unit tests may only work in Visual Studio. I have not tested in any other IDE or OS outside of Windows/Visual Studio


 Azure URL: http://marcswilson-assignment1.azurewebsites.net/
 SwaggerUI: http://marcswilson-assignment1.azurewebsites.net/swagger/index.html
 SwaggerJson: http://marcswilson-assignment1.azurewebsites.net/swagger/v1/swagger.json
 GitHub: https://github.com/mswilson4040/CSCI-E-94-Assingment-1


 Known Issues:

 - When running the unit tests, there seems to be an exception that's thrown in the output (but doesn't throw an actual exception). It looks like It might just be
 trying to reference a project I previously deleted but it's hard to tell. C#/Visual Studio seem to have many different places where hint paths and project references
 exist so I'm assuming there's a bad reference in there somewhere. Everything works fine though so I quit looking into it:

 [2/9/2019 1:54:17 PM Error] LoadProjectIdsAsync: Unable to fetch new VsProject after the original was Disposed
   at Microsoft.VisualStudio.TestWindow.VsAdapters.ProjectDescriptor.<TryProjectActionAsync>d__28`1.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.VisualStudio.TestWindow.VsAdapters.ProjectDescriptor.<<get_ProjectOutput>b__30_0>d.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.VisualStudio.Threading.JoinableTask.CompleteOnCurrentThread()
   at Microsoft.VisualStudio.TestWindow.VsAdapters.ProjectDescriptor.get_ProjectOutput()
   at Microsoft.VisualStudio.TestWindow.VsAdapters.VsProjectDiscoverer.<>c__DisplayClass35_1.<GetProjectIdsFromFilesAsync>b__2(ProjectDescriptor p)
   at System.Linq.Enumerable.FirstOrDefault[TSource](IEnumerable`1 source, Func`2 predicate)
   at Microsoft.VisualStudio.TestWindow.VsAdapters.VsProjectDiscoverer.<>c__DisplayClass35_0.<GetProjectIdsFromFilesAsync>g__FindOrQueue|1(TestFileDescriptor tfd, Queue`1 unmatched)
   at Microsoft.VisualStudio.TestWindow.VsAdapters.VsProjectDiscoverer.<>c__DisplayClass35_0.<<GetProjectIdsFromFilesAsync>b__0>d.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.VisualStudio.Threading.JoinableTask.<JoinAsync>d__78.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.VisualStudio.TestWindow.VsAdapters.VsProjectDiscoverer.<GetProjectIdsFromFilesAsync>d__35.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.VisualStudio.TestWindow.VsAdapters.VsProjectDiscoverer.<LoadProjectIdsAsync>d__33.MoveNext()