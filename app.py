import os
import random

import requests
from dotenv import load_dotenv
from flask import Flask, request, jsonify, redirect

from positive_or_negative import predict
from todoist_api import get_today_tasks, get_today_belogings


app = Flask(__name__)
app.config["JSON_AS_ASCII"] = False  # json形式をascii以外で扱う

load_dotenv()


TODOIST_AUTHORIZE_URL = 'https://todoist.com/oauth/authorize'
TODOIST_ACCESS_TOKEN_URL = 'https://todoist.com/oauth/access_token'
CLIENT_ID = os.getenv('CLIENT_ID')
CLIENT_SECRET = os.getenv('CLIENT_SECRET')
SCOPE = 'data:read_write'
STATE = os.getenv('STATE')


def get_seed():
    seed = random.randrange(5)
    return seed


@app.route("/get_message", methods=["POST"])
def text_reaction():
    request_json = request.get_json()
    input_text = request_json['text']
    name = request_json['name']
    token = request_json['token']

    POSITIVE_MESSAGE = [
           f"そうだったの？ {name}が元気だと僕も嬉しい！",
           f"やったね〜！ {name}の話もっと聞かせて欲しいな",
           "そうなの？ 最高な一日だね！",
           f"{name}が楽しそうで僕も楽しい気分になったよ",
           "良かったね！ きっと良いことは続くよ〜"
            ]
    NEGATIVE_MESSAGE = [
            "そっかそっか...。 大変だったね",
            "頑張ってね。 無理はしないでね！",
            f"つらかったね。僕はいつでも{name}の味方だよ",
            "そうだったんだね。もっとお話聞くよ?",
            "そうだったんだね。もっとお話聞くよ?",
            ]

    seed = get_seed()
    if predict(input_text) == 'positive':
        message = POSITIVE_MESSAGE[seed]
        task = get_today_tasks(token)
        if len(task) != 0:
            message += f" ところで、今日{task[0]}があるみたい。頑張ってね〜"
    else:
        message = NEGATIVE_MESSAGE[seed]
    return jsonify({"status": "success", "message": message}), 200


@app.route("/get_tasks", methods=["POST"])
def get_tasks():
    request_json = request.get_json()
    token = request_json['token']
    tasks = get_today_tasks(token)
    return jsonify({"status": "success", "tasks": tasks}), 200


@app.route("/get_belogings", methods=["POST"])
def get_belogings():
    request_json = request.get_json()
    token = request_json['token']
    beloging = get_today_belogings(token)
    return jsonify({"status": "success", "belogings": beloging}), 200


# Todoist OAuth
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
