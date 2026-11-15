
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using SentinelKyc.Console;

AIAgent agent = AzureOpenAIFactory.GetChatClient().AsAIAgent(
    instructions: "You are a helpful assistant that provides information with precise answers.");

Console.WriteLine("Ask a question:");
var question = Console.ReadLine();

Console.WriteLine(await agent.RunAsync(string.IsNullOrWhiteSpace(question) ? "What is the date today?" : question));
