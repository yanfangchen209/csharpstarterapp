// /*Execution Flow:
// firstTask is created but starts only after calling firstTask.Start().
// The program then immediately starts secondTask, an asynchronous task that waits for 150 ms.
// The program calls ConsoleAfterDelay, which is a synchronous call and blocks the main thread for 75 ms.
// It then starts thirdTask, which is another asynchronous task.
// The program awaits secondTask, then firstTask, prints "After the Task was created", and finally waits for thirdTask to finish.*
// */

// namespace csharpstarterapp
// {
//     internal class Program2
//     {
//         static async Task Main(string[] args)
//         {
//             Task firstTask = new Task(() => {
//                 Thread.Sleep(100);
//                 Console.WriteLine("Task 1");

//             });
//             firstTask.Start();

//             Task secondTask = ConsoleAfterDelayAsync("Task 2", 150);

//             ConsoleAfterDelay("Delay", 75);

//             Task thirdTask = ConsoleAfterDelayAsync("Task 3", 50);

//             await secondTask;
//             await firstTask;
//             Console.WriteLine("After the Task was created");
//             await thirdTask;
//         }

//         static void ConsoleAfterDelay(string text, int delayTime)
//         {
//             Thread.Sleep(delayTime);
//             Console.WriteLine(text);
//         }

//         static async Task ConsoleAfterDelayAsync(string text, int delayTime)
//         {
//             await Task.Delay(delayTime);
//             Console.WriteLine(text);
//         }

//     }
// }
