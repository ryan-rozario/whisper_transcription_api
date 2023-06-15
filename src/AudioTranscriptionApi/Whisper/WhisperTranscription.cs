using Whisper.net;
using Whisper.net.Ggml;

public static class WhisperTranscription
{
    private const string modelPath = "./Whisper/model_binary/ggml-base.bin";

    public static async Task<string> TranscribeAudio(string wavFileName){

        using WhisperFactory whisperFactory = WhisperFactory.FromPath(modelPath);

        // This section creates the processor object which is used to process the audio file, it uses language `auto` to detect the language of the audio file.
        using var processor = whisperFactory.CreateBuilder()
            .WithLanguage("auto")
            .Build();

        using var fileStream = File.OpenRead(wavFileName);

        string output = "";

        await foreach (var result in processor.ProcessAsync(fileStream))
        {
            Console.WriteLine($"{result.Start}->{result.End}: {result.Text}");
            output += result.Text;
        }

        return output;
        
    }
    
}