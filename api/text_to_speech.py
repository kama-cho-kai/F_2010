from google.cloud import texttospeech as tts


def text_to_speech(message):
    client = tts.TextToSpeechClient()
    synthesis_input = tts.SynthesisInput(text=message)

    voice = tts.VoiceSelectionParams(
        language_code="ja-JP", name="ja-JP-Wavenet-B"
    )

    audio_config = tts.AudioConfig(
        audio_encoding=tts.AudioEncoding.MP3
    )

    response = client.synthesize_speech(
        input=synthesis_input, voice=voice, audio_config=audio_config
    )

    # with open("output.mp3", "wb") as out:
    #     out.write(response.audio_content)

    return response.audio_content


if __name__ == "__main__":
    message = "こんばんわ"
    response = text_to_speech(message)
