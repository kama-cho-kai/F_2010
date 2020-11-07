using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using Grpc.Auth;
using Grpc.Core;
using UnityEngine;
using UnityEngine.UI;
using WWUtils.Audio;
using Debug = UnityEngine.Debug;

public class QuickStart : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Button button;
    [SerializeField] private static AudioSource audioSource;
    [SerializeField] private string credential;

    private const string GcpUrl = "https://www.googleapis.com/auth/cloud-platform";
    private const string ChannelTarget = "texttospeech.googleapis.com:443";

    private static TextToSpeechClient _client;
    private static AudioConfig _audioConfig;
    private static VoiceSelectionParams _voiceSelectionParams;

    private static ChannelCredentials _credentials;

    private static SynchronizationContext _context;

    private static Stopwatch _stopwatch = new Stopwatch();

    private void Start()
    {
        // ボタンを押したときのイベントを追加
        button.onClick.AddListener(() =>
        {
            var str = inputField.text;
            if (string.IsNullOrEmpty(str)) return;
            inputField.text = "";

            CreateRequest(str);
            Debug.Log($"Send Request: {str}");
        });

        // 認証情報をResourceから読み込む
        var credentialStr = Resources.Load<TextAsset>(credential).text;
        var googleCredential = GoogleCredential.FromJson(credentialStr);
        _credentials = googleCredential.CreateScoped(GcpUrl).ToChannelCredentials();

        var channel = new Channel(ChannelTarget, _credentials);
        _client = new TextToSpeechClientImpl(new TextToSpeech.TextToSpeechClient(channel), new TextToSpeechSettings());

        // オプションを記述
        _audioConfig = new AudioConfig()
        {
            AudioEncoding = AudioEncoding.Linear16,
            SampleRateHertz = 44100
        };

        // 声のパラメータを指定
        // https://cloud.google.com/text-to-speech/docs/voices?hl=jaに記載されているものから選択できます
        _voiceSelectionParams = new VoiceSelectionParams()
        {
            SsmlGender = SsmlVoiceGender.Female,
            Name = "ja-JP-Wavenet-B",
            LanguageCode = "ja-JP"
        };

        _context = SynchronizationContext.Current;
    }

    /// <summary>
    /// リクエストを送信する
    /// </summary>
    /// <param name="text">音声合成を行う対象の文</param>
    public static void CreateRequest(string text)
    {
        var request = new SynthesizeSpeechRequest
        {
            Input = new SynthesisInput { Text = text },
            AudioConfig = _audioConfig,
            Voice = _voiceSelectionParams
        };

        _stopwatch.Restart();

        // リクエストを非同期で送信し，返ってきた後に再生するメソッドに投げる
        Task.Run(async () => { SetAudioClip(await _client.SynthesizeSpeechAsync(request)); });
    }

    /// <summary>
    /// Google CloudからのレスポンスをAudioClipに書き出し，再生する
    /// </summary>
    /// <param name="response">Google Cloudからのレスポンス</param>
    private static void SetAudioClip(SynthesizeSpeechResponse response)
    {
        var bytes = response.AudioContent.ToByteArray();

        // byte[]をAudioClipで利用できる形に変換する
        var wav = new WAV(bytes);
        Debug.Log("Get Response: Elapsed time " + _stopwatch.ElapsedMilliseconds + "ms.\nData Length: " +
                  (wav.SampleCount * (1f / wav.Frequency) * 1000f).ToString("F0") + "ms.");
        _context.Post(_ =>
        {
            // AudioSourceに新しいAudioClipを貼り付ける
            audioSource.clip = AudioClip.Create("TextToSpeech", wav.SampleCount, 1, wav.Frequency, false);
            audioSource.clip.SetData(wav.LeftChannel, 0);

            // AudioClipを再生
            audioSource.Play();
        }, null);
    }
}