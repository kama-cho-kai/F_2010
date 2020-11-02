import random

from flask import Flask, jsonify

MESSAGE = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]

app = Flask(__name__)
app.config["JSON_AS_ASCII"] = False  # json形式をascii以外で扱う


def get_seed():
    seed = random.randrange(10)
    return seed


@app.route("/", methods=["POST"])
def text_reaction():
    seed = get_seed()
    message = MESSAGE[seed]
    return jsonify({"message": message}), 200


if __name__ == "__main__":
    app.run(host="127.0.0.1", port="8080", debug=True)
