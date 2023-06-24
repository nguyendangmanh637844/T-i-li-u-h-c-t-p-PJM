using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write("AI: Xin chào bạn iu của tui <3 Tôi có thể giúp gì cho bạn không?\n");
do
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("You: ");
    string question = Console.ReadLine();
    var api = new OpenAIClient("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");
    var chatPrompts = new List<ChatPrompt>
{
    new ChatPrompt("assistant", question),
};
    var chatRequest = new ChatRequest(chatPrompts, Model.GPT3_5_Turbo);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("AI: ");
    await api.ChatEndpoint.StreamCompletionAsync(chatRequest, result =>
    {
        Console.Write(result.FirstChoice);
    });
    Console.Write("\n");
}
while (true);