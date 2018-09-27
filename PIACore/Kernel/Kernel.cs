using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using dotenv.net;
using Json;
using Newtonsoft.Json;
using PIACore.AI.AlwaysCallAI;
using PIACore.Helpers;


namespace PIACore.Kernel
{
    /// <summary>
    /// Kernel of the project.
    /// </summary>
    public class Kernel
    {
        /// <summary>
        /// Main class.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            DotEnv.Config();

            var environments = LoadJsonDefinition();

            foreach (var environment in environments)
            {
                
                var myThread = new Thread(
                    () => SingleAiLoading(
                        (Dictionary<string, object>) ((List<object>) environment.Value)[0])
                    );
                
                myThread.Start();
            }
        }

        /// <summary>
        /// Loads a specific AI into a Game object.
        /// </summary>
        /// <param name="configuration"></param>
        private static void SingleAiLoading(Dictionary<string, object> configuration)
        {
            
            var gameType = typeof(Game<>);
            var classLocation = (string) configuration["ClassLocation"];
            var completeType = gameType.MakeGenericType(Type.GetType(classLocation));

            var runMethod = completeType.GetMethod("Run");

            object[] arguments =
            {
                (string) configuration["TableIdentifier"],
                Convert.ToInt32((double) configuration["TimeInMillis"]),
                (string) configuration["Slug"],
                (string) configuration["ApiKey"]
            };

            var game = Activator.CreateInstance(completeType, arguments);
                    
            Logger.Debug("Application thread started ",(string) configuration["Slug"]);
            runMethod.Invoke(game, null);
        }

        /// <summary>
        /// Load the JSON file containing all definitions.
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, object> LoadJsonDefinition()
        {
            Dictionary<string, object> response;
            using (StreamReader r = new StreamReader("runDefinition.json"))
            {
                string json = r.ReadToEnd();
                response = (Dictionary<string, object>) JsonParser.FromJson(json);
            }

            return response;
        }
    }
}