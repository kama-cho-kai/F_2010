import os
import random

import requests
from dotenv import load_dotenv
from flask import Flask, request, jsonify, redirect

from positive_or_negative import predict


app = Flask(__name__)
app.config["JSON_AS_ASCII"] = False  # json形式をascii以外で扱う

load_dotenv()


POSITIVE_MESSAGE = [0, 1, 2, 3, 4]
NEGATIVE_MESSAGE = [5, 6, 7, 8, 9]

TODOIST_AUTHORIZE_URL = 'https://todoist.com/oauth/authorize'
TODOIST_ACCESS_TOKEN_URL = 'https://todoist.com/oauth/access_token'
CLIENT_ID = os.getenv('CLIENT_ID')
CLIENT_SECRET = os.getenv('CLIENT_SECRET')
SCOPE = 'data:read'
STATE = os.getenv('STATE')


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


@app.route("/cooperation_todoist")
def cooperation_todoist():
    url = f"{TODOIST_AUTHORIZE_URL}?client_id={CLIENT_ID}&scope={SCOPE}&state={STATE}"
    return redirect(url)


@app.route("/auth_todoist")
def auth_todoist():
    code = request.args.get("code")
    state = request.args.get("state")
    if state != STATE:
        return jsonify({"status": "error"}), 400

    data = {
        'client_id': CLIENT_ID,
        'client_secret': CLIENT_SECRET,
        'code': code,
        }
    response = requests.post(TODOIST_ACCESS_TOKEN_URL, data=data)
    return jsonify({"status": "success", "response": response.json()}), 200


if __name__ == "__main__":
    app.run(host="127.0.0.1", port="8080", debug=True)
