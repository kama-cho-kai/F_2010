import random

from flask import Flask, request, jsonify

from positive_or_negative import predict


POSITIVE_MESSAGE = [0, 1, 2, 3, 4]
NEGATIVE_MESSAGE = [5, 6, 7, 8, 9]


app = Flask(__name__)
app.config["JSON_AS_ASCII"] = False  # json形式をascii以外で扱う


def get_seed():
    seed = random.randrange(5)
    return seed


@app.route("/", methods=["POST"])
def text_reaction():
    request_json = request.get_json()
    input_text = request_json['text']
    seed = get_seed()

    if predict(input_text) == 'positive':
        message = POSITIVE_MESSAGE[seed]
    else:
        message = NEGATIVE_MESSAGE[seed]
    return jsonify({"message": message}), 200


if __name__ == "__main__":
    app.run(host="127.0.0.1", port="8080", debug=True)
