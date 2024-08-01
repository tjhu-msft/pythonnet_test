using Python.Runtime;
using System;


public class RunPython
{
    public static readonly string PythonDll = @"C:\Users\t-tiahuang\AppData\Local\miniconda3\envs\python311\python311.dll";
    public static readonly string PythonEnv = @"C:\Users\t-tiahuang\AppData\Local\miniconda3\envs\python311";

    public static void Run()
    {
        Runtime.PythonDLL = @"C:\Users\t-tiahuang\AppData\Local\miniconda3\envs\python311\python311.dll";
        PythonEngine.Initialize();

        using (Py.GIL())
        {
            PythonEngine.Exec("print('Hello from Python!')");
            dynamic sys = Py.Import("sys");
            Console.WriteLine(sys.version);
        }
    }

    public static void RunVenv() {

        // be sure not to overwrite your existing "PATH" environmental variable.
        var path = Environment.GetEnvironmentVariable("PATH").TrimEnd(';');
        path = string.IsNullOrEmpty(path) ? PythonEnv : path + ";" + PythonEnv;
        Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("PYTHONHOME", PythonEnv, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("PYTHONPATH", $"{PythonEnv}\\Lib\\site-packages;{PythonEnv}\\Lib", EnvironmentVariableTarget.Process);

        Runtime.PythonDLL = PythonDll;

        PythonEngine.Initialize();
        PythonEngine.PythonHome = PythonEnv;
        PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);

        using (Py.GIL())
        {
            PythonEngine.Exec("print('Hello from Python!')");
            dynamic sys = Py.Import("sys");
            Console.WriteLine(sys.version);
        }
    }
}
